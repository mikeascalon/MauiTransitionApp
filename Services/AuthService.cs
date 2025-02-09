﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitionApp.Data;
using TransitionApp.Models;

namespace TransitionApp.Services
{
    public class AuthService
    {
        private readonly TransitionContext _context;

        public AuthService(TransitionContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            return _context.Users
                  .Include(u => u.Tasks) // Load tasks associated with the user
                  .FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering user: {ex.Message}");
                return false;
            }
        }
    }

    
}
