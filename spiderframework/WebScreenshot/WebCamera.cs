using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using mshtml;

namespace fox.spider.screenshot
{
    public class WebCamera
    {
        public static Bitmap takeBrowserScreen(AxSHDocVw.AxWebBrowser browser, AxSHDocVw.AxWebBrowser adBrowser)
        {
            System.GC.Collect();
            IHTMLDocument2 oDoc = (IHTMLDocument2)browser.Document;
            //oDoc.parentWindow.execScript("window.document.body.scroll=\"no\";", "javascript");
            IHTMLElement2 oBody = (IHTMLElement2)oDoc.body;
            int iWidth = oBody.scrollWidth;
            int iHeight = oBody.scrollHeight;
            int iScreenWidth = browser.Width - 4;
            int iScreenHeight = browser.Height -4;
            int iWidthOffset = 4;
            int iHeightOffset = 4;
            if (iScreenWidth < iWidth)
            {
                iScreenHeight -= 17;
                iHeightOffset += 17;
            }
            if (iScreenHeight < iHeight)
            {
                iScreenWidth -= 17;
                iWidthOffset += 17;
            }
            return takePicture(browser.Handle, oDoc.parentWindow, iWidth, iHeight,
                iScreenWidth, iScreenHeight, 2, 2, iWidthOffset, iHeightOffset, adBrowser == null ? IntPtr.Zero : adBrowser.Handle);
            /*Bitmap oBM = new Bitmap(iWidth, iHeight);
            int iLeftCount = iWidth / iScreenWidth + 1;//因為邊框=2，左右兩個共 4
            int iTopCount = iHeight / iScreenHeight + 1;//因為邊框=2，上下兩個共 4
            Graphics g = Graphics.FromImage(oBM);
            int iX = 0;
            for (int i = 0; i < iLeftCount; i++)
            {
                int iY = 0;
                Image oImage = null;
                for (int j = 0; j < iTopCount; j++)
                {
                    int iTop = j * iScreenHeight;// 計算要移動的位置
                    int iLeft = i * iScreenWidth;// 計算要移動的位置
                    int iImageMoveX = (iLeft + iScreenWidth) > iWidth ?
                        iLeft + iScreenWidth - iWidth : 0;
                    int iImageMoveY = (iTop + iScreenHeight) > iHeight ?
                        iTop + iScreenHeight - iHeight : 0;

                    oDoc.parentWindow.scrollTo(iLeft, iTop);

                    oImage = RectCaptureTool.CaptureControl(browser.Handle);

                    g.DrawImage(oImage, new Rectangle(iX, iY, oImage.Width - iWidthOffset, oImage.Height - iHeightOffset),
                        new Rectangle(iImageMoveX + 2, iImageMoveY + 2, oImage.Width - iWidthOffset, oImage.Height - iHeightOffset),
                        GraphicsUnit.Pixel);

                    iY += oImage.Height - iHeightOffset;
                }
                iX += oImage.Width - iWidthOffset;
            }
            g.Dispose();
            System.GC.Collect();
            return oBM;*/
        }

        protected static Bitmap takePicture(IntPtr handle, IHTMLWindow2 window, int width, int height,
            int screenWidth, int screenHeight, int xOffset, int yOffset, int widthOffset, int heightOffset, IntPtr adBrowser)
        {
            IHTMLElement2 oBody = (IHTMLElement2)window.document.body;
            int iScrollTop = oBody.scrollTop;
            int iScrollLeft = oBody.scrollLeft;

            Bitmap oBM = null;
            if (!IntPtr.Zero.Equals(adBrowser))
            {
                oBM = new Bitmap(width, height + 85);
            }
            else
            {
                oBM = new Bitmap(width, height);
            }
            int iLeftCount = width / screenWidth + 1;//計算要移動的次數
            int iTopCount = height / screenHeight + 1;//計算要移動的次數
            Graphics g = Graphics.FromImage(oBM);
            int iX = 0;
            for (int i = 0; i < iLeftCount; i++)
            {
                int iY = 0;
                Image oImage = null;
                for (int j = 0; j < iTopCount; j++)
                {
                    int iTop = j * screenHeight;// 計算要移動的位置
                    int iLeft = i * screenWidth;// 計算要移動的位置
                    int iImageMoveX = (iLeft + screenWidth) > width ?
                        iLeft + screenWidth - width : 0;
                    int iImageMoveY = (iTop + screenHeight) > height ?
                        iTop + screenHeight - height : 0;

                    window.scrollTo(iLeft, iTop);
                    System.Windows.Forms.Application.DoEvents();
                    oImage = RectCaptureTool.CaptureControl(handle);//抓圖

                    g.DrawImage(oImage, new Rectangle(iX, iY, oImage.Width - widthOffset, oImage.Height - heightOffset),
                        new Rectangle(iImageMoveX + xOffset, iImageMoveY + yOffset, 
                        oImage.Width - widthOffset, oImage.Height - heightOffset),
                        GraphicsUnit.Pixel);//把圖片中的某個區塊畫到預計要輸出的圖片中

                    iY += oImage.Height - heightOffset;
                }
                iX += oImage.Width - widthOffset;
            }
            if (!IntPtr.Zero.Equals(adBrowser))
            {//把 AdSense 的圖片放在照片的最底下
                g.FillRectangle(Brushes.White, new Rectangle(0, height, width, 85));
                Image oAd = RectCaptureTool.CaptureControl(adBrowser);
                g.DrawImage(oAd, new Rectangle(0, height, oAd.Width - 4, oAd.Height - 4),
                    new Rectangle(2, 2, oAd.Width - 4, oAd.Height - 4), GraphicsUnit.Pixel);
            }

            g.Dispose();
            window.scrollTo(iScrollLeft, iScrollTop);
            System.GC.Collect();
            return oBM;
        }

        public static Size calcOffsetToBrowser(IHTMLElement elm)
        {
            IHTMLElement oElm = elm;
            Size oReturn = new Size();
            while (oElm != null)
            {
                if (oElm.offsetParent != null)
                {
                    oReturn.Width += oElm.offsetLeft;
                    oReturn.Height += oElm.offsetTop;
                    oElm = oElm.offsetParent;
                }
                else if (oElm.tagName.Equals("BODY"))
                {
                    IHTMLWindow4 oWin = (IHTMLWindow4)((IHTMLDocument2)oElm.document).parentWindow;
                    oElm = (IHTMLElement)oWin.frameElement;
                }
                else
                {
                    oElm = null;
                }
            }
            return oReturn;
        }

        public static Bitmap takeFrameScreen(AxSHDocVw.AxWebBrowser browser, IHTMLFrameBase2 frame, AxSHDocVw.AxWebBrowser adBrowser)
        {
            object oObj = true;
            frame.contentWindow.document.body.scrollIntoView(oObj);
            frame.contentWindow.document.body.style.borderWidth = "0px";
            IHTMLElement oFrameElement = (IHTMLElement)frame;
            string sBorderWidth = oFrameElement.style.borderWidth;
            oFrameElement.style.borderWidth = "0px";
            IHTMLFrameBase oFrame = (IHTMLFrameBase)frame;
            string sFrameBorder = oFrame.frameBorder;
            oFrame.frameBorder = "0";
            System.Windows.Forms.Application.DoEvents();

            Size oOffset = calcOffsetToBrowser(frame.contentWindow.document.body);
            int iTopCoordinate = oOffset.Height+2;
            int iLeftCoordinate = oOffset.Width+2;
            IHTMLElement2 oBody = (IHTMLElement2)frame.contentWindow.document.body;
            int iWidth = oBody.scrollWidth;
            int iHeight = oBody.scrollHeight;
            int iScreenWidth = oBody.clientWidth;
            int iScreenHeight = oBody.clientHeight;
            int iWidthOffset = browser.Width - iScreenWidth;
            int iHeightOffset = browser.Height - iScreenHeight;
            Bitmap oBM = takePicture(browser.Handle, frame.contentWindow, iWidth, iHeight, iScreenWidth, iScreenHeight, iLeftCoordinate, iTopCoordinate,
                iWidthOffset, iHeightOffset, adBrowser == null ? IntPtr.Zero : adBrowser.Handle);
            frame.contentWindow.document.body.style.borderWidth = "1px";
            oFrame.frameBorder = sFrameBorder;
            oFrameElement.style.borderWidth = sBorderWidth;
            return oBM;
        }
    }
}
