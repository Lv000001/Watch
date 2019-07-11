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
    /// TextareaWithTwoWildCardControl.xaml 的交互逻辑
    /// </summary>
    public partial class TextareaWithTwoWildCardControl : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// 分辨率  0：454*454  1：390*390
        /// </summary>
        public int Resolution { set; get; } = 0;

        public TextareaWithTwoWildCardControl()
        {
            InitializeComponent();

            WatchElement = new WatchElement();
            if (watchElement.HWElement == null)
            {
                watchElement.HWElement = new HWTextareaWithTwoWildCard();
            }
            this.DataContext = watchElement;

            cmbDataType.ItemsSource = ConstData.DataTypeDescriptions;
            cmbDataType.SelectedValuePath = "Value";
            cmbDataType.DisplayMemberPath = "Key";
            cmbDataType.SelectedIndex = 0;

            cmbConnect.ItemsSource = ConstData.Connector;
            cmbConnect.SelectedValuePath = "Value";
            cmbConnect.DisplayMemberPath = "Key";
            cmbConnect.SelectedIndex = 0;

            cmbDataType2.ItemsSource = ConstData.DataTypeDescriptions;
            cmbDataType2.SelectedValuePath = "Value";
            cmbDataType2.DisplayMemberPath = "Key";
            cmbDataType2.SelectedIndex = 0;

            cmbFont.ItemsSource = ConstData.Fonts;
            cmbFont.SelectedIndex = 0;

            cmbAlignment.ItemsSource = ConstData.Alignments;
            cmbAlignment.SelectedValuePath = "Value";
            cmbAlignment.DisplayMemberPath = "Key";
            cmbAlignment.SelectedIndex = 0;
        }

        public TextareaWithTwoWildCardControl(int resolution) : this()
        {
            Resolution = resolution;
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
                        int index = 0;
                        foreach (var item in ConstData.DataTypeDescriptions.Values)
                        {
                            if (item == ((HWTextareaWithTwoWildCard)watchElement.HWElement).Data_type)
                            {
                                break;
                            }
                            index++;
                        }
                        cmbDataType.SelectedIndex = index;

                        index = 0;
                        foreach (var item in ConstData.Alignments.Values)
                        {
                            if (item == ((HWTextareaWithTwoWildCard)watchElement.HWElement).Alignment_type)
                            {
                                break;
                            }
                            index++;
                        }
                        cmbAlignment.SelectedIndex = index;

                        index = 0;
                        foreach (var item in ConstData.Alignments.Values)
                        {
                            if (item == ((HWTextareaWithTwoWildCard)watchElement.HWElement).Data_connector_type)
                            {
                                break;
                            }
                            index++;
                        }
                        cmbConnect.SelectedIndex = index;

                        index = 0;
                        foreach (var item in ConstData.Alignments.Values)
                        {
                            if (item == ((HWTextareaWithTwoWildCard)watchElement.HWElement).Data2_type)
                            {
                                break;
                            }
                            index++;
                        }
                        cmbDataType2.SelectedIndex = index;

                        for (int i = 0; i < ConstData.Fonts.Count; i++)
                        {
                            if (((HWTextareaWithTwoWildCard)watchElement.HWElement).Font_type.StartsWith(ConstData.Fonts[i]))
                            {
                                cmbFont.SelectedIndex = i;
                                break;
                            }
                        }
                      
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
            HWTextareaWithTwoWildCard text = watchElement.HWElement as HWTextareaWithTwoWildCard;
            switch (e.PropertyName)
            {
                case "X": text.Drawable_x = watchElement.X; break;
                case "Y": text.Drawable_y = watchElement.Y; break;
                case "Width": text.Drawable_width = watchElement.Width; break;
                case "Height": text.Drawable_height = watchElement.Height; break;
            }
        }

        private void CmbFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string font = e.AddedItems[0].ToString();
                if (Resolution == 0 && ConstData.FontSize454.ContainsKey(font))
                {
                    cmbFontSize.ItemsSource = ConstData.FontSize454[font];
                }
                if (Resolution == 1 && ConstData.FontSize390.ContainsKey(font))
                {
                    cmbFontSize.ItemsSource = ConstData.FontSize390[font];
                }
                cmbFontSize.SelectedIndex = 0;
                RefreshControl();
            }
        }

        private void CmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string font = cmbFont.Text;
            if (e.AddedItems.Count > 0)
            {
                string fontSize = e.AddedItems[0].ToString();
                ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Font_type = font + "_" + fontSize;
                RefreshControl();
            }
        }

        private void CmbAlignment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Alignment_type = cmbAlignment.SelectedValue.ToString();
            RefreshControl();
        }

        /// <summary>
        /// 选择颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            if (DialogResult.OK == cd.ShowDialog())
            {
                // gridColor.Background = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
                ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Color_red = cd.Color.R;
                ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Color_green = cd.Color.G;
                ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Color_blue = cd.Color.B;

                WatchElement.NotifyPropertyChanged("HWElement");
                RefreshControl();

            }
        }

        private void CmbDataType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Data_type = cmbDataType.SelectedValue.ToString();
            RefreshControl();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            WatchElement.NotifyPropertyChanged("HWElement");
            RefreshControl();
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private void RefreshControl()
        {
            if (WatchElement.DesignerControl != null && WatchElement.DesignerControl.Content != null && WatchElement.DesignerControl.Content is TextBlock text)
            {
                var textareaWithTwoWildCard = WatchElement.HWElement as HWTextareaWithTwoWildCard;
                int value = ConstData.Datas[textareaWithTwoWildCard.Data_type];
                string connect = "";
                switch (textareaWithTwoWildCard.Data_connector_type)
                {
                    case "CONN_COLON": connect = ":"; break;       // 冒号	“:”
                    case "CONN_STUB": connect = "-"; break;         /// CONN_STUB       // 短线	“-”
                    case "CONN_SLASH": connect = "/"; break;         /// CONN_SLASH		// 斜线	“/”
                    case "CONN_BACKSLASH": connect = "\\"; break;         /// CONN_BACKSLASH	// 反斜线“\”
                    case "CONN_POINT": connect = "."; break;         /// CONN_POINT		// 点	“.”
                    case "CONN_PERCENT": connect = "%"; break;        /// CONN_PERCENT		// 百分号“%”
                    case "CONN_SPACE": connect = " "; break;           /// CONN_SPACE		// 空格	“ ”
                }
                if (textareaWithTwoWildCard.Data2_type == "DATA_NULL")
                {
                    text.Text = value + connect;
                }
                else
                {
                    int value2 = ConstData.Datas[textareaWithTwoWildCard.Data2_type];
                    text.Text = value + connect + value2;
                }
                text.Foreground = new SolidColorBrush(Color.FromArgb(textareaWithTwoWildCard.Alpha, textareaWithTwoWildCard.Color_red, textareaWithTwoWildCard.Color_green, textareaWithTwoWildCard.Color_blue));
                ClockDialCreater.SetAlignment(text, textareaWithTwoWildCard.Alignment_type);
                ClockDialCreater.SetFontSize(text, textareaWithTwoWildCard.Font_type);
            }
        }

        /// <summary>
        /// 两个数据之间连接字符串
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbConnect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Data_connector_type = cmbConnect.SelectedValue.ToString();
            RefreshControl();
        }
        /// <summary>
        /// 第二个订阅数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbDataType2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((HWTextareaWithTwoWildCard)WatchElement.HWElement).Data2_type = cmbDataType2.SelectedValue.ToString();
            RefreshControl();
        }
    }
}
