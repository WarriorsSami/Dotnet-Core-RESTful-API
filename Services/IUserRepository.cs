using System.Threading.Tasks;
using WebApiCiCd.Models;

namespace WebApiCiCd.Data
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id);
    }
}