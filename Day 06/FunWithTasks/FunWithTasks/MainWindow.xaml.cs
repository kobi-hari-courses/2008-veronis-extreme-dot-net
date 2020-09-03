using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace FunWithTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnGo_Click(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("A");
            var res = await goAsync();
            Debug.WriteLine("B");
        }

        private async Task<string> goAsync()
        {
            Debug.WriteLine("1");
            txtStatus.Text = "Calculating...";
            btnGo.IsEnabled = false;
            btnCancel.IsEnabled = true;
            progress.IsIndeterminate = true;

            Debug.WriteLine("2");

            var results = await PrimesCalculator.GetAllPrimesAsync(2, 180000);

            Debug.WriteLine("3");

            lstResults.ItemsSource = results;
            txtStatus.Text = "Completed";
            btnGo.IsEnabled = true;
            btnCancel.IsEnabled = false;
            progress.IsIndeterminate = false;

            Debug.WriteLine("4");

            return "Hello";
        }

        private void btnGo_Click1(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "Calculating...";
            btnGo.IsEnabled = false;
            btnCancel.IsEnabled = true;
            progress.IsIndeterminate = true;

            PrimesCalculator.GetAllPrimesAsync(2, 180000)
                .ContinueWith(task =>
                {
                    var results = task.Result;
                    lstResults.ItemsSource = results;
                    txtStatus.Text = "Completed";
                    btnGo.IsEnabled = true;
                    btnCancel.IsEnabled = false;
                    progress.IsIndeterminate = false;

                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
