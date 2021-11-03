using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCiCd.Entities;

namespace WebApiCiCd.Services
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
    }
}