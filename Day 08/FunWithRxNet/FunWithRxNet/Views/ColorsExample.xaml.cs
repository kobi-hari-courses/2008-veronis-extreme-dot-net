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
    /// Interaction logic for ColorsExample.xaml
    /// </summary>
    public partial class ColorsExample : UserControl
    {
        public ColorsExample()
        {
            InitializeComponent();
            var o_search = txtSearch.ObserveTextChanged();
            var o_results = o_search
                .Throttle(TimeSpan.FromSeconds(1))
                .Select(keyword => ColorsService.Instance.Search(keyword))
                .Switch()
                .ObserveOnDispatcher();

            o_results.Subscribe(res => lstResults.ItemsSource = res);

        }
    }
}
