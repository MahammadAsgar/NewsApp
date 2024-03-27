namespace NewsMedia.Application.UnitOfWorks
{
    public interface IUnitofWork
    {
        void Commit();
        Task CommitAsync();
    }
}
