using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;

namespace StarRatingsControl
{
    public class FixedAspectWinFormsHost : WindowsFormsHost
    {
        static FixedAspectWinFormsHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FixedAspectWinFormsHost), new FrameworkPropertyMetadata(typeof(FixedAspectWinFormsHost)));
            
        }

        public static DependencyProperty AspectRatioDependecyProperty = DependencyProperty.Register("AspectRatio",
                typeof(double),
                typeof(FixedAspectWinFormsHost),
                new FrameworkPropertyMetadata(1.777778, FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(OnAspectRationChanged)));
        public double AspectRatio
        {
            get
            {
                return (double)GetValue(AspectRatioDependecyProperty);
            }
            set
            {
                SetValue(AspectRatioDependecyProperty, value);
            }
        }

       static void OnAspectRationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var faWfHost = sender as FixedAspectWinFormsHost;
            double desiredRatio = (double)e.NewValue;
            ResizeByDesiredRatio(faWfHost, desiredRatio);
        }

        private static void ResizeByDesiredRatio(FixedAspectWinFormsHost faWfHost, double desiredRatio)
        {
            double curRatio = faWfHost.ActualWidth / faWfHost.ActualHeight;
             
            if (curRatio > desiredRatio)
            { 
                faWfHost.Width = faWfHost.ActualHeight * desiredRatio;
            }
            else if (curRatio < desiredRatio)
            {
                faWfHost.Height = faWfHost.ActualWidth / desiredRatio;
            }
        }

        private Size GetNewSize(Size constraint)
        {
            Size newSize = constraint;
            double curRatio = constraint.Width / constraint.Height;
            if (curRatio > this.AspectRatio)
            {
                newSize.Width = constraint.Height * this.AspectRatio;
            }
            else if (curRatio < this.AspectRatio)
            {
                newSize.Height = constraint.Width / this.AspectRatio;
            }
            return newSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size newSize = this.GetNewSize(finalSize);

            return base.ArrangeOverride(newSize);
        }

    }
}
