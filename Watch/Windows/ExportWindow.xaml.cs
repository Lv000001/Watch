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
using System.Windows.Shapes;
using Watch.Entity;

namespace Watch.Windows
{
    /// <summary>
    /// ExportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExportWindow : Window
    {
        public ExportWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表盘描述信息
        /// </summary>
        private WatchDescription watch = new WatchDescription();

        /// <summary>
        /// 表盘描述信息
        /// </summary>
        public WatchDescription WatchDescription
        {
            get { return watch; }
            set
            {
                if (value != null)
                {
                    watch = value;
                    this.DataContext = watch;
                }
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
