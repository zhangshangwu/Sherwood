using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarRatingsControl
{
    /* Usage: 
  * 
  * webCamera webCamera = new webCamera();
  * 
  * webCamera.StartPreview(PictureBoxName);
  * 
  * webCamera.StopPreview();
  * 
  * 
  * pBoxCaptured.Image = webCamera.CaptureImage();
  * 
  * 
  * 
  * */


    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    //using DevExpress.XtraEditors;



    namespace EnrollmentSystem
    {
        public class WebCamera
        {
            const int WM_CAP_START = 0x400;
            const int WS_CHILD = 0x40000000;

            const int WS_VISIBLE = 0x10000000;
            const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
            const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
            const int WM_CAP_EDIT_COPY = WM_CAP_START + 30;
            const int WM_CAP_SEQUENCE = WM_CAP_START + 62;

            const int WM_CAP_FILE_SAVEAS = WM_CAP_START + 23;
            const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
            const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;

            const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
            const int SWP_NOMOVE = 0x2;
            const int SWP_NOSIZE = 1;
            const int SWP_NOZORDER = 0x4;

            const int HWND_BOTTOM = 1;
            //--The capGetDriverDescription function retrieves the version description of the capture driver--
            [DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

            public static extern bool capGetDriverDescriptionA(short wDriverIndex, string lpszName, int cbName, string lpszVer, int cbVer);
            //--The capCreateCaptureWindow function creates a capture window--
            [DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]


            public static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWnd, int nID);
            //--This function sends the specified message to a window or windows--
            [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]


            public static extern int SendMessage(int hwnd, int Msg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);
            //--Sets the position of the window relative to the screen buffer--
            [DllImport("user32", EntryPoint = "SetWindowPos", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]


            public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
            //--This function destroys the specified window--
            [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]


            public static extern bool DestroyWindow(int hndw);

            int VideoSource = 0;

            int hWnd;

            public bool StartPreview(PictureBox _preview)
            {
                _preview.SizeMode = PictureBoxSizeMode.Zoom;
                try
                {
                    hWnd = capCreateCaptureWindowA(Convert.ToString(VideoSource), WS_VISIBLE | WS_CHILD, 0, 0, 0, 0, Convert.ToInt32(_preview.Handle.ToString()), 0);
                    Boolean temp = Convert.ToBoolean(SendMessage(hWnd, WM_CAP_DRIVER_CONNECT, VideoSource, 0));
                    if (temp)
                    {
                        //---set the preview scale---
                        SendMessage(hWnd, WM_CAP_SET_SCALE, Convert.ToInt32(true), 0);
                        //---set the preview rate (ms)---
                        SendMessage(hWnd, WM_CAP_SET_PREVIEWRATE, 30, 0);
                        //---start previewing the image---
                        SendMessage(hWnd, WM_CAP_SET_PREVIEW, Convert.ToInt32(true), 0);
                        //---resize window to fit in PictureBox control---
                        SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, _preview.Width, _preview.Height, SWP_NOMOVE | SWP_NOZORDER);

                        return true;
                    }
                    else
                    {
                        DestroyWindow(hWnd);
                        MessageBox.Show("Device was not detected.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    DestroyWindow(hWnd);
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }

            public void StopPreview()
            {
                try
                {
                    SendMessage(hWnd, WM_CAP_DRIVER_DISCONNECT, VideoSource, 0);
                    //VideoSource = 0;
                    DestroyWindow(hWnd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            public Image CaptureImage()
            {
                try
                {
                    IDataObject data = default(IDataObject);
                    Image bitMap = default(Image);

                    //---copy the image to the clipboard---
                    SendMessage(hWnd, WM_CAP_EDIT_COPY, 0, 0);

                    //---retrieve the image from clipboard and convert it 
                    // to the bitmap format
                    data = Clipboard.GetDataObject();

                    if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                    {
                        bitMap = (Image)data.GetData(typeof(System.Drawing.Bitmap));
                    }

                    return bitMap;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }
    }
}
