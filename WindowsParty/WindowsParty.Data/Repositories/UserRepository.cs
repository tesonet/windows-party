using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windowsparty.Model;
using WindowsParty.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace WindowsParty.Data.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly UserContexts _context;
        public UserRepository(UserContexts context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User GetUser(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
