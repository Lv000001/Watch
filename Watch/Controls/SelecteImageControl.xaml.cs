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
    /// SelecteImageControl.xaml 的交互逻辑
    /// </summary>
    public partial class SelecteImageControl : System.Windows.Controls.UserControl
    {
        public SelecteImageControl()
        {
            InitializeComponent();
            WatchElement = new WatchElement();
            if (watchElement.HWElement == null)
            {
                watchElement.HWElement = new HWSelectImage();
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
                        for (int i = 0; i < 15; i++)
                        {
                            List<string> fileNames = ((HWSelectImage)watchElement.HWElement).Res_names;
                            if (fileNames.Count > i)
                            {
                                switch (i)
                                {
                                    case 0: path00.Text = fileNames[i]; break;
                                    case 1: path01.Text = fileNames[i]; break;
                                    case 2: path02.Text = fileNames[i]; break;
                                    case 3: path03.Text = fileNames[i]; break;
                                    case 4: path04.Text = fileNames[i]; break;
                                    case 5: path05.Text = fileNames[i]; break;
                                    case 6: path06.Text = fileNames[i]; break;
                                    case 7: path07.Text = fileNames[i]; break;
                                    case 8: path08.Text = fileNames[i]; break;
                                    case 9: path09.Text = fileNames[i]; break;
                                    case 10: path10.Text = fileNames[i]; break;
                                    case 11: path11.Text = fileNames[i]; break;
                                    case 12: path12.Text = fileNames[i]; break;
                                    case 13: path13.Text = fileNames[i]; break;
                                    case 14: path14.Text = fileNames[i]; break;
                                    default:
                                        break;
                                }
                            }
                        }
                        int index = 0;
                        foreach (var item in ConstData.DataTypeDescriptions.Values)
                        {
                            if (item == ((HWSelectImage)watchElement.HWElement).Data_type)
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
            HWSelectImage selecteImage = watchElement.HWElement as HWSelectImage;
            switch (e.PropertyName)
            {
                case "X": selecteImage.Drawable_x = watchElement.X; break;
                case "Y": selecteImage.Drawable_y = watchElement.Y; break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog())
            {
                int index = int.Parse(((System.Windows.Controls.Button)sender).Tag.ToString());
                switch (index)
                {
                    case 0: path00.Text = ofd.FileName; break;
                    case 1: path01.Text = ofd.FileName; break;
                    case 2: path02.Text = ofd.FileName; break;
                    case 3: path03.Text = ofd.FileName; break;
                    case 4: path04.Text = ofd.FileName; break;
                    case 5: path05.Text = ofd.FileName; break;
                    case 6: path06.Text = ofd.FileName; break;
                    case 7: path07.Text = ofd.FileName; break;
                    case 8: path08.Text = ofd.FileName; break;
                    case 9: path09.Text = ofd.FileName; break;
                    case 10: path10.Text = ofd.FileName; break;
                    case 11: path11.Text = ofd.FileName; break;
                    case 12: path12.Text = ofd.FileName; break;
                    case 13: path13.Text = ofd.FileName; break;
                    case 14: path14.Text = ofd.FileName; break;
                    default:
                        break;
                }
                
                if (((HWSelectImage)WatchElement.HWElement).Res_names.Count > index)
                {
                    ((HWSelectImage)WatchElement.HWElement).Res_names[index] = ofd.FileName;
                }                

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

        /// <summary>
        /// 订阅的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbDataType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((HWSelectImage)WatchElement.HWElement).Data_type = cmbDataType.SelectedValue.ToString();
            RefreshControl();
         }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private void RefreshControl()
        {
            if (WatchElement != null && WatchElement.HWElement != null && WatchElement.HWElement is HWSelectImage selecteImage)
            {
                int value = ConstData.Datas[selecteImage.Data_type];
                if (value < 0 || value > 15 || value >= selecteImage.Res_names.Count)
                {
                    value = 0;
                }
                Image image = new Image();
                BitmapImage bitmapImage = ImageHelper.GetImage(selecteImage.Res_names[value]);
                if (bitmapImage == null)
                {
                    return ;
                }
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
