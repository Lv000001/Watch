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
    /// TextureMapperControl.xaml 的交互逻辑
    /// </summary>
    public partial class TextureMapperControl : System.Windows.Controls.UserControl
    {
        public TextureMapperControl()
        {
            InitializeComponent();
            WatchElement = new WatchElement();
            if (watchElement.HWElement == null)
            {
                watchElement.HWElement = new HWTextureMapper();
            }

            cmbDataType.ItemsSource = ConstData.RatioDataTypeDescriptions;
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
                        path.Text = ((HWTextureMapper)watchElement.HWElement).Res_name;
                        int index = 0;
                        foreach (var item in ConstData.DataTypeDescriptions.Values)
                        {
                            if (item == ((HWTextureMapper)watchElement.HWElement).Data_type)
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
            HWTextureMapper textureMapper = watchElement.HWElement as HWTextureMapper;
            switch (e.PropertyName)
            {
                case "X": textureMapper.Drawable_x = watchElement.X; break;
                case "Y": textureMapper.Drawable_y = watchElement.Y; break;
                case "Width": textureMapper.Drawable_width = watchElement.Width; break;
                case "Height": textureMapper.Drawable_height = watchElement.Height; break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog())
            {
                ((HWTextureMapper)WatchElement.HWElement).Res_name = ofd.FileName;
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
            ((HWTextureMapper)WatchElement.HWElement).Data_type = cmbDataType.SelectedValue.ToString();
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
                HWTextureMapper textureMapper = WatchElement.HWElement as HWTextureMapper;
                BitmapImage bitmapImage = ImageHelper.GetImage(textureMapper.Res_name);
                image.Source = bitmapImage;
                image.Width = bitmapImage.PixelWidth;
                image.Height = bitmapImage.PixelHeight;
                image.Stretch = Stretch.Fill;

                int value = ConstData.Datas[textureMapper.Data_type];
                double angle = textureMapper.Begin_arc + (textureMapper.End_arc - textureMapper.Begin_arc) * value / 100.0;
                ClockDialCreater.SetRotateTransform(image, new Point(textureMapper.Rotation_center_x, textureMapper.Rotation_center_y), angle);
            }
        }
    }
}
