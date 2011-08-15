using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DoneInTime.ViewModel;

namespace DoneInTime.View
{
    /// <summary>
    /// Interaction logic for TimeCounter.xaml
    /// </summary>
    public partial class TimeCounter : Window
    {
        public TimeCounter()
        {
            InitializeComponent();
            this.DataContext = new TimeCounterViewModel();
        }
    }
}
