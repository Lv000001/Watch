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
    /// ImageControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageControl : System.Windows.Controls.UserControl
    {
        public ImageControl()
        {
            InitializeComponent();
            WatchElement = new WatchElement();
            if (watchElement.HWElement == null)
            {
                watchElement.HWElement = new HWImage();
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
                    if (watchElement.HWElement != null)
                    {
                        path.Text = ((HWImage)watchElement.HWElement).Res_name;
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
            HWImage image = watchElement.HWElement as HWImage;
            switch (e.PropertyName)
            {
                case "X": image.X = watchElement.X; break;
                case "Y": image.Y = watchElement.Y; break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog())
            {
                ((HWImage)WatchElement.HWElement).Res_name = ofd.FileName;
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
                }
            }
        }
    }
}
