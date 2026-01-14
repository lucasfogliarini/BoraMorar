namespace BoraMorar
{
    public interface IStatusManaged<TStatus>
    {
        TStatus Status { get; }
        void ChangeStatus(TStatus newStatus);
    }
}
