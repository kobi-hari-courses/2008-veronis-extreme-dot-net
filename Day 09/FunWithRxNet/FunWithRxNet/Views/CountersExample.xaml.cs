using System;
using System.Collections.Generic;
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

namespace FunWithRxNet.Views
{
    /// <summary>
    /// Interaction logic for CountersExample.xaml
    /// </summary>
    public partial class CountersExample : UserControl
    {
        public CountersExample()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            readerWrapper.Content = (readerWrapper.Content == null) ? new CounterReader() : null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            writerWrapper.Content = (writerWrapper.Content == null) ? new CounterWriter() : null;
        }
    }
}
