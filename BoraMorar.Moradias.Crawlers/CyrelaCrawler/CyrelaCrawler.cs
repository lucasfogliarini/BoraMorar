using AngleSharp;
using AngleSharp.Dom;
using BoraMorar.Imoveis;

namespace BoraMorar.Moradias.Crawlers
{
    /// <summary>
    /// Crawler especializado em extrair dados de empreendimentos do site da Cyrela.
    /// Ele acessa a página principal de listagem e depois entra em cada link para obter os detalhes.
    /// </summary>
    public class CyrelaCrawler : ICyrelaCrawler
    {
        const string BaseUrl = "https://www.cyrela.com.br/";
        const string PropertiesUrl = "https://www.cyrela.com.br/empreendimentos?field_cidade=218";

        /// <summary>
        /// Realiza o processo completo de crawling:
        /// - Acessa a página principal
        /// - Coleta os links dos empreendimentos
        /// - Visita cada página individual
        /// - Extrai dados e preenche CrawledProperty
        /// - Filtra pelo intervalo de datas
        /// </summary>
        public async Task<IEnumerable<CrawledProperty>> CrawlPropertiesAsync(DateTime startDate, DateTime? endDate = null)
        {
            endDate ??= DateTime.MaxValue;
            var propriedades = new List<CrawledProperty>();

            // Coleta os links de cada empreendimento
            var links = await ObterLinksEmpreendimentosAsync();

            // Visita e extrai detalhes de cada página
            foreach (var link in links)
            {
                var propriedade = await ExtrairDetalhesEmpreendimentoAsync(link);
                if (propriedade != null && propriedade.CrawledAt >= startDate && propriedade.CrawledAt <= endDate)
                {
                    propriedades.Add(propriedade);
                }
            }

            return propriedades.OrderBy(p => p.CrawledAt);
        }

        /// <summary>
        /// Extrai os links dos empreendimentos a partir da página principal.
        /// Apenas links com "/empreendimentos/" são considerados.
        /// </summary>
        private async Task<List<string>> ObterLinksEmpreendimentosAsync()
        {
            var document = await CarregarDocumentoHtmlAsync(PropertiesUrl);

            var links = document.QuerySelectorAll("a")
                .Select(a => a.GetAttribute("href"))
                .Where(href => !string.IsNullOrEmpty(href) && href.Contains("/empreendimentos/"))
                .Select(href => href.StartsWith("http") ? href : $"{BaseUrl}{href}")
                .Distinct()
                .ToList();

            return links;
        }

        private int? ExtrairArea(string descricao, IDocument document)
        {
            var match = System.Text.RegularExpressions.Regex.Match(descricao, @"(\d+)\s*m²");
            return match.Success ? int.Parse(match.Groups[1].Value) : ExtrairNumero(document, ".field_suites");
        }

        private int? ExtrairQuartos(string descricao, IDocument document)
        {
            var match = System.Text.RegularExpressions.Regex.Match(descricao, @"(\d+)\s*dormit[óo]rios?");
            return match.Success ? int.Parse(match.Groups[1].Value) : ExtrairNumero(document, ".field_dormitorios");
        }

        private int? ExtrairVagas(string descricao, IDocument document)
        {
            var match = System.Text.RegularExpressions.Regex.Match(descricao, @"(\d+)\s*vagas?");
            return match.Success ? int.Parse(match.Groups[1].Value) : ExtrairNumero(document, ".field_vagas");
        }

        /// <summary>
        /// Visita a página de um empreendimento individual e extrai os dados relevantes
        /// como nome, preço, área, endereço, características e links auxiliares.
        /// </summary>
        private async Task<CrawledProperty?> ExtrairDetalhesEmpreendimentoAsync(string url)
        {
            try
            {
                var document = await CarregarDocumentoHtmlAsync(url);

                string GetMeta(string propertyName) => document.QuerySelector($"meta[property='{propertyName}']")?.GetAttribute("content")?.Trim() ?? "";                                

                var endereco = new Endereco(
                    tipo: TipoEndereco.Logradouro,
                    logradouro: GetMeta("og:street_address"),
                    cidade: GetMeta("og:locality"),
                    bairro: GetMeta("og:region"),
                    cep: document.QuerySelector(".cep")?.TextContent?.Trim() ?? ""
                );

                // Lista de características adicionais
                var caracteristicas = document.QuerySelectorAll(".caracteristicas li")
                    .Select(li => li.TextContent.Trim())
                    .ToList();

                var descricao = GetMeta("og:description");
                var bookHref = document.QuerySelector(".btnDownloadBook a")?.GetAttribute("href");
                return new CrawledProperty
                {
                    CrawledAt = DateTime.Now,
                    Nome = GetMeta("og:title"),
                    AreaPrivativa = ExtrairArea(descricao, document),
                    Endereco = endereco,
                    Quartos = ExtrairQuartos(descricao, document),
                    Banheiros = ExtrairVagas(descricao, document),
                    VagasGaragem = ExtrairVagas(descricao, document),
                    Caracteristicas = caracteristicas,
                    PaginaUrl = GetMeta("og:url"),
                    BookUrl = $"{BaseUrl}{bookHref}",
                    Incorporadora = Incorporadora.CyrelaGoldsztein,
                };
            }
            catch
            {
                // Erros são ignorados silenciosamente; idealmente deveria haver log
                return null;
            }
        }

        /// <summary>
        /// Carrega e parseia um documento HTML da URL fornecida.
        /// Usa AngleSharp para abrir o conteúdo em memória.
        /// </summary>
        private async Task<IDocument> CarregarDocumentoHtmlAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            using var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            return await context.OpenAsync(req => req.Content(html));
        }

        /// <summary>
        /// Extrai um número inteiro de um seletor, retornando 0 se não for possível converter.
        /// </summary>
        private int? ExtrairNumero(IDocument document, string seletor)
        {
            var texto = document.QuerySelector(seletor)?.TextContent?.Trim();
            return int.TryParse(texto, out var numero) ? numero : null;
        }
    }
}
