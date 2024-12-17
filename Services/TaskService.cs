﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransitionApp.Data;
using TransitionApp.Models;

namespace TransitionApp.Services
{
    public class TaskService
    {
        private readonly TransitionContext _context;

        public TaskService(TransitionContext context)
        {
            _context = context;
        }

        // Get all tasks for a specific user
        public async Task<List<UserTask>> GetTasksByUserAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        // Get a specific task by ID
        public async Task<UserTask?> GetTaskByIdAsync(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        // Add a new task
        public async Task AddTaskAsync(UserTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        // Update an existing task
        public async Task UpdateTaskAsync(UserTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        // Delete a task
        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await GetTaskByIdAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
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
