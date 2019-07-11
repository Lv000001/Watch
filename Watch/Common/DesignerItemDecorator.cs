using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Watch.Adorners;

namespace Watch
{

    public class DesignerItemDecorator : Control
    {
        private Adorner adorner;

        public bool ShowDecorator
        {
            get { return (bool)GetValue(ShowDecoratorProperty); }
            set { SetValue(ShowDecoratorProperty, value); }
        }

        public static readonly DependencyProperty ShowDecoratorProperty =
            DependencyProperty.Register("ShowDecorator", typeof(bool), typeof(DesignerItemDecorator),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ShowDecoratorProperty_Changed)));

        public DesignerItemDecorator()
        {
            Unloaded += new RoutedEventHandler(this.DesignerItemDecorator_Unloaded);
        }

        private void HideAdorner()
        {
            if (this.adorner != null)
            {
                this.adorner.Visibility = Visibility.Hidden;
                SetCanResize(GetCanResize(this));
            }
        }

        private void ShowAdorner()
        {
            if (this.adorner == null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);

                if (adornerLayer != null)
                {
                    ContentControl designerItem = this.DataContext as ContentControl;
                    Canvas canvas = VisualTreeHelper.GetParent(designerItem) as Canvas;
                    this.adorner = new SizeAdorner(designerItem);
                    SetCanResize(GetCanResize(this));
                    adornerLayer.Add(this.adorner);

                    if (this.ShowDecorator)
                    {
                        this.adorner.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.adorner.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                this.adorner.Visibility = Visibility.Visible;
                SetCanResize(GetCanResize(this));
            }
        }

        private void DesignerItemDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.adorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.adorner);
                }

                this.adorner = null;
            }
        }

        private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignerItemDecorator decorator = (DesignerItemDecorator)d;
            bool showDecorator = (bool)e.NewValue;

            if (showDecorator)
            {
                decorator.ShowAdorner();
            }
            else
            {
                decorator.HideAdorner();
            }
        }

        private void SetCanResize(bool canResize)
        {
            if (adorner != null && adorner is SizeAdorner size)
            {
                size.SetCanResize(canResize);
            }
        }

        // Using a DependencyProperty as the backing store for CanResize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanResizeProperty =
            DependencyProperty.Register("CanResize", typeof(bool), typeof(DesignerItemDecorator), new PropertyMetadata(true, (d, e) =>
            {
                DesignerItemDecorator decorator = (DesignerItemDecorator)d;
                bool canResize = (bool)e.NewValue;
                decorator.SetCanResize(canResize);
            }));


        public static bool GetCanResize(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanResizeProperty);
        }

        public static void SetCanResize(DependencyObject obj, bool value)
        {
            obj.SetValue(CanResizeProperty, value);
        }
    }
}
