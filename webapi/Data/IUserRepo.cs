using webapi.Models.Entities;

namespace webapi.Data
{
    public interface IUserRepo
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(Guid id);
        Task CreateUser(User user);
        Task DeleteUserId(Guid id);

        Task<User?> GetUserByEmail(string email);
    }
}
