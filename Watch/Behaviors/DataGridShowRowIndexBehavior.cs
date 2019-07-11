using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Watch
{
    public class DataGridShowRowIndexBehavior
    {
        public static bool GetShwoRowIndex(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowRowIndexProperty);
        }

        public static void SetShowRowIndex(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowRowIndexProperty, value);
        }

        public static readonly DependencyProperty ShowRowIndexProperty = DependencyProperty.RegisterAttached("ShowRowIndex", typeof(bool), typeof(DataGridShowRowIndexBehavior), new UIPropertyMetadata(false, ShowRowIndexPropertyChanged));

        private static void ShowRowIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid == null) return;
            dataGrid.LoadingRow +=
                delegate (object sender, DataGridRowEventArgs e1)
                {
                    e1.Row.Header = e1.Row.GetIndex() + 1;
                };
        }

    }
}
