using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace WareHouseControl
{
    /// <summary>
    /// 单元格按列分组管理，包含列数个单元格
    /// </summary>
    public class CellsRowGroup:IDisposable
    {
#region 属性
        private List<HouseCell> _CellsList;
        public List<HouseCell> CellsList
        {
            get
            {
                return _CellsList;
            }
            private set
            {
                _CellsList = value;
            }
        }
        public Color BorderColor { get; set; }
        public int BorderLineWeight { get; set; }
        private Point _Location = new Point();
        public Point Location 
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }
        private int _LayerIndex = 1;
        public int LayerIndex
        {
            get
            {
                return _LayerIndex;
            }
            set
            {
                _LayerIndex = value;
                if (_LayerIndex < 1)
                    _LayerIndex = 1;
            }
        }
        private int _RowIndex = 1;
        public int RowIndex
        {
            get
            {
                return _RowIndex;
            }
            set
            {
                _RowIndex = value;
                if (_RowIndex < 1)
                    _RowIndex = 1;
            }
        }
        private int _ColumnCount = 50;
        public int ColumnCount
        {
            get
            {
                return _ColumnCount;
            }
            set
            {
                _ColumnCount = value;
                if (_ColumnCount < 1)
                    _ColumnCount = 1;
            }
        }
        /// <summary>
        /// 货架单元格列宽
        /// </summary>
        private int _CellColumnWidth = 30;
        public int CellColumnWidth
        {
            get
            {
                return _CellColumnWidth;
            }
            set
            {
                _CellColumnWidth = value;
            
            }
        }

        /// <summary>
        /// 货架单元格行高
        /// </summary>
        private int _CellRowHeight = 30;
        public int CellRowHeight
        {
            get
            {
                return _CellRowHeight;
            }
            set
            {
                _CellRowHeight = value;
              
            }
        }
        private Size _RowSize = new Size();
        public Size RowSize
        { 
            get
            {
                return _RowSize;
            }
            private set
            {
                _RowSize = value;
            }
        }

        private Color _BkgColor = Color.Black;
        public Color BkgColor
        {
            get
            {
                return _BkgColor;
            }
            set
            {
                _BkgColor = value;
               
            }
        }

        private Color _CellBorderColor = Color.Orange;
        public Color CellBorderColor
        {
            get
            {
                return _CellBorderColor;
            }
            set
            {
                _CellBorderColor = value;
            }
        }

        /// <summary>
        /// rows对象覆盖的区域
        /// </summary>
        private Rectangle _RowRect = new Rectangle();
        public Rectangle RowRect
        {
            get
            {
                return _RowRect;
            }
            private set
            {
                _RowRect = value;
            }
        }
#endregion

        public CellsRowGroup()
        {
            _CellsList = new List<HouseCell>();
            BkgColor = new Color();
            BkgColor = Color.Black;
            BorderColor = new Color();
            BorderColor = Color.Orange;
            BorderLineWeight = 2;
            _RowSize = Size.Empty;
        }
        public void Dispose()
        {

        }
        public void FillCells(Point OriPt,int L,int R)
        {
            _Location = OriPt;
            _RowIndex = R;
            _RowRect = new Rectangle(_Location, new Size(_ColumnCount * _CellColumnWidth, _CellRowHeight));
            for (int C = 1; C <= _ColumnCount; C++)
            {
                Point ltCornerPt = new Point();
                ltCornerPt.X = _Location.X+(C-1)*_CellColumnWidth;
                ltCornerPt.Y = _Location.Y;
                HouseCell cell = new HouseCell(new CellPos(L, R, C), ltCornerPt, new Size(_CellColumnWidth, _CellRowHeight), _BkgColor, _CellBorderColor);
                _CellsList.Add(cell);
            }
        }
        public void Add(HouseCell cell)
        {
            
            _CellsList.Add(cell);
            
            _RowSize.Width += cell.CellSize.Width;
            if (_RowSize.Height < cell.CellSize.Height)
                _RowSize.Height = cell.CellSize.Height;

        }
        /// <summary>
        /// 得到cell单元格
        /// </summary>
        /// <param name="columnIndex">列序号，从1开始编号</param>
        /// <returns></returns>
        public HouseCell GetCell(int columnIndex)
        {
            if (columnIndex > _CellsList.Count)
                return null;
            int i=columnIndex-1;
            if (i >= 0)
                return _CellsList.ElementAt(i);
            return null;
        }

        /// <summary>
        /// 返回单元格数
        /// </summary>
        /// <returns></returns>
        public int GetCellCount()
        {
            return _CellsList.Count;
        }

       // int nCount = 0;
        /// <summary>
        /// 自绘
        /// </summary>
        /// <param name="g"></param>
        public void PaintSelf(Graphics g)
        {
            //if (_LayerIndex == 1)
            //{
            //    Console.WriteLine("{0},layer {1},row {2},paint\n",nCount++, _LayerIndex, _RowIndex);
            //}
            foreach (HouseCell cell in _CellsList)
            {
                cell.PaintSelf(g);
            }
            //层编号
            int textBack = 15;
            int textRecWidth = 20;
            Font font = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush fontBrush = new SolidBrush(Color.Azure);
            StringFormat drawingFormat1 = new StringFormat();
            drawingFormat1.Alignment = StringAlignment.Center;
            
            RectangleF rect = new RectangleF(_Location.X - textBack, _Location.Y, textRecWidth,_RowSize.Height);
            string str = _RowIndex.ToString();
            g.DrawString(str, font, fontBrush, rect, drawingFormat1);
        }
    }
}
