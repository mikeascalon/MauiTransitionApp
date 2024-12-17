using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitionApp.Data;

namespace TransitionApp.Services
{
    public class AuthService
    {
        private readonly TransitionContext _context;

        public AuthService(TransitionContext context)
        {
            _context = context;
        }

        public bool Authenticate(string username, string password)
        {
            // Retrieve user and compare plain text password
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            return user != null && user.PasswordHash == password;
        }

    }
}
