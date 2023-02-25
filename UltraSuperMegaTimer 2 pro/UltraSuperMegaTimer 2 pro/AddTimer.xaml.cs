using System;
using System.Windows;
using System.Windows.Controls;

namespace UltraSuperMegaTimer_2_pro
{
    public partial class AddTimer : Window
    {
        private MainWindow _mainWindow;
        public AddTimer(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        } 

        public AddTimer(MainWindow mainWindow, string name, DateTime dateTime)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            Name.Text = name;
            Calendar.SelectedDate = dateTime;
            Hours.Text = dateTime.Hour.ToString();
            Minutes.Text = dateTime.Minute.ToString();
            Seconds.Text = dateTime.Second.ToString();
        }

        private void AddNewTimer(object sender, RoutedEventArgs e)
        {
            if (Name.Text == null || Calendar.SelectedDate == null) return;

            DateTime dateTime = new DateTime(Calendar.SelectedDate.Value.Year,
                Calendar.SelectedDate.Value.Month,
                Calendar.SelectedDate.Value.Day,
                int.Parse(Hours.Text),
                int.Parse(Minutes.Text),
                int.Parse(Seconds.Text));

            _mainWindow.AddNewTimer(Name.Text, dateTime);
            Close();
        }

        private void Hours_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((int.TryParse(Hours.Text, out int hours) == false) || (hours < 0 || hours >= 24))
                Hours.Text = 0.ToString();
        }

        private void Minutes_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((int.TryParse(Minutes.Text, out int minutes) == false) || (minutes < 0 || minutes >= 60))
                Minutes.Text = 0.ToString();
        }

        private void Seconds_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((int.TryParse(Seconds.Text, out int seconds) == false) || (seconds < 0 || seconds >= 60))
                Seconds.Text = 0.ToString();
        }
    }
}
