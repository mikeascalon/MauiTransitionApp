using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TransitionApp.Models
{
    public class UserTask
    {
        [Key]
        public int TaskId { get; set; } // Primary key
        public string TaskName { get; set; } = string.Empty; // Task name
        public int MonthsLeft { get; set; } // 24,18, 12, or 6
        public bool IsDone { get; set; } = false; // Completion status
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property
    }
}
