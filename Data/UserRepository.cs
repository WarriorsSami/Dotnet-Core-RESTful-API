using System.Linq;
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
        
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();

            return user;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(usr => usr.Email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(usr => usr.Id == id);
        }
    }
}