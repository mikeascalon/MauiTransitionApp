using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TransitionApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } // Primary key
        public string Username { get; set; } = string.Empty; // Required field
        public string PasswordHash { get; set; } = string.Empty; // Store hashed passwords
        public string? Email { get; set; }
        public string Role { get; set; } = "User"; // Default role is User
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for related tasks
        public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();

        public TaskTemplateType TaskTemplate { get; set; } = TaskTemplateType.ETS; // Default to ETS
    }

    public enum TaskTemplateType
    {
        ETS,       // Expiration of Term of Service
        Retiring,  // Retirement
        MedBoard   // Medical Board
    }
}
