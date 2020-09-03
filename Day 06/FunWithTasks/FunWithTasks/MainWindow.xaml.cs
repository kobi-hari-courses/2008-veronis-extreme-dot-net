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
        private class UserRangeResolver : IRangeResolver
        {
            private MainWindow _host;
            private TaskCompletionSource<(int start, int finish)> _tcs = null;

            public Task<(int start, int finish)> GetRange(CancellationToken ct)
            {
                if (_tcs != null) return _tcs.Task;
                _host.panelRange.Visibility = Visibility.Visible;
                _tcs = new TaskCompletionSource<(int start, int finish)>();

                ct.Register(() => {
                    _tcs?.TrySetCanceled();
                    _cleanup();
                });


                return _tcs.Task;
            }

            private void _cleanup()
            {
                _host.panelRange.Visibility = Visibility.Collapsed;
                _tcs = null;
            }

            internal void Resolve()
            {
                if (_tcs != null)
                {
                    var start = int.Parse(_host.txtFrom.Text);
                    var finish = int.Parse(_host.txtTo.Text);

                    _tcs?.TrySetResult((start, finish));
                    _cleanup();
                }
            }

            public UserRangeResolver(MainWindow host)
            {
                _host = host;
            }
        }

        private CancellationTokenSource _cts = null;
        private UserRangeResolver _currentRangeResolver = null;

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
            Task<List<int>[]> taskAll = null;
            //List<Task<List<int>>> tasks = null;
            var rangeReslver = new UserRangeResolver(this);
            _currentRangeResolver = rangeReslver;

            try
            {
                _cts = new CancellationTokenSource();
                var progress = new Progress<int>(val => progressBar.Value = val);
                var progress2 = new Progress<int>(val => progressBar2.Value = val);

                Debug.WriteLine("1");
                txtStatus.Foreground = Brushes.Black;
                txtStatus.Text = "Calculating...";
                btnGo.IsEnabled = false;
                btnCancel.IsEnabled = true;
                progressBar.Value = 0;
                progressBar2.Value = 0;

                Debug.WriteLine("2");

                var range = await rangeReslver.GetRange(_cts.Token);
                var middle = (int)((range.start + range.finish) / 2);

                var task1 = PrimesCalculator.GetAllPrimesAsync(range.start, middle, _cts.Token, progress);
                var task2 = PrimesCalculator.GetAllPrimesAsync(middle + 1, range.finish, _cts.Token, progress2);

                //tasks = new List<Task<List<int>>> { task1, task2 };

                //var taskAny = Task.WhenAny(tasks);
                //var firstTask = await taskAny;
                //tasks.Remove(firstTask);

                taskAll = Task.WhenAll(task1, task2);
                var resultAll = await taskAll;

                //var result1 = await task1;
                //var result2 = await task2;

                var results = resultAll.SelectMany(list => list).ToList();

                Debug.WriteLine("3");

                lstResults.ItemsSource = results;
                txtStatus.Foreground = Brushes.Green;
                txtStatus.Text = "Completed";

                Debug.WriteLine("4");

            }
            catch (AccessViolationException)
            {
                //var exception = taskAll.Exception;
                //var exceptions = exception.Flatten();

                lstResults.ItemsSource = null;
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Text = "Randomly Failed on access violation...";
            } 
            catch (OperationCanceledException)
            {
                var exception = taskAll?.Exception;
                lstResults.ItemsSource = null;
                txtStatus.Foreground = Brushes.Orange;
                txtStatus.Text = "Operation Cancelled By User";
            }
            finally
            {
                btnGo.IsEnabled = true;
                btnCancel.IsEnabled = false;
                progressBar.Value = 100;
                progressBar2.Value = 100;
                _cts = null;
                //await Task.WhenAll(tasks);
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

        private void btnRangeOk_Click(object sender, RoutedEventArgs e)
        {
            _currentRangeResolver?.Resolve();
        }
    }
}
