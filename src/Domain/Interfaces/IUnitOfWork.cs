namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    ICustomerRepository CustomerRepository { get; }
}
