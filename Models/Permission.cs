using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TransitionApp.Models
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; } // Primary key

        // Foreign Key to User
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } // Navigation property for the user

        // Foreign Key to Supervisor
        public int SupervisorId { get; set; }
        [ForeignKey(nameof(SupervisorId))]
        public User Supervisor { get; set; } // Navigation property for the supervisor

        public bool CanViewProgressBar { get; set; } = true; // Default permission
    }
}
