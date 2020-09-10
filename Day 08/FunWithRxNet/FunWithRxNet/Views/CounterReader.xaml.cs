using FunWithRxNet.Services;
using FunWithRxNet.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
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

namespace FunWithRxNet.Views
{
    /// <summary>
    /// Interaction logic for CounterReader.xaml
    /// </summary>
    public partial class CounterReader : UserControl
    {
        public CounterReader()
        {
            InitializeComponent();

            CounterService.Instance
                .GetCounter()
                .TakeUntil(this.ObserveUnloaded())
                .Subscribe(val => {
                    counterLabel.Text = val.ToString();
                    Debug.WriteLine($"Counter.OnNext({val})");
                    });
        }
    }
}
