using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WareHouseControl
{
    public partial class PopPanel : Form
    {
#region 属性
        private string _StrDisp = string.Empty;
        private Color _BkgColor = Color.CornflowerBlue;
        public Color BkgColor
        {
            get
            {
                return _BkgColor;
            }
            set
            {
                _BkgColor = value;
                this.panel1.BackColor = _BkgColor;
                Refresh();
            }
        }
#endregion
        
        public PopPanel()
        {
            InitializeComponent();
        }
         public void SetDispContent(string strContent)
         {
             _StrDisp = strContent;
             
             this.Refresh();
         }

         private void panel1_Paint(object sender, PaintEventArgs e)
         {
             Graphics curG = e.Graphics;
             //内存绘图,解决闪烁

             BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
             currentContext.MaximumBuffer = new Size(this.panel1.Width + 1, this.panel1.Height + 1);
             BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);
             Graphics g = myBuffer.Graphics;
             g.SmoothingMode = SmoothingMode.HighQuality;//
             g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
             g.Clear(Color.FromArgb(100,_BkgColor));

             //绘制标题
             Font TitleFont = new Font("Arial", 15, FontStyle.Regular);
             SolidBrush fontBrush = new SolidBrush(Color.Azure);
             StringFormat drawingFormat1 = new StringFormat();
             drawingFormat1.Alignment = StringAlignment.Center;
             RectangleF TitleRect = new RectangleF(0, 0, this.panel1.Width, 100);
             g.DrawString(_StrDisp, TitleFont, fontBrush, TitleRect, drawingFormat1);
             //绘制内容

             myBuffer.Render(curG);
             g.Dispose();
             myBuffer.Dispose();//释放资源
         }
    }
}
