using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UltraSuperMegaTimer_2_pro
{
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer _timer;
        private Dictionary<string, DateTime> _timers = new Dictionary<string, DateTime>();
        private string _selectedTimerName;

        public MainWindow()
        {
            InitializeComponent();

            UpdateCurrentTime();



            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Tick += new EventHandler(DispatcherTimer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }
        public void UpdateCurrentTime()
        {
            if (_selectedTimerName == null) return;

            DateTime currentTime = DateTime.Now;
            if (TimeFormat.SelectedIndex != -1)
            {
                TimeSpan timeSpan = _timers[_selectedTimerName] - new DateTime(
                    currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.Minute, currentTime.Second);

                switch (TimeFormat.SelectedIndex)
                {
                    case 0:
                        Time.Content = $"{timeSpan.Days}:{timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
                        break;
                    case 1:
                        Time.Content = $"{timeSpan.Hours + timeSpan.Days * 24}:{timeSpan.Minutes}:{timeSpan.Seconds}";
                        break;
                    case 2:
                        Time.Content = $"{timeSpan.Minutes + (timeSpan.Hours + timeSpan.Days * 24) * 60}:{timeSpan.Seconds}";
                        break;
                    case 3:
                        Time.Content = $"{timeSpan.Seconds + (timeSpan.Minutes + (timeSpan.Hours + timeSpan.Days * 24) * 60) * 60}";
                        break;
                }
            }
        }

        private void TimeFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCurrentTime();
        }

        private void AddTimer_Click(object sender, RoutedEventArgs e)
        {
            AddTimer addWindow = new AddTimer(this);
            addWindow.Show();
        }

        public void AddNewTimer(string name, DateTime date)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Header = name;
            menuItem.Click += Timer_Click;
            if (_timers.ContainsKey(name) == false)
            {
                _timers.Add(name, date);
                Timers.Items.Add(menuItem);
            }
            else
            {
                _timers[name] = date;
            }
            OpenTimer(name);
        }

        private void Timer_Click(object sender, RoutedEventArgs e)
        {
            string header = sender.ToString().Substring(
                sender.ToString().IndexOf("Header:"),
                sender.ToString().LastIndexOf(" ") - sender.ToString().IndexOf("Header:"));
            header = header.Replace("Header:", string.Empty);
            OpenTimer(header);
        }

        private void OpenTimer(string header)
        {
            _selectedTimerName = header;
            UpdateCurrentTime();
            Name.Content = _selectedTimerName;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Timers.Items.Remove(_selectedTimerName); //Not worrking
            _timers.Remove(_selectedTimerName);
            _selectedTimerName = null;
            Name.Content = string.Empty;
            Time.Content = string.Empty;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AddTimer addWindow = new AddTimer(this, _selectedTimerName, _timers[_selectedTimerName]);
            addWindow.Show();
        }
    }
}
