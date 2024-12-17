using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransitionApp.Data;
using TransitionApp.Models;


namespace TransitionApp.Services
{
    public class TaskTemplateService
    {

        private readonly TransitionContext _context;

        public TaskTemplateService(TransitionContext context)
        {
            _context = context;
        }

        // Fetch TaskTemplates by type (ETS, Retiring, MedBoard)
        public async Task<List<TaskTemplate>> GetTemplatesByTypeAsync(TaskTemplateType type)
        {
            return await _context.TaskTemplates
                                 .Where(t => t.TemplateType == type)
                                 .OrderBy(t => t.Order)
                                 .ToListAsync();
        }

        // Fetch all templates if needed
        public async Task<List<TaskTemplate>> GetAllTemplatesAsync()
        {
            return await _context.TaskTemplates.OrderBy(t => t.Order).ToListAsync();
        }
    }
}
