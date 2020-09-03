using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace FunWithTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cts = null;

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
            try
            {
                _cts = new CancellationTokenSource();
                var progress = new Progress<int>(val => progressBar.Value = val);


                Debug.WriteLine("1");
                txtStatus.Foreground = Brushes.Black;
                txtStatus.Text = "Calculating...";
                btnGo.IsEnabled = false;
                btnCancel.IsEnabled = true;
                progressBar.Value = 0;

                Debug.WriteLine("2");

                var results = await PrimesCalculator.GetAllPrimesAsync(2, 180000, _cts.Token, progress);

                Debug.WriteLine("3");

                lstResults.ItemsSource = results;
                txtStatus.Foreground = Brushes.Green;
                txtStatus.Text = "Completed";

                Debug.WriteLine("4");
            }
            catch (AccessViolationException)
            {
                lstResults.ItemsSource = null;
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Text = "Randomly Failed on access violation...";
            } 
            catch (OperationCanceledException)
            {
                lstResults.ItemsSource = null;
                txtStatus.Foreground = Brushes.Orange;
                txtStatus.Text = "Operation Cancelled By User";
            }
            finally
            {
                btnGo.IsEnabled = true;
                btnCancel.IsEnabled = false;
                progressBar.Value = 100;
                _cts = null;
            }

            return "Hello";
        }

        private void btnGo_Click1(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "Calculating...";
            btnGo.IsEnabled = false;
            btnCancel.IsEnabled = true;
            //progress.IsIndeterminate = true;

            PrimesCalculator.GetAllPrimesAsync(2, 180000)
                .ContinueWith(task =>
                {
                    try
                    {
                        var results = task.Result;
                        lstResults.ItemsSource = results;
                        txtStatus.Text = "Completed";
                    }
                    catch (AggregateException ae)
                    {
                        txtStatus.Foreground = Brushes.Red;
                        txtStatus.Text = "Access Violation Exception";
                    }
                    finally
                    {
                        btnGo.IsEnabled = true;
                        btnCancel.IsEnabled = false;
                        //progress.IsIndeterminate = false;
                    }

                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }
    }
}
