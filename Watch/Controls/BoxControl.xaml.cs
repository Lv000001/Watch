using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Watch.Element;

namespace Watch.Controls
{
    /// <summary>
    /// BoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class BoxControl : System.Windows.Controls.UserControl
    {
        public BoxControl()
        {
            InitializeComponent();
            WatchElement= new WatchElement();
            if (watchElement.HWElement == null)
            {
                watchElement.HWElement = new HWBox();
            }

            this.DataContext = watchElement;
        }

        private WatchElement watchElement;
        public WatchElement WatchElement
        {
            get { return watchElement; }
            set
            {
                if (watchElement != null)
                {
                    watchElement.PropertyChanged -= WatchElement_PropertyChanged;
                }
                if (value != null)
                {
                    watchElement = value;                   
                    this.DataContext = watchElement;
                    watchElement.PropertyChanged += WatchElement_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// 元素属性发生变化后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WatchElement_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HWBox box = watchElement.HWElement as HWBox;
            switch (e.PropertyName)
            {
                case "X": box.Drawable_x = watchElement.X; break;
                case "Y": box.Drawable_y = watchElement.Y; break;
                case "Width": box.Drawable_width = watchElement.Width; break;
                case "Height": box.Drawable_height = watchElement.Height; break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            if (DialogResult.OK == cd.ShowDialog())
            {
                // gridColor.Background = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
                ((HWBox)WatchElement.HWElement).Color_red = cd.Color.R;
                ((HWBox)WatchElement.HWElement).Color_green = cd.Color.G;
                ((HWBox)WatchElement.HWElement).Color_blue = cd.Color.B;

                WatchElement.NotifyPropertyChanged("HWElement");
                if (WatchElement.DesignerControl != null)
                {
                    ((Rectangle)WatchElement.DesignerControl.Content).Fill = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
                }
            }
        }
    }
}
