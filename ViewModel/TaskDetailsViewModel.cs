﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TransitionApp.Services;
using TransitionApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TransitionApp.ViewModel
{
    internal class TaskDetailsViewModel:INotifyPropertyChanged
    {

        private readonly TaskService _taskService;

        public UserTask _task;

        public ICommand SaveCommand { get; }

        public TaskDetailsViewModel(UserTask task, TaskService taskService)
        {
            Task = task;
            _taskService = taskService;

            
        }

        public UserTask Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged();
            }
        }

        public async Task SaveTaskAsync()
        {
            await _taskService.UpdateTaskAsync(Task);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
