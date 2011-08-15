using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Input;

namespace DoneInTime.Model
{
    public sealed class Task : IDisposable, INotifyPropertyChanged
    {
        #region "Members"
        private TimeCounter timeCounter { get; set; }
        private DispatcherTimer chrono { get; set; }

        private string name;
        public string Name {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private TimeSpan _timeCount;
        public TimeSpan TimeCount
        {
            get
            {
                return _timeCount;
            }
            set
            {
                _timeCount = value; 
                NotifyPropertyChanged("TimeCount");
            }
        }

        private bool _isRunning;
        public bool IsRunning {
            get
            {
                return _isRunning;
            }
            private set
            {
                _isRunning = value;
                NotifyPropertyChanged("IsRunning");
            }
        }
        #endregion

        #region "Methods"
        public void Start() {
            if (!Properties.Settings.Default.allowMultipleRunningTasks)
            {
                foreach (Task t in timeCounter.Tasks)
                {
                    t.Stop();
                }
            }
            chrono.Start();
            IsRunning = true;
        }

        public void Stop()
        {
            chrono.Stop();
            IsRunning = false;
        }
        public void Dispose()
        {
            chrono.Tick -= Chrono_Tick;
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region "Events"
        private void Chrono_Tick(object sender, EventArgs e)
        {
            TimeCount = TimeCount.Add(chrono.Interval);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        #region "Constructors"
        private void Initialize(string name, TimeSpan timeCount, TimeCounter tc)
        {
            Name = name;
            TimeCount = timeCount;
            timeCounter = tc;
            chrono = new DispatcherTimer();
            chrono.Interval = TimeSpan.FromSeconds(1);
            chrono.Tick += Chrono_Tick;
        }

        public Task(string name, TimeCounter tc)
        {
            Initialize(name, new TimeSpan(), tc);
        }

        public Task(string name, TimeSpan timeCount, TimeCounter tc)
        {
            Initialize(name, timeCount, tc);
        }
        #endregion
    }
}
