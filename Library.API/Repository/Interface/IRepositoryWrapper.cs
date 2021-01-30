namespace Library.API.Repository.Interface
{
    public interface IRepositoryWrapper
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
    }
}