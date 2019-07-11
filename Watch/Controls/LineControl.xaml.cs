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
    /// LineControl.xaml 的交互逻辑
    /// </summary>
    public partial class LineControl : System.Windows.Controls.UserControl
    {
        public LineControl()
        {
            InitializeComponent();

            WatchElement = new WatchElement();
            if (watchElement.HWElement == null)
            {
                watchElement.HWElement = new HWLine();
            }
            this.DataContext = watchElement;
            cmbDataType.ItemsSource = ConstData.RatioDataTypeDescriptions;
            cmbDataType.SelectedValuePath = "Value";
            cmbDataType.DisplayMemberPath = "Key";
            cmbDataType.SelectedIndex = 0;
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
                    if (watchElement.HWElement!=null)
                    {
                        path.Text = ((HWLine)watchElement.HWElement).Res_name;
                        int index = 0;
                        foreach (var item in ConstData.RatioDataTypeDescriptions.Values)
                        {
                            if (item == ((HWLine)watchElement.HWElement).Data_type)
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
            HWLine line = watchElement.HWElement as HWLine;
            switch (e.PropertyName)
            {
                case "X": line.Drawable_x = watchElement.X; break;
                case "Y": line.Drawable_y = watchElement.Y; break;
                case "Width": line.Drawable_width = watchElement.Width; break;
                case "Height": line.Drawable_height = watchElement.Height; break;
            }
        }

        /// <summary>
        /// 打开图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog())
            {
                ((HWLine)WatchElement.HWElement).Res_name = ofd.FileName;
                path.Text = ofd.FileName;
                if (WatchElement.DesignerControl != null)
                {
                    Image image = new Image();
                    BitmapImage bitmapImage = ImageHelper.GetImage(ofd.FileName);
                    image.Source = bitmapImage;
                    image.Width = bitmapImage.PixelWidth;
                    image.Height = bitmapImage.PixelHeight;
                    image.Stretch = Stretch.Fill;

                    WatchElement.DesignerControl.Content = image;
                }
            }
        }

        /// <summary>
        /// 数据改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbDataType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDataType.SelectedValue != null && watchElement.HWElement != null)
            {
                HWLine line = watchElement.HWElement as HWLine;
                line.Data_type = cmbDataType.SelectedValue.ToString();

                if (watchElement.DesignerControl != null)
                {
                    int value = ConstData.Datas[line.Data_type];
                    ClockDialCreater.SetRectClip(((Image)watchElement.DesignerControl.Content), new Point(line.Drawable_x, line.Drawable_y), new Size(line.Drawable_width, line.Drawable_height), value);
                }
            }
        }
    }
}
