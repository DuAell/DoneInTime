using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using DoneInTime.Model;

namespace DoneInTime.ViewModel
{
    public class TaskViewModel
    {
        #region "Members"
        public Task Task { get; private set; }
        #endregion

        #region "Commands"
        public ICommand StartChronoCommand { get; internal set; }
        public void ExecuteStartChronoCommand()
        {
            Task.Start();
        }
        public bool CanExecuteStartChronoCommand()
        {
            return !Task.IsRunning;
        }

        public ICommand StopChronoCommand { get; internal set; }
        public void ExecuteStopChronoCommand()
        {
            Task.Stop();
        }
        public bool CanExecuteStopChronoCommand()
        {
            return Task.IsRunning;
        }
        #endregion

        #region "Constructors"
        public TaskViewModel(Task task)
        {
            Task = task;
            StartChronoCommand = new RelayCommand(ExecuteStartChronoCommand, CanExecuteStartChronoCommand);
            StopChronoCommand = new RelayCommand(ExecuteStopChronoCommand, CanExecuteStopChronoCommand);
        }
        #endregion
    }
}
