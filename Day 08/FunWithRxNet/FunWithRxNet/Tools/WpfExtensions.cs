using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FunWithRxNet.Tools
{
    public static class WpfExtensions
    {
        public static IObservable<Unit> ObserveClick(this Button source)
        {
            return Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => source.Click += h,
                h => source.Click -= h)
                .Select(ep => Unit.Default)
                .TakeUntil(source.ObserveUnloaded());
        }

        public static IObservable<Unit> ObserveUnloaded(this FrameworkElement source)
        {
            return Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => source.Unloaded += h,
                h => source.Unloaded -= h)
                .Select(ep => Unit.Default)
                .Take(1);
        }

    }
}
