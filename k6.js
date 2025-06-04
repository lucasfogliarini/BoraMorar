import http from 'k6/http';
import { check, sleep } from 'k6';

let token;
token = '..';
const endpoint = 'http://localhost:1001';

const executor = 'constant-arrival-rate'; // Executor que mantém uma taxa constante de requisições
//const executor = 'constant-vus'; // Executor que mantém um número constante de VUs (Virtual Users)
const durationValue = 1; // Duração do teste em durationUnit
const durationUnit = 'm'; // 'm' para minutos, 's' para segundos
const rate = 80000; // Taxa de Entrada | Arrival Rate (λ) por minuto
const vus = 200; // Número de VUs alocados ou pré-alocados para o teste

const timeoutValue = 10;
const timeout = `${timeoutValue}s`; // Define o timeout para as requisições
const duration = durationValue + durationUnit; // Converte o valor de duração para o formato correto

export const options = {
    scenarios: {
        run: {
            executor: executor,
            //vus: vus, // Número de VUs alocados para o teste usando executor 'constant-vus'
            preAllocatedVUs: vus, // Número de VUs pré-alocados para o teste
            maxVUs: vus * 10, // Número máximo de VUs que podem ser alocados durante o teste
            rate: rate,
            timeUnit: '1m',// Define a taxa de requisições por minuto
            duration: duration,
            exec: 'run', // Nome da função que será executada
        }
    },
};

const headers = {
    'Accept': 'text/plain',
    'Authorization': `Bearer ${token}`,
};

export function run() {
    const res = http.get(endpoint, { headers, timeout: timeout });
    //console.log(`${res.status_text}: ${res.timings.duration}ms`);
    check(res, {
        '200_399': (r) => r.status >= 200 && r.status < 400
    });

    //sleep(1); // Pausa de 1 segundo entre as requisições
}

export function handleSummary(data) {
    // Helper para converter bytes para MB
    const bytesToMB = (bytes) => (bytes / (1024 * 1024)).toFixed(2);

    const {
        iterations,
        successRate,
        successPasses,
        failureRate,
        successFails
    } = getStatusRates(data);

    const green = "\x1b[32m";
    const red = "\x1b[31m";
    const yellow = "\x1b[33m";
    const reset = "\x1b[0m";

    const testRunDurationSec = (data.state.testRunDurationMs / 1000);
    const rateSec = data.metrics.http_reqs.values.rate;
    const rateMin = rateSec * 60;
    const vusAvgAprox = (data.metrics.vus.values.min + data.metrics.vus.values.max) / 2;
    return {
        stdout: `
======== 📊 Resumo do teste usando ${executor} ========

🌐 Endpoint: ${endpoint}

✔ Requisições por status
  - ${green} Sucesso (2xx-3xx): ${successRate.toFixed(2)}% (${successPasses} de ${iterations})
  - ${red} Falhas (4xx-5xx): ${failureRate.toFixed(2)}% (${successFails} de ${iterations})
${reset}

⚡ Taxa de Entrada | Arrival Rate (λ): ${rateSec.toFixed(2)} req/s | ${rateMin.toFixed()} req/min de ${rate} req/min pretendido
✔ Vazão | Throughput (X): ${(successPasses / testRunDurationSec).toFixed(2)} req/s

⏱ Tempo de resposta | Response Time (R):
   - Média:  ${data.metrics.http_req_duration.values.avg.toFixed(2)} ms
   - Máximo: ${data.metrics.http_req_duration.values.max.toFixed(2)} ms
   - Mínimo: ${data.metrics.http_req_duration.values.min.toFixed(2)} ms
   - Mediana: ${data.metrics.http_req_duration.values.med.toFixed(2)} ms

👥 Usuários Virtuais por segundo | Virtual Users (λ⋅R):
   - Médio: ${vusAvgAprox}
   - Máximo: ${data.metrics.vus_max.values.max}
   - Mínmo: ${data.metrics.vus_max.values.min}

🔁 Iterações concluídas: ${data.metrics.iterations.values.count}

📤 Tráfego de dados:
   - Enviados:  ${bytesToMB(data.metrics.data_sent.values.count)} MB
   - Recebidos: ${bytesToMB(data.metrics.data_received.values.count)} MB

⏱️ Duração total do teste: ${testRunDurationSec.toFixed(2)} s
`,
    };
}

function getStatusRates(data) {
    const checkStatusSuccess = data.root_group.checks.find(c => c.name === '200_399');

    const iterations = data.metrics.iterations.values.count;

    const successPasses = checkStatusSuccess?.passes || 0;
    const successFails = checkStatusSuccess?.fails || 0;

    const successRate = iterations > 0 ? (successPasses / iterations) * 100 : 0;
    const failureRate = iterations > 0 ? (successFails / iterations) * 100 : 0;

    return {
        iterations,
        successRate,
        successPasses,
        failureRate,
        successFails
    };
}


/*
| Tempo de Resposta | Classificação | Comentário                                      |
| ----------------- | ------------- | ----------------------------------------------- |
| **< 1ms**         | Ultra-rápido  | Muito raro fora de memória local/cache          |
| **1–10ms**        | Excelente     | Tipicamente dentro do mesmo datacenter ou cache |
| **10–100ms**      | Bom           | Considerado rápido para APIs em rede            |
| **100–500ms**     | Aceitável     | Padrão aceitável para web APIs                  |
| **> 500ms – 1s**  | Lento         | Pode impactar UX dependendo da aplicação        |
| **> 1s**          | Ruim          | Tempo de espera visível para o usuário          |
*/
