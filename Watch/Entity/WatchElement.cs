using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Watch.Element;

namespace Watch
{
    /// <summary>
    /// 表盘元素
    /// </summary>
    public class WatchElement : INotifyPropertyChanged
    {
        /// <summary>
        /// 唯一标识该元素
        /// </summary>
        public string ID { get; set; }

        private int width;
        private int height;
        private int x;
        private int y;

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                if (contetControl != null)
                {
                    contetControl.SizeChanged -= ContetControl_SizeChanged;
                    contetControl.Width = value;
                    contetControl.SizeChanged += ContetControl_SizeChanged;
                }
                NotifyPropertyChanged("Width");
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                if (contetControl != null)
                {
                    contetControl.SizeChanged -= ContetControl_SizeChanged;
                    contetControl.Height = value;
                    contetControl.SizeChanged += ContetControl_SizeChanged;
                }
                NotifyPropertyChanged("Height");
            }
        }

        /// <summary>
        /// x坐标
        /// </summary>
        public int X
        {
            set
            {
                if (x != value)
                {
                    x = value;
                    if (contetControl != null)
                    {
                        var descriptor = DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(ContentControl));
                        descriptor.RemoveValueChanged(contetControl, OnCanvasLeftChanged);
                        Canvas.SetLeft(contetControl, value);
                        descriptor.AddValueChanged(contetControl, OnCanvasLeftChanged);
                    }

                    NotifyPropertyChanged("X");
                }
            }
            get
            {
                return x;
            }
        }

        /// <summary>
        /// y坐标
        /// </summary>
        public int Y
        {
            set
            {
                if (y != value)
                {
                    y = value;

                    if (contetControl != null)
                    {
                        var descriptor = DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(ContentControl));
                        descriptor.RemoveValueChanged(contetControl, OnCanvasLeftChanged);
                        Canvas.SetTop(contetControl, value); descriptor.AddValueChanged(contetControl, OnCanvasLeftChanged);
                    }

                    NotifyPropertyChanged("Y");
                }

            }
            get { return y; }
        }

        private HWElement hwElement;
        /// <summary>
        /// 元素信息
        /// </summary>
        public HWElement HWElement
        {
            set
            {
                hwElement = value;
                NotifyPropertyChanged("HWElement");
            }
            get { return hwElement; }
        }

        private ContentControl contetControl;
        /// <summary>
        /// 设计的时候界面上对应的控件
        /// </summary>
        public ContentControl DesignerControl
        {
            get
            { return contetControl; }
            set
            {
                if (contetControl != null)
                {
                    var descriptor = DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(ContentControl));
                    descriptor.RemoveValueChanged(contetControl, OnCanvasLeftChanged);
                    contetControl.SizeChanged += ContetControl_SizeChanged;
                }

                contetControl = value;
                if (contetControl != null)
                {
                    X = (int)Canvas.GetLeft(DesignerControl);
                    Y = (int)Canvas.GetTop(DesignerControl);
                    var descriptor = DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(ContentControl));
                    descriptor.AddValueChanged(contetControl, OnCanvasLeftChanged);
                    contetControl.SizeChanged += ContetControl_SizeChanged;
                }
            }
        }

        private void ContetControl_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Width = (int)DesignerControl.Width;
            Height = (int)DesignerControl.Height;
        }

        private void OnCanvasLeftChanged(object sender, EventArgs e)
        {
            X = (int)Canvas.GetLeft(DesignerControl);
            Y = (int)Canvas.GetTop(DesignerControl);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
