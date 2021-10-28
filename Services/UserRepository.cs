using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCiCd.Models;

namespace WebApiCiCd.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
        
        public async Task<User> Create(User user)
        { 
            await _context.Users.AddAsync(user);
            user.Id = await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(usr => usr.Email == email);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(usr => usr.Id == id);
        }
    }
}