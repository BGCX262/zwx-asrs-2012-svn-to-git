using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace WareHouseControl
{

    /// <summary>
    /// 货架单元坐标结构体
    /// </summary>
    public class CellPos:object
    {
        public CellPos(int l, int r, int c)
        { this._L = l; this._R = r; this._C = c; }
        /// <summary>
        /// 层序号，从1开始编号
        /// </summary>
        private int _L;
        public int L
        {
            get
            {
                return _L;
            }
            set
            {
                _L = value;
            }
        }

        /// <summary>
        /// 行序号，从1开始编号
        /// </summary>
        private int _R;
        public int R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;
            }
        }

        /// <summary>
        /// 列序号，从1开始编号
        /// </summary>
        private int _C;
        public int C
        {
            get
            {
                return _C;
            }
            set
            {
                _C = value;
            }

        }
    }

    /// <summary>
    /// 单元格鼠标状态
    /// </summary>
    public enum CellMouseStatus
    {
        MOUSE_NONE=1, //无鼠标事件
        MOUSE_ENTER, //鼠标进入
        MOUSE_LEAVE, //鼠标离开
        MOUSE_DOWN, ////鼠标按下
        MOUSE_UP //抬起
    }
    /// <summary>
    /// 仓位状态
    /// </summary>
    public enum CellStoreStatus
    {
        CELL_EMPTY = 1, //仓位空
        CELL_FULL, //仓位满
        CELL_ERROR1 //仓位异常
    }
    /// <summary>
    /// 货架单元格，封装了单元格的绘制、事件
    /// </summary>
    public class HouseCell
    {
#region 属性
        public CellStoreStatus CellStatus{get;set;}
        /// <summary>
        /// 左上角位置
        /// </summary>
        public Point ltCornerPos { get; set; }

        /// <summary>
        /// 单元格尺寸
        /// </summary>
        public Size CellSize{get;set;}

        /// <summary>
        /// 单元格前景色
        /// </summary>
        public Color ForeColor{get;set;}

        /// <summary>
        /// 单元格边框色
        /// </summary>
        public Color BorderColor{get;set;}

        /// <summary>
        /// 单元格坐标（层，行，列）
        /// </summary>
        public CellPos CellCoord{get;set;}

        /// <summary>
        /// cell对象覆盖的区域
        /// </summary>
        private Rectangle _CellRect;
        public Rectangle CellRect
        {
            get
            {
                return _CellRect;
            }
            private set
            {
                _CellRect = value;
            }
        }

        /// <summary>
        /// 单元格鼠标状态
        /// </summary>
        private CellMouseStatus _CellMouse = CellMouseStatus.MOUSE_NONE;
        public CellMouseStatus CellMouse 
        {
            get
            {
                return _CellMouse;
            }
            set
            {
                _CellMouse = value;
            }
        }

        
#endregion
        public HouseCell(CellPos coord,Point ltCorner, Size size, Color bkgColor,Color borderColor)
        {
            CellCoord = coord;
            ltCornerPos = ltCorner;
            CellSize = size;
            ForeColor = bkgColor;
            BorderColor = borderColor;
            _CellRect = new Rectangle(ltCorner,size);
            CellStatus = CellStoreStatus.CELL_EMPTY;


        }
        /// <summary>
        /// 自绘
        /// </summary>
        /// <param name="g"></param>
        public void PaintSelf(Graphics g)
        {
            Pen BorderPen = null;
            Pen linePen = new Pen(Color.Azure, 1);
            SolidBrush bkgBrush = null;
            float borderLineWeight = 2.0f;
            //float innerLineWeight = 1.0f;
            switch(CellStatus)
            {
                case CellStoreStatus.CELL_EMPTY:
                    {
                        bkgBrush = new SolidBrush(PublicAttr.CellEmptyColor);
                        g.FillRectangle(bkgBrush, _CellRect);
                        g.DrawLine(linePen, ltCornerPos, new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y + CellSize.Height));
                        g.DrawLine(linePen, new Point(ltCornerPos.X, ltCornerPos.Y + CellSize.Height), new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y));
                        break;
                    }
                case CellStoreStatus.CELL_FULL:
                    {
                        bkgBrush = new SolidBrush(PublicAttr.CellFullColor);
                        g.FillRectangle(bkgBrush, _CellRect);
                       
                        break;
                    }
                case CellStoreStatus.CELL_ERROR1:
                    {
                        bkgBrush = new SolidBrush(PublicAttr.CellExecptionColor);
                        g.FillRectangle(bkgBrush, _CellRect);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
             
            //           
            //g.FillRectangle(bkgBrush, _CellRect);
            switch (CellMouse)
            {
                case CellMouseStatus.MOUSE_NONE:
                    {
                        BorderPen = new Pen(BorderColor, borderLineWeight);
                        g.DrawRectangle(BorderPen, _CellRect);
                        

                        break;
                    }
                case CellMouseStatus.MOUSE_ENTER:
                    {

                        bkgBrush = new SolidBrush(Color.FromArgb(100, Color.SpringGreen));
                        BorderPen = new Pen(Color.Lime, borderLineWeight);
                        Rectangle rect = new Rectangle();
                        rect = _CellRect;
                        rect.Inflate(5, 5);
                        g.FillRectangle(bkgBrush, rect);
                        g.DrawRectangle(BorderPen, rect);
                        //g.DrawLine(linePen, ltCornerPos, new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y + CellSize.Height));
                        //g.DrawLine(linePen, new Point(ltCornerPos.X, ltCornerPos.Y + CellSize.Height), new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y));
                        break;
                    }
                case CellMouseStatus.MOUSE_LEAVE:
                    {
                        BorderPen = new Pen(BorderColor, borderLineWeight);
                        g.DrawRectangle(BorderPen, _CellRect);
                       // g.DrawLine(linePen, ltCornerPos, new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y + CellSize.Height));
                        //g.DrawLine(linePen, new Point(ltCornerPos.X, ltCornerPos.Y + CellSize.Height), new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y));
                        break;
                    }
                case CellMouseStatus.MOUSE_DOWN:
                    {
                        BorderPen = new Pen(Color.Lime, borderLineWeight);
                        Rectangle rect = new Rectangle();
                        rect = _CellRect;
                        rect.Inflate(-2, -2);
                        g.DrawRectangle(BorderPen, rect);
                      //  g.DrawLine(linePen, ltCornerPos, new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y + CellSize.Height));
                      //  g.DrawLine(linePen, new Point(ltCornerPos.X, ltCornerPos.Y + CellSize.Height), new Point(ltCornerPos.X + CellSize.Width, ltCornerPos.Y));
                        break;
                    }
                case CellMouseStatus.MOUSE_UP:
                    {
                        break;
                    }
                default:
                    break;
            }

            if(BorderPen!= null)
                BorderPen.Dispose();
            if(bkgBrush!= null)
                bkgBrush.Dispose();
            if(linePen != null)
                linePen.Dispose();
        }
    }
}
