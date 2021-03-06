﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using DoneInTime.Model;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace DoneInTime.ViewModel
{
    public class TimeCounterViewModel
    {
        #region "Members"
        public TimeCounter TimeCounter { get; private set; }

        private ObservableCollection<TaskViewModel> _tasks;

        public ICollectionView Tasks { get; private set; }
        #endregion

        #region "Commands"
        public ICommand AddTaskCommand { get; internal set;} 
        public void ExecuteAddTaskCommand()
        {
            Task t = new Task("New Task", TimeCounter);
            _tasks.Add(new TaskViewModel(t));
            TimeCounter.Tasks.Add(t);
        }

        public ICommand DelTaskCommand { get; internal set; }
        public void ExecuteDelTaskCommand()
        {
            TaskViewModel tv = (TaskViewModel)Tasks.CurrentItem;
            _tasks.Remove(tv);
            TimeCounter.Tasks.Remove(tv.Task);
            tv.Task.Dispose();
        }
        public bool CanExecuteDelTaskCommand()
        {
            if (Tasks.CurrentItem != null) return true;
            return false;
        }

        public ICommand ResetTasksCommand { get; internal set; }
        public void ExecuteResetTasksCommand()
        {
            var iResult = System.Windows.MessageBox.Show("Reset all tasks ?", "Reset", System.Windows.MessageBoxButton.OKCancel);
            if (iResult == System.Windows.MessageBoxResult.OK)
            {
                foreach (TaskViewModel tv in Tasks)
                {
                    tv.Task.TimeCount = new TimeSpan();
                }
            }
        }
        public bool CanExecuteResetTasksCommand()
        {
            if (Tasks.IsEmpty == false) return true;
            return false;
        }
        #endregion

        #region "Constructors"
        public TimeCounterViewModel()
        {
            var xmlPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DoneInTime");
            if (!Directory.Exists(xmlPath))
            {
                Directory.CreateDirectory(xmlPath);
            }

            TimeCounter = new TimeCounter(Path.Combine(xmlPath,"Tasks.xml"));
            List<TaskViewModel> l = (from c in TimeCounter.Tasks select new TaskViewModel(c)).ToList<TaskViewModel>();
            _tasks = new ObservableCollection<TaskViewModel>(l);
            Tasks = CollectionViewSource.GetDefaultView(_tasks);
            AddTaskCommand = new RelayCommand(ExecuteAddTaskCommand);
            DelTaskCommand = new RelayCommand(ExecuteDelTaskCommand, CanExecuteDelTaskCommand);
            ResetTasksCommand = new RelayCommand(ExecuteResetTasksCommand, CanExecuteResetTasksCommand);
        }
        #endregion
    }

}
