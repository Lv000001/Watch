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
using Watch.Controls;
using Watch.Element;

namespace Watch
{
    /// <summary>
    /// AddElementWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddElementWindow : Window
    {
        /// <summary>
        /// 分辨率  0：454*454  1：390*390
        /// </summary>
        public int Resolution { set; get; } = 0;

        public AddElementWindow()
        {
            InitializeComponent();
        }

        public AddElementWindow(int resolution):this()
        {
            Resolution = resolution;
        }

        /// <summary>
        /// 元素
        /// </summary>
        public HWElement HWElement { set; get; }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridCtrl != null)
            {
                gridCtrl.Children?.Clear();
                switch (listType.SelectedIndex)
                {
                    case 0: gridCtrl.Children.Add(new BoxControl()); break;
                    case 1: gridCtrl.Children.Add(new CircleControl()); break;
                    case 2: gridCtrl.Children.Add(new ImageControl()); break;
                    case 3: gridCtrl.Children.Add(new LineControl()); break;
                    case 4: gridCtrl.Children.Add(new SelecteImageControl()); break;
                    case 5: gridCtrl.Children.Add(new TextareaWithOneWildCardControl(Resolution)); break;
                    case 6: gridCtrl.Children.Add(new TextareaWithTwoWildCardControl(Resolution)); break;
                    case 7: gridCtrl.Children.Add(new TextureMapperControl()); break;
                }
            }

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (gridCtrl.Children != null)
            {
                switch (listType.SelectedIndex)
                {
                    case 0:
                        HWElement = ((BoxControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                    case 1:
                        HWElement = ((CircleControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                    case 2:
                        HWElement = ((ImageControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                    case 3:
                        HWElement = ((LineControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                    case 4:
                        HWElement = ((SelecteImageControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                    case 5:
                        HWElement = ((TextareaWithOneWildCardControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                    case 6:
                        HWElement = ((TextareaWithTwoWildCardControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                    case 7:
                        HWElement = ((TextureMapperControl)gridCtrl.Children[0]).WatchElement.HWElement; break;
                }

                DialogResult = true;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
