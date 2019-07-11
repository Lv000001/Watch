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
    /// CircelControl.xaml 的交互逻辑
    /// </summary>
    public partial class CircleControl : System.Windows.Controls.UserControl
    {
        public CircleControl()
        {
            InitializeComponent();
            WatchElement = new WatchElement();
            if (watchElement.HWElement == null)
            {
                watchElement.HWElement = new HWCircle();
            }

            cmbDataType.ItemsSource = ConstData.DataTypeDescriptions;
            cmbDataType.SelectedValuePath = "Value";
            cmbDataType.DisplayMemberPath = "Key";
            cmbDataType.SelectedIndex = 0;
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
                    if (watchElement.HWElement != null)
                    {
                        path.Text = ((HWCircle)watchElement.HWElement).Res_name;
                        int index = 0;
                        foreach (var item in ConstData.DataTypeDescriptions.Values)
                        {
                            if (item == ((HWCircle)watchElement.HWElement).Data_type)
                            {
                                break;
                            }
                            index++;
                        }
                        cmbDataType.SelectedIndex = index;
                    }
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
            HWCircle circel = watchElement.HWElement as HWCircle;
            switch (e.PropertyName)
            {
                case "X": circel.Drawable_x = watchElement.X; break;
                case "Y": circel.Drawable_y = watchElement.Y; break;
                case "Width": circel.Drawable_width = watchElement.Width; break;
                case "Height": circel.Drawable_height = watchElement.Height; break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog())
            {
                ((HWCircle)WatchElement.HWElement).Res_name = ofd.FileName;
                path.Text = ofd.FileName;
                if (WatchElement.DesignerControl != null)
                {
                    Image image = new Image();
                    BitmapImage bitmapImage = ImageHelper.GetImage(ofd.FileName);
                    image.Source = bitmapImage;
                    image.Width = bitmapImage.PixelWidth;
                    image.Height = bitmapImage.PixelHeight;
                    image.Stretch = Stretch.Fill;

                    WatchElement.DesignerControl.Width = image.Width;
                    WatchElement.DesignerControl.Height = image.Height;
                    WatchElement.DesignerControl.Content = image;
                    RefreshControl();
                }
            }
        }

        /// <summary>
        /// 订阅的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbDataType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((HWCircle)WatchElement.HWElement).Data_type = cmbDataType.SelectedValue.ToString();
            RefreshControl();
        }

        /// <summary>
        /// 值发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshControl();
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private void RefreshControl()
        {
            if (WatchElement.DesignerControl != null && WatchElement.DesignerControl.Content != null && WatchElement.DesignerControl.Content is Image image)
            {
                HWCircle circel = WatchElement.HWElement as HWCircle;
                int curValue = ConstData.Datas[circel.Data_type];
                ClockDialCreater.SetClip(image, new Point(circel.Drawable_x, circel.Drawable_y), new Size(circel.Drawable_width, circel.Drawable_height),
                    new Point(circel.Circle_x, circel.Circle_y), circel.Circle_r, circel.Line_width, circel.Arc_start,
                    circel.Arc_end, curValue);
            }
        }
    }
}
