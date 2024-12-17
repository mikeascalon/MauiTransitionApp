using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitionApp.Models
{
    public class TaskTemplate
    {
        public int TaskTemplateId { get; set; } // Primary key
        public TaskTemplateType TemplateType { get; set; } // ETS, Retiring, MedBoard
        public string Name { get; set; } = string.Empty; // Task name
        public string Description { get; set; } = string.Empty; // Optional task description
        public int Order { get; set; } // Order in which tasks should appear

        public int? MonthsLeft { get; set; } // Number of months left (nullable for tasks without a timeline)
    }
}
