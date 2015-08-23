using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace WareHouseControl
{
    /// <summary>
    /// 单元格分层管理，每层包含行数*列数个单元格
    /// </summary>
    public class CellsLayerGroup:IDisposable
    {
#region 属性
        private List<CellsRowGroup> _RowsList;
        public List<CellsRowGroup> RowsList
        {
            get
            {
                return _RowsList;
            }
            private set
            {
                _RowsList = value;
            }
        }
        public Color BkgColor { get; set; }
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
        public int Width { get; set; }
        public int Height { get; set; }
        private int _LayerIndex;
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
        /// <summary>
        /// 巷道宽,最小30像素宽
        /// </summary>
        private int _ChannelWidth = 40;
        public int ChannelWidth
        {
            get
            {
                return _ChannelWidth;
            }
            set
            {
                _ChannelWidth = value;
              
            }
        }

        private int _ChannelCount = 2;
        public int ChannelCount
        {
            get
            { return _ChannelCount; }
            set
            {
                _ChannelCount = value;
                if (_ChannelCount < 1)
                    _ChannelCount = 1;
                
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
                foreach (CellsRowGroup row in _RowsList)
                {

                    row.ColumnCount = _ColumnCount;
                }
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
        public int  CellRowHeight
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
        private Size _LayerSize = new Size();
        public Size LayerSize
        {
            get
            {
                return _LayerSize;
            }
            private set
            {
                _LayerSize = value;

            }
        }

        /// <summary>
        /// 货架显示区标题栏高度
        /// </summary>
        private int _CanvasTopHeight = 30;
        public int CanvasTopHeight
        {
            get
            {
                return _CanvasTopHeight;
            }
            set
            {
                _CanvasTopHeight = value;
                if (_CanvasTopHeight < 30)
                    _CanvasTopHeight = 30;

            }
        }

        private int _CanvasLeftWidth = 30;
        public int CanvasLeftWidth
        {
            get
            {
                return _CanvasLeftWidth;
            }
            set
            {
                _CanvasLeftWidth = value;
                if (_CanvasLeftWidth < 30)
                    _CanvasLeftWidth = 30;
            }
        }

        /// <summary>
        /// 层对象覆盖的区域
        /// </summary>
        private Rectangle _LayerRect = new Rectangle();
        public  Rectangle LayerRect
        {
            get
            {
                return _LayerRect;
            }
            private set
            {
                _LayerRect = value;
            }
        }
#endregion
        public CellsLayerGroup()
        {
            _RowsList = new List<CellsRowGroup>();
            BkgColor = new Color();
            BkgColor = Color.Black;
            BkgColor = new Color();
            BkgColor = Color.Black;
            BorderColor = new Color();
            BorderColor = Color.Blue;
            BorderLineWeight = 1;

        }
        public void Dispose()
        {

        }
        public void FillRows(Point OriPt,int layerIndex)
        {
            _LayerIndex = layerIndex;
            _Location = OriPt;
            Width = _ColumnCount * _CellColumnWidth;
            Height = (_ChannelWidth + 2 * _CellRowHeight) * _ChannelCount;
            _LayerSize.Width = _ColumnCount * _ChannelWidth;
            _LayerSize.Height = (_ChannelWidth + 2 * _CellRowHeight) * _ChannelCount;
            _LayerRect = new Rectangle(_Location, _LayerSize);
            //添加cell
            for (int R = 1; R <= _ChannelCount * 2; R++)
            {
                CellsRowGroup row = new CellsRowGroup();
                row.ColumnCount = _ColumnCount;
                row.CellColumnWidth = _CellColumnWidth;
                row.CellRowHeight = _CellRowHeight;
                row.LayerIndex = layerIndex;
                int x=_Location.X;
                int y=_Location.Y+ (R-1)*_CellRowHeight+R/2*_ChannelWidth;
                row.FillCells(new Point(x, y),LayerIndex,R);
                _RowsList.Add(row);
             }
            
           
        }
        public void Add(ref CellsRowGroup cellRow)
        {

            _RowsList.Add(cellRow);
        }
        public CellsRowGroup GetRowCells(int RowIndex)
        {
            if (RowIndex > _RowsList.Count)
                return null;
            if (RowIndex < 1)
                return null;
            return _RowsList.ElementAt(RowIndex - 1);
        }
       public void PaintSelf(Graphics g)
       {
           foreach(CellsRowGroup rowGroup in _RowsList)
           {
               if(rowGroup != null)
               {
                   rowGroup.PaintSelf(g);
               }
           }
           //绘制层号，行号
           int lineBack = 12;
           int lineShortSeg = 8;
           int textBack=30;
           int textRecWidth=20;
           Pen pen = new Pen(Color.Azure, 2);
           Point stpt = new Point(_Location.X - lineBack, _Location.Y);
           Point endpt = new Point(stpt.X ,stpt.Y+_LayerSize.Height);
           g.DrawLine(pen, stpt, endpt);
           stpt = new Point(_Location.X - lineBack, _Location.Y);
           endpt = new Point(stpt.X + lineShortSeg, stpt.Y);
           g.DrawLine(pen, stpt, endpt);
           stpt = new Point(_Location.X - lineBack, _Location.Y + _LayerSize.Height);
           endpt = new Point(stpt.X + lineShortSeg, stpt.Y);
           g.DrawLine(pen, stpt, endpt);
           Font font = new Font("Arial", 10, FontStyle.Regular);
           SolidBrush fontBrush = new SolidBrush(Color.Azure);
           StringFormat drawingFormat1 = new StringFormat();
           drawingFormat1.Alignment = StringAlignment.Center;
           drawingFormat1.FormatFlags = StringFormatFlags.DirectionVertical;
           RectangleF rect = new RectangleF(_Location.X-textBack,_Location.Y,textRecWidth,_LayerSize.Height);
           string str = _LayerIndex.ToString()+"层";
           g.DrawString(str, font, fontBrush, rect, drawingFormat1);
           font.Dispose();
           fontBrush.Dispose();
       }
    }
}
