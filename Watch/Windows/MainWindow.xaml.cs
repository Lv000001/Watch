using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Watch.Common;
using Watch.Controls;
using Watch.Element;
using Watch.Entity;
using Watch.Windows;

namespace Watch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<DataTypeInfo> dataTypeInfos = new ObservableCollection<DataTypeInfo>();
        /// <summary>
        /// 分辨率  0：454*454  1：390*390
        /// </summary>
        private int resolution = 0;

        /// <summary>
        /// 当前打开的文件
        /// </summary>
        private string curOpenFile = "";

        /// <summary>
        /// 当前正在编辑的element
        /// </summary>
        ObservableCollection<WatchElement> watchElements = new ObservableCollection<WatchElement>();

        public MainWindow()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += MainWindow_DragEnter;
            Drop += MainWindow_Drop;
            KeyDown += MainWindow_KeyDown;
            this.Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;
            StateChanged += MainWindow_StateChanged;
            string[] dataTypeStrs = ConstData.DEFAULT_VALUE.Split('\n');

            for (int i = 0; i < dataTypeStrs.Length; i++)
            {
                string[] dataTypeInfo = dataTypeStrs[i].Split('\t');
                if (dataTypeInfo.Length == 4)
                {
                    DataTypeInfo info = new DataTypeInfo();
                    info.Name = dataTypeInfo[0];
                    info.DataType = dataTypeInfo[1];
                    info.Range = dataTypeInfo[2];
                    info.Value = int.Parse(dataTypeInfo[3]);
                    if (info.DataType.ToUpper().EndsWith("RATIO"))
                    {
                        ConstData.RatioDataTypeDescriptions.Add(info.Name, info.DataType);
                    }
                    else
                    {
                        ConstData.DataTypeDescriptions.Add(info.Name, info.DataType);
                    }

                    ConstData.Datas.Add(info.DataType, info.Value);
                    dataTypeInfos.Add(info);
                }
            }
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            MainWindow_SizeChanged(null, null);
        }

        /// <summary>
        /// 重新定位表带图片位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (resolution == 0)
            {
                canvasMain.Width = 454;
                canvasMain.Height = 454;
                imgBg.Width = 1200;
                imgBg.Height = 1200;

                Canvas.SetLeft(canvasMain, (canvasBg.ActualWidth - canvasMain.ActualWidth) / 2.0);
                Canvas.SetTop(canvasMain, (canvasBg.ActualHeight - canvasMain.ActualHeight) / 2.0);

                Canvas.SetLeft(imgBg, Canvas.GetLeft(canvasMain) - 366);
                Canvas.SetTop(imgBg, Canvas.GetTop(canvasMain) - 366);
            }
            else
            {
                canvasMain.Width = 390;
                canvasMain.Height = 390;
                imgBg.Width = 1031.84;
                imgBg.Height = 1031.84;

                Canvas.SetLeft(canvasMain, (canvasBg.ActualWidth - canvasMain.ActualWidth) / 2.0);
                Canvas.SetTop(canvasMain, (canvasBg.ActualHeight - canvasMain.ActualHeight) / 2.0);
                Canvas.SetLeft(imgBg, Canvas.GetLeft(canvasMain) - 309.25);
                Canvas.SetTop(imgBg, Canvas.GetTop(canvasMain) - 309.25);
            }


        }

        /// <summary>
        /// F5刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                BtnRefresh_Click(null, null);
            }
        }

        /// <summary>
        /// 加载结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            grid.DataContext = dataTypeInfos;
            gridElements.DataContext = watchElements;
        }

        /// <summary>
        /// 文件拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Drop(object sender, DragEventArgs e)
        {
            string fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            Load(fileName);
        }

        /// <summary>
        /// 拖拽文件进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {

                e.Effects = DragDropEffects.Link;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void Load(string filePath)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(curOpenFile) && !string.IsNullOrWhiteSpace(filePath))
                {
                    if (MessageBoxResult.OK != MessageBox.Show("存在未保存的项目，是否继续加载？", "警告", MessageBoxButton.OKCancel))
                    {
                        return;
                    }
                }

                canvasMain.Children.Clear();
                curOpenFile = filePath;
                string watchface = curOpenFile + "/watchface";
                string watch_face_config = watchface + "/watch_face_config.xml";
                string res = watchface + "/res/";

                ConfigReader reader = new ConfigReader(watch_face_config, res);
                watchElements.Clear();
                List<HWElement> elementList = reader.Read();
                radio454.IsChecked = (reader.Resolution == 0);

                ClockDialCreater creater = new ClockDialCreater(elementList, canvasMain);
                var watchs = creater.Creater();
                for (int i = 0; i < watchs.Count; i++)
                {
                    watchElements.Add(watchs[i]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Load(folderBrowserDialog.SelectedPath);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(curOpenFile))
            {
                for (int i = 0; i < dataTypeInfos.Count; i++)
                {
                    ConstData.Datas[dataTypeInfos[i].DataType] = dataTypeInfos[i].Value;
                }
            }
        }

        /// <summary>
        /// 切换分辨率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            resolution = 0;
            if (canvasMain != null)
            {
                MainWindow_SizeChanged(null, null);
            }
        }

        /// <summary>
        /// 切换分辨率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            resolution = 1;
            if (canvasMain != null)
            {
                MainWindow_SizeChanged(null, null);
            }
        }

        /// <summary>
        /// element选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridElements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = gridElements.SelectedValue;
            if (select != null)
            {
                WatchElement element = (WatchElement)select;
                foreach (UIElement item in canvasMain.Children)
                {
                    Selector.SetIsSelected(item, false);
                }
                if (element.DesignerControl.Visibility == Visibility.Visible)
                {
                    Selector.SetIsSelected(element.DesignerControl, true);
                }

                if (element.HWElement is HWImage)
                {
                    ImageControl image = new ImageControl();
                    image.WatchElement = element;
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(image);
                }
                else if (element.HWElement is HWBox)
                {
                    BoxControl boxControl = new BoxControl
                    {
                        WatchElement = element
                    };
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(boxControl);
                }
                else if (element.HWElement is HWLine)
                {
                    LineControl Line = new LineControl
                    {
                        WatchElement = element
                    };
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(Line);
                }
                else if (element.HWElement is HWCircle)
                {
                    CircleControl circelControl = new CircleControl
                    {
                        WatchElement = element
                    };
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(circelControl);
                }
                else if (element.HWElement is HWSelectImage)
                {
                    SelecteImageControl selecte = new SelecteImageControl
                    {
                        WatchElement = element
                    };
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(selecte);
                }
                else if (element.HWElement is HWTextareaWithOneWildCard)
                {
                    TextareaWithOneWildCardControl textCtrl = new TextareaWithOneWildCardControl
                    {
                        WatchElement = element
                    };
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(textCtrl);
                }
                else if (element.HWElement is HWTextareaWithTwoWildCard)
                {
                    TextareaWithTwoWildCardControl textCtrl = new TextareaWithTwoWildCardControl
                    {
                        WatchElement = element
                    };
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(textCtrl);
                }
                else if (element.HWElement is HWTextureMapper)
                {
                    TextureMapperControl texture = new TextureMapperControl
                    {
                        WatchElement = element
                    };
                    gridAttribute.Children.Clear();
                    gridAttribute.Children.Add(texture);
                }
                else
                {
                    gridAttribute.Children.Clear();
                }
            }
        }

        /// <summary>
        /// 改变编辑区域背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyInfo ss = (PropertyInfo)comboBox1.SelectedValue;
            gridBg.Background = new SolidColorBrush((Color)ss.GetValue(null));
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddElementWindow add = new AddElementWindow(resolution);
            if (add.ShowDialog() ?? false)
            {
                WatchElement element = ClockDialCreater.CreaterElement(add.HWElement, canvasMain, curOpenFile);
                watchElements.Add(element);
            }
        }

        /// <summary>
        /// 删除元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var select = gridElements.SelectedValue;
            if (select != null)
            {
                WatchElement element = (WatchElement)select;
                int curIndex = 0;
                for (int i = 0; i < watchElements.Count; i++)
                {
                    if (watchElements[i].ID == element.ID)
                    {
                        curIndex = i;
                        break;
                    }
                }
                watchElements.RemoveAt(curIndex);
                canvasMain.Children.RemoveAt(curIndex);

            }

        }

        /// <summary>
        /// 把元素向顶层移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            var select = gridElements.SelectedValue;
            if (select != null)
            {
                WatchElement element = (WatchElement)select;
                MoveZOrder(element, -1);
            }

        }

        /// <summary>
        /// 把元素向底层移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            var select = gridElements.SelectedValue;
            if (select != null)
            {
                WatchElement element = (WatchElement)select;
                MoveZOrder(element, 1);
            }
        }

        /// <summary>
        /// 改变元素的zOrder
        /// </summary>
        /// <param name="element"></param>
        /// <param name="zOrder"></param>
        private void MoveZOrder(WatchElement element, int zOrder)
        {
            int curIndex = 0;
            for (int i = 0; i < watchElements.Count; i++)
            {
                if (watchElements[i].ID == element.ID)
                {
                    curIndex = i;
                    break;
                }
            }

            if (curIndex == 0 && zOrder < 0 || curIndex == watchElements.Count - 1 && zOrder > 0)
            {
                return;
            }

            watchElements.RemoveAt(curIndex);
            watchElements.Insert(curIndex + zOrder, element);
            canvasMain.Children.RemoveAt(curIndex);
            canvasMain.Children.Insert(curIndex + zOrder, element.DesignerControl);
            gridElements.SelectedIndex = curIndex + zOrder;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            if (watchElements == null || watchElements.Count == 0)
            {
                MessageBox.Show("没有要导出的表盘元素");
                return;
            }

            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folder = folderBrowserDialog.SelectedPath;
                ExportWindow export = new ExportWindow();
                export.WatchDescription = new WatchDescription
                {
                    Screen = resolution == 0 ? "HWHD02" : "HWHD01",
                    Version = resolution == 0 ? "2.1.1" : "3.1.1"
                };
                if (export.ShowDialog() ?? false)
                {
                    ConfigWriter writer = new ConfigWriter(folder, watchElements, export.WatchDescription);
                    writer.Write();
                    string res = folder + "/watchface/res/";
                    SaveToImage(res + "A100_001.png");

                    SaveToPreviewImage(folder + "/preview/");
                    Process.Start(folder);
                }
            }
        }


        /// <summary>
        /// 生产缩略图
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveToImage(string fileName)
        {
            //  保存到特定路径
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {

                DrawingVisual tDrawingVisual = new DrawingVisual();
                using (DrawingContext context = tDrawingVisual.RenderOpen())
                {
                    VisualBrush tVisualBrush = new VisualBrush(canvasMain);
                    tVisualBrush.Stretch = Stretch.Fill;
                    context.DrawRectangle(tVisualBrush, null, new Rect(0, 0, 250, 250));
                    context.Close();
                }

                //对象转换成位图
                RenderTargetBitmap bmp = new RenderTargetBitmap(250, 250, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(tDrawingVisual);
                //对象的集合编码转成图像流
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                //保存到路径中
                encoder.Save(fs);
                //释放资源
                fs.Close();
                fs.Dispose();
            }

        }

        /// <summary>
        /// 生产缩略图
        /// </summary>
        /// <param name="folder"></param>
        private void SaveToPreviewImage(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            //  保存到特定路径
            using (FileStream fs = new FileStream(folder+ "cover.jpg", FileMode.Create))
            {
              //  cover  960 icon_small 390
                DrawingVisual tDrawingVisual = new DrawingVisual();
                using (DrawingContext context = tDrawingVisual.RenderOpen())
                {
                    VisualBrush tVisualBrush = new VisualBrush(canvasMain);
                    tVisualBrush.Stretch = Stretch.Fill;
                    context.DrawRectangle(tVisualBrush, null, new Rect(0, 0, 960, 960));
                    context.Close();
                }

                //对象转换成位图
                RenderTargetBitmap bmp = new RenderTargetBitmap(960, 960, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(tDrawingVisual);
                //对象的集合编码转成图像流
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                //保存到路径中
                encoder.Save(fs);
                //释放资源
                fs.Close();
                fs.Dispose();
            }

            using (FileStream fs = new FileStream(folder + "icon_small.jpg", FileMode.Create))
            {
                //  cover  960 icon_small 390
                DrawingVisual tDrawingVisual = new DrawingVisual();
                using (DrawingContext context = tDrawingVisual.RenderOpen())
                {
                    VisualBrush tVisualBrush = new VisualBrush(canvasMain);
                    tVisualBrush.Stretch = Stretch.Fill;
                    context.DrawRectangle(tVisualBrush, null, new Rect(0, 0, 390, 390));
                    context.Close();
                }

                //对象转换成位图
                RenderTargetBitmap bmp = new RenderTargetBitmap(390, 390, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(tDrawingVisual);
                //对象的集合编码转成图像流
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                //保存到路径中
                encoder.Save(fs);
                //释放资源
                fs.Close();
                fs.Dispose();
            }

        }
    }
}
