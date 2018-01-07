using StarRatingsControl.EnrollmentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StarRatingsControl
{
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            this.Loaded += Window2_Loaded;
            this.SizeChanged += Window2_SizeChanged;
        }

        private void Window2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.pbImge.Image = System.Drawing.Image.FromFile(@"Resources\videocall.png");
            this.tbStatus.Text = string.Format("hostsize {0}:{1}; pb size:{2}:{3}; ratio{4},ratio{5} ", 
                wfh.ActualHeight,wfh.ActualWidth,pbwf.Width,pbwf.Height, wfh.ActualHeight/wfh.ActualWidth,(double)pbwf.Height/(double)pbwf.Width);
        }

        private void Window2_Loaded(object sender, RoutedEventArgs e)
        {
            WebCamera wc = new WebCamera();
            wc.StartPreview(this.pbwf);
        }

        private void btnSetAspectRatio_Click(object sender, RoutedEventArgs e)
        {
            this.wfh.AspectRatio = double.Parse(this.txtRatio.Text);
        }
    }
}
