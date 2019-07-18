using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Watch.Adorners;
using Watch.Element;

namespace Watch
{
    public class ClockDialCreater
    {
        /// <summary>
        /// 表盘元素
        /// </summary>
        private List<HWElement> hWElements;

        /// <summary>
        /// 画板，用来展示表盘
        /// </summary>
        private Canvas canvas;

        public ClockDialCreater(List<HWElement> hWElements, Canvas canvas)
        {
            this.hWElements = hWElements;
            this.canvas = canvas;
        }

        /// <summary>
        /// 创建
        /// </summary>
        public ObservableCollection<WatchElement> Creater()
        {
            ObservableCollection<WatchElement> watchs = new ObservableCollection<WatchElement>();
            ContentControl control = null;
            foreach (var element in hWElements)
            {
                if (element is HWBox box)
                {
                    control = AddBox(box);
                }

                if (element is HWCircle circel)
                {
                    control = AddCircle(circel);
                }

                if (element is HWImage image)
                {
                    control = AddImage(image);
                }

                if (element is HWLine line)
                {
                    control = AddLine(line);
                }

                if (element is HWSelectImage selecteImage)
                {
                    control = AddSelectImage(selecteImage);
                }

                if (element is HWTextareaWithOneWildCard textareaWithOneWildCard)
                {
                    control = AddTextareaWithOneWildCard(textareaWithOneWildCard);
                }

                if (element is HWTextareaWithTwoWildCard textareaWithTwoWildCard)
                {
                    control = AddTextareaWithTwoWildCarde(textareaWithTwoWildCard);
                }

                if (element is HWTextureMapper textureMapper)
                {
                    control = AddTextureMapper(textureMapper);
                }
                if (control != null)
                {
                    watchs.Add(new WatchElement { HWElement = element, DesignerControl = control, ID = Guid.NewGuid().ToString() });
                }
            }
            return watchs;
        }

        /// <summary>
        /// 创建一个元素
        /// </summary>
        /// <param name="hWElement"></param>
        /// <param name="canvas"></param>
        /// <param name="resPath"></param>
        /// <returns></returns>
        public static WatchElement CreaterElement(HWElement hWElement, Canvas canvas, string resPath)
        {
            ClockDialCreater clockDialCreater = new ClockDialCreater(new List<HWElement> { hWElement }, canvas);
            ObservableCollection<WatchElement> watchs = clockDialCreater.Creater();
            return (watchs == null || watchs.Count == 0) ? null : watchs[0];
        }

        private ContentControl AddBox(HWBox box)
        {
            Rectangle rect = new Rectangle()
            {
                Fill = new SolidColorBrush(Color.FromRgb(box.Color_red, box.Color_green, box.Color_blue)),
                StrokeThickness = 0
            };

            ContentControl control = new ContentControl
            {
                Width = box.Drawable_width,
                Height = box.Drawable_height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };

            control.Content = rect;
            canvas.Children.Add(control);

            Canvas.SetLeft(control, box.Drawable_x);
            Canvas.SetTop(control, box.Drawable_y);
            return control;
        }

        private ContentControl AddCircle(HWCircle circel)
        {
            Image image = new Image();
            BitmapImage bitmapImage = ImageHelper.GetImage(circel.Res_name);
            image.Source = bitmapImage;
            image.Width = bitmapImage.PixelWidth;
            image.Height = bitmapImage.PixelHeight;
            image.Stretch = Stretch.Fill;

            ContentControl control = new ContentControl
            {
                Width = image.Width,
                Height = image.Height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };

            control.Content = image;
            canvas.Children.Add(control);

            Canvas.SetLeft(control, circel.Drawable_x);
            Canvas.SetTop(control, circel.Drawable_y);


            int curValue = ConstData.Datas[circel.Data_type];
            SetClip(image, new Point(circel.Drawable_x, circel.Drawable_y), new Size(circel.Drawable_width, circel.Drawable_height),
                new Point(circel.Circle_x, circel.Circle_y), circel.Circle_r, circel.Line_width, circel.Arc_start,
                circel.Arc_end, curValue);
            return control;

        }

        private ContentControl AddImage(HWImage img)
        {
            Image image = new Image();
            BitmapImage bitmapImage = ImageHelper.GetImage(img.Res_name);
            image.Source = bitmapImage;
            image.Width = bitmapImage.PixelWidth;
            image.Height = bitmapImage.PixelHeight;
            image.Stretch = Stretch.Fill;

            ContentControl control = new ContentControl
            {
                Width = image.Width,
                Height = image.Height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };

            control.Content = image;
            canvas.Children.Add(control);
            DesignerItemDecorator.SetCanResize(control, false);

            Canvas.SetTop(control, img.Y);
            Canvas.SetLeft(control, img.X);
            return control;
        }

        private ContentControl AddLine(HWLine line)
        {
            Image image = new Image();
            BitmapImage bitmapImage = ImageHelper.GetImage(line.Res_name);
            image.Source = bitmapImage;
            image.Width = bitmapImage.PixelWidth;
            image.Height = bitmapImage.PixelHeight;
            image.Stretch = Stretch.Fill;
            ContentControl control = new ContentControl
            {
                Width = image.Width,
                Height = image.Height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };

            control.Content = image;
            canvas.Children.Add(control);
            Canvas.SetTop(control, line.Drawable_y);
            Canvas.SetLeft(control, line.Drawable_x);

            int value = ConstData.Datas[line.Data_type];
            SetRectClip(image, new Point(line.Drawable_x, line.Drawable_y), new Size(line.Drawable_width, line.Drawable_height), value);
            return control;
        }

        private ContentControl AddSelectImage(HWSelectImage selecteImage)
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
                return null;
            }
            image.Source = bitmapImage;
            image.Width = bitmapImage.PixelWidth;
            image.Height = bitmapImage.PixelHeight;
            image.Stretch = Stretch.Fill;

            ContentControl control = new ContentControl
            {
                Width = image.Width,
                Height = image.Height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };
            control.Content = image;
            canvas.Children.Add(control);
            Canvas.SetLeft(control, selecteImage.Drawable_x);
            Canvas.SetTop(control, selecteImage.Drawable_y);
            return control;
        }

        private ContentControl AddTextareaWithOneWildCard(HWTextareaWithOneWildCard textareaWithOneWildCard)
        {
            TextBlock text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Stretch;
            int value = ConstData.Datas[textareaWithOneWildCard.Data_type];
            text.Text = value.ToString();
            text.Foreground = new SolidColorBrush(Color.FromArgb(textareaWithOneWildCard.Alpha,
                textareaWithOneWildCard.Color_red, textareaWithOneWildCard.Color_green, textareaWithOneWildCard.Color_blue));
            SetAlignment(text, textareaWithOneWildCard.Alignment_type);
            SetFontSize(text, textareaWithOneWildCard.Font_type);

            ContentControl control = new ContentControl
            {
                Width = textareaWithOneWildCard.Drawable_width,
                Height = textareaWithOneWildCard.Drawable_height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };
            control.Content = text;
            canvas.Children.Add(control);
            Canvas.SetLeft(control, textareaWithOneWildCard.Drawable_x);
            Canvas.SetTop(control, textareaWithOneWildCard.Drawable_y);
            return control;
        }

        private ContentControl AddTextareaWithTwoWildCarde(HWTextareaWithTwoWildCard textareaWithTwoWildCard)
        {
            TextBlock text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Stretch;

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
            SetAlignment(text, textareaWithTwoWildCard.Alignment_type);
            SetFontSize(text, textareaWithTwoWildCard.Font_type);
            text.Foreground = new SolidColorBrush(Color.FromArgb(textareaWithTwoWildCard.Alpha,
               textareaWithTwoWildCard.Color_red, textareaWithTwoWildCard.Color_green, textareaWithTwoWildCard.Color_blue));
            ContentControl control = new ContentControl
            {
                Width = textareaWithTwoWildCard.Drawable_width,
                Height = textareaWithTwoWildCard.Drawable_height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };
            control.Content = text;
            canvas.Children.Add(control);

            Canvas.SetLeft(control, textareaWithTwoWildCard.Drawable_x);
            Canvas.SetTop(control, textareaWithTwoWildCard.Drawable_y);
            return control;
        }

        private ContentControl AddTextureMapper(HWTextureMapper textureMapper)
        {
            Image image = new Image();
            BitmapImage bitmapImage = ImageHelper.GetImage(textureMapper.Res_name);
            if (bitmapImage == null)
            {
                return null;
            }
            image.Source = bitmapImage;
            image.Width = bitmapImage.PixelWidth;
            image.Height = bitmapImage.PixelHeight;
            image.Stretch = Stretch.Fill;

            ContentControl control = new ContentControl
            {
                Width = textureMapper.Drawable_width,
                Height = textureMapper.Drawable_height,
                Style = (Style)Application.Current.Resources["DesignerItemStyle"]
            };
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            image.Margin = new Thickness((textureMapper.Drawable_width - textureMapper.Rotation_center_x * 2) / 2.0, (textureMapper.Drawable_height - textureMapper.Rotation_center_y * 2.0) / 2, 0, 0);
            control.Content = image;
            canvas.Children.Add(control);
            Canvas.SetLeft(control, textureMapper.Drawable_x);
            Canvas.SetTop(control, textureMapper.Drawable_y);
            int value = ConstData.Datas[textureMapper.Data_type];
            double angle = textureMapper.Begin_arc + (textureMapper.End_arc - textureMapper.Begin_arc) * value / 100.0;
            SetRotateTransform(image, new Point(textureMapper.Rotation_center_x, textureMapper.Rotation_center_y), angle);
            return control;
        }

        /// <summary>
        /// 设置Circle的显示
        /// </summary>
        /// <param name="image">图片控件</param>
        /// <param name="point">显示位置</param>
        /// <param name="size">显示尺寸</param>
        /// <param name="center">圆心(相对于显示位置的坐标)</param>
        /// <param name="radius">半径</param>
        /// <param name="lineWidth">显示宽度</param>
        /// <param name="startAngle">开始角度</param>
        /// <param name="endAngle">结束角度</param>
        /// <param name="curValue">当前值(0~100%)</param>
        public static void SetClip(Image image, Point point, Size size, Point center, int radius, int lineWidth, int startAngle, int endAngle, int curValue)
        {
            if (lineWidth > radius)
            {
                return;
            }
            PathGeometry pathGeometry = new PathGeometry();
            double curAngle = startAngle + (endAngle - startAngle) * curValue / 100.0;
            ArcSegment arcBig = new ArcSegment();
            ArcSegment arcSmall = new ArcSegment();
            arcBig.Size = new Size(radius + lineWidth / 2, radius + lineWidth / 2);
            arcSmall.Size = new Size(radius - lineWidth / 2, radius - lineWidth / 2);
            // 圆心
            Point centerPoint = new Point(center.X, center.Y);
            Point smallArcStartPoint = new Point(centerPoint.X + arcSmall.Size.Width * Math.Sin(startAngle * Math.PI / 180), centerPoint.Y - arcSmall.Size.Height * Math.Cos(startAngle * Math.PI / 180));
            Point smallArcEndPoint = new Point(centerPoint.X + arcSmall.Size.Width * Math.Sin(curAngle * Math.PI / 180), centerPoint.Y - arcSmall.Size.Height * Math.Cos(curAngle * Math.PI / 180));
            Point bigArcStartPoint = new Point(centerPoint.X + arcBig.Size.Width * Math.Sin(startAngle * Math.PI / 180), centerPoint.Y - arcBig.Size.Height * Math.Cos(startAngle * Math.PI / 180));
            Point bigArcEndPoint = new Point(centerPoint.X + arcBig.Size.Width * Math.Sin(curAngle * Math.PI / 180), centerPoint.Y - arcBig.Size.Height * Math.Cos(curAngle * Math.PI / 180));


            arcSmall.IsLargeArc = (Math.Abs(startAngle - curAngle) > 180);
            arcBig.IsLargeArc = (Math.Abs(startAngle - curAngle) > 180);

            arcBig.Point = bigArcEndPoint;
            arcSmall.Point = smallArcStartPoint;

            arcBig.SweepDirection = (startAngle > curAngle) ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;
            arcSmall.SweepDirection = (startAngle < curAngle) ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = smallArcStartPoint;
            pathFigure.Segments.Add(new LineSegment(bigArcStartPoint, false));
            pathFigure.Segments.Add(arcBig);
            pathFigure.Segments.Add(new LineSegment(smallArcEndPoint, false));
            pathFigure.Segments.Add(arcSmall);
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            image.Clip = pathGeometry;
        }

        /// <summary>
        /// 设置line控件显示的长度
        /// </summary>
        /// <param name="image"></param>
        /// <param name="point"></param>
        /// <param name="size"></param>
        /// <param name="curValue"></param>
        public static void SetRectClip(Image image, Point point, Size size, int curValue)
        {
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            Point rightTop = new Point(size.Width * curValue / 100.0, 0);
            Point rightBottom = new Point(size.Width * curValue / 100.0, size.Height);
            Point leftBottom = new Point(0, size.Height);
            pathFigure.StartPoint = new Point();
            pathFigure.Segments.Add(new LineSegment(rightTop, false));
            pathFigure.Segments.Add(new LineSegment(rightBottom, false));
            pathFigure.Segments.Add(new LineSegment(leftBottom, false));
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            image.Clip = pathGeometry;
        }

        /// <summary>
        /// 图片旋转
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="center">图片的中心点</param>
        /// <param name="angle">旋转角度</param>
        public static void SetRotateTransform(Image image, Point center, double angle)
        {
            RotateTransform rotateTransform = new RotateTransform(angle);
            rotateTransform.CenterX = center.X;
            rotateTransform.CenterY = center.Y;
            image.RenderTransform = rotateTransform;//图片控件旋转
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontName"></param>
        public static void SetFontSize(TextBlock text, string fontName)
        {
            string[] strs = fontName.Split('_');
            if (double.TryParse(strs[strs.Length - 1], out double size))
            {
                text.FontSize = size;
            }

            if (fontName.ToUpper().StartsWith("F_DINCONDENSED_BOLD"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_DINCONDENSED_BOLD"]);
            }

            if (fontName.ToUpper().StartsWith("F_DINNEXTFORHUAWEI_BOLD"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_DINNEXTFORHUAWEI_BOLD"]);
            }

            if (fontName.ToUpper().StartsWith("F_DIN_BLACKITALIC"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_DIN_BLACKITALIC"]);
            }

            if (fontName.ToUpper().StartsWith("F_DIN_BLACKALTERNATE"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_DIN_BLACKALTERNATE"]);
            }

            if (fontName.ToUpper().StartsWith("F_DINNEXTLTPRO_MEDIUM"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_DINNEXTLTPRO_MEDIUM"]);
            }

            if (fontName.ToUpper().StartsWith("F_DINNEXTLTPRO_REGULAR"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_DINNEXTLTPRO_REGULAR"]);
            }

            if (fontName.ToUpper().StartsWith("F_EUROSTILELT_DEMI"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_EUROSTILELT_DEMI"]);
            }

            if (fontName.ToUpper().StartsWith("F_EUROSTILELT_BOLD_EXTENDED2"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_EUROSTILELT_BOLD_EXTENDED2"]);
            }

            if (fontName.ToUpper().StartsWith("F_HYQIHEI_60S"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_HYQIHEI_60S"]);
            }

            if (fontName.ToUpper().StartsWith("F_ROBOTOCONDENSED_REGULAR"))
            {
                text.SetValue(TextBlock.FontFamilyProperty, Application.Current.Resources["F_ROBOTOCONDENSED_REGULAR"]);
            }
        }

        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alignment_type"></param>
        public static void SetAlignment(TextBlock text, string alignment_type)
        {
            switch (alignment_type.ToLower())
            {
                case "left": text.TextAlignment = TextAlignment.Left; break;
                case "right": text.TextAlignment = TextAlignment.Right; break;
                case "center": text.TextAlignment = TextAlignment.Center; break;
            }
        }
    }
}
