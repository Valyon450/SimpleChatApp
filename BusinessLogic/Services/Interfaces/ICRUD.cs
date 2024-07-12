namespace BusinessLogic.Services.Interfaces
{
    public interface ICRUD<DTO, CreateRequest, UpdateRequest>
       where DTO : class
       where CreateRequest : class
       where UpdateRequest : class
    {
        Task<IEnumerable<DTO>?> GetAllAsync(CancellationToken cancellationToken);

        Task<DTO?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<int> CreateAsync(CreateRequest requestObject, CancellationToken cancellationToken);

        Task UpdateAsync(UpdateRequest requestObject, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
