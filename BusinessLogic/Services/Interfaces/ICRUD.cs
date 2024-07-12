namespace BusinessLogic.Services.Interfaces
{
    public interface ICRUD<DTO, CreateRequest, UpdateRequest>
       where DTO : class
       where CreateRequest : class
       where UpdateRequest : class
    {
        IEnumerable<DTO> GetAll();

        Task<DTO> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<int> CreateAsync(CreateRequest requestObject, CancellationToken cancellationToken);

        Task UpdateAsync(UpdateRequest requestObject, CancellationToken cancellationToken);

        Task DeleteByIdAsync(int id, CancellationToken cancellationToken);
    }
}
