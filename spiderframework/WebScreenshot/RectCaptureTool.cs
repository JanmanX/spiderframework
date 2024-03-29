using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace fox.spider.screenshot
{
    public class RectCaptureTool
    {
        public static Image CaptureControl(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            Bitmap img = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img);
            User32.PrintWindow(handle, g.GetHdc(), 0);
            g.ReleaseHdc();
            // create a device context we can copy to
            //IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            //// create a bitmap we can copy it to,
            //// using GetDeviceCaps to get the width/height
            //IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            //// select the bitmap object
            //IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            //// bitblt over
            //GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            //// restore selection
            //GDI32.SelectObject(hdcDest, hOld);
            //// clean up
            //GDI32.DeleteDC(hdcDest);
            //User32.ReleaseDC(handle, hdcSrc);
            //// get a .NET image object for it
            //Image img = Image.FromHbitmap(hBitmap);
            //// free up the Bitmap object
            //GDI32.DeleteObject(hBitmap);
            return Image.FromHbitmap(img.GetHbitmap());
        }


        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>a
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

            [DllImport("user32.dll")]
            public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

            public const int GW_CHILD = 5;
            public const int GW_HWNDNEXT = 2;
        }
    }
}
