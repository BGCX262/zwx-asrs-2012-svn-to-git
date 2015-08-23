using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft;
namespace WareHouseControl
{
   
    // */[ToolboxBitmap(@"E:\资料\图标\示教盒图标\res\位控板.ico")]  
    public partial class WareHouseControl : UserControl
    {
        #region 仓库属性设置

        private Size _CellRegionSize = new Size();
        public Size CellRegionSize
        {
            get
            { return _CellRegionSize; }
            private set
            {
                _CellRegionSize = value;
            }
        }
        /// <summary>
        /// 层间距(显示）
        /// </summary>
        private int _layerBetweenDist = 10;
        [Description("层间距"),Category("Data")]
        public int LayerBetweenDist
        {
            get
            {
                return _layerBetweenDist;
            }
            set 
            {
                _layerBetweenDist = value;
                if (_layerBetweenDist < 5)
                    _layerBetweenDist = 5;
            }
        }
        /// <summary>
        /// 巷道数，每条巷道对应两条货架
        /// </summary>
        private int _ChannelCount = 2;
        [Description("巷道数"), Category("Data")]
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
        [Description("每列货架单元数"), Category("Data")]
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
        /// 层数
        /// </summary>
        private int _LayerCount = 4;
        [Description("层数"), Category("Data")]
        public int LayerCount
        {
            get
            {
                return _LayerCount;
            }
            set
            {
                _LayerCount = value;
                if (_LayerCount < 1)
                    _LayerCount = 1;
            }
        }

        /// <summary>
        /// 巷道宽度,堆垛机在巷道中间的轨道
        /// </summary>
        private int _ChannelWidth = 30;
        [Description("巷道宽"), Category("Data")]
        public int ChannelWidth
        {
            get
            {
                return _ChannelWidth;
            }
            set
            {
                _ChannelWidth = value;
                if (_ChannelWidth < 20)
                    _ChannelWidth = 20;
            }
        }
        /// <summary>
        /// 货架单元格列宽
        /// </summary>
        private int _CellColumnWidth = 20;
        [Description("货架单元格列宽"), Category("Data")]
        public int CellColumnWidth
        {
            get
            {
                return _CellColumnWidth;
            }
            set
            {
                _CellColumnWidth = value;
                if (_CellColumnWidth < 20)
                    _CellColumnWidth = 20;
            }
        }

        /// <summary>
        /// 货架单元格行高
        /// </summary>
        private int _CellRowHeight = 20;
        [Description("货架单元格行高"), Category("Data")]
        public int CellRowHeight
        {
            get
            {
                return _CellRowHeight;
            }
            set
            {
                _CellRowHeight = value;
                if (_CellRowHeight < 10)
                    _CellRowHeight = 10;
            }
        }
        /// <summary>
        /// 货架显示区标题栏高度
        /// </summary>
        private int _CanvasTopHeight = 30;
        [Description("显示区Top标题区高度"),Category("Layout")]
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
        [Description("显示区Left标题区宽度"),Category("Layout")]
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
        private Color _BkgColor = Color.Black;
        [Description("背景色"), Category("Appearance")]
        public Color BkgColor
        {
            get
            {
                return _BkgColor;
            }
            set
            {
                _BkgColor = value;
                this.panelWareCells.BackColor = _BkgColor;
                Invalidate(true);
            }
        }

        private Color _CellBorderColor = Color.Orange;
         [Description("单元格边框颜色"), Category("Appearance")]
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
         private Color _CellForeColor = new Color();
         [Description("单元格前景色"), Category("Appearance")]
        public Color CellForeColor
        {
            get
            {
                return _CellForeColor;
            }
            set
            {
                _CellForeColor = value;

            }
        }
        #endregion
#region 自定义事件
        [Description("货架单元格的鼠标事件:单击鼠标事件"),Category("Mouse")]
        public event CellMouseEventHandler CellClick;

        [Description("货架单元格的鼠标事件:单击离开事件"), Category("Mouse")]
        public event CellMouseEventHandler CellMouseLeave;

        //[Description("货架单元格的鼠标事件:单击悬停事件"), Category("Mouse")]
        //public event CellMouseEventHandler CellMouseHover;

        [Description("货架单元格的鼠标事件：鼠标进入事件"),Category("Mouse")]
        public event CellMouseEventHandler CellMouseEnter;

#endregion
#region 包含的对象
        private PopPanel _popPanel = null;
        private List<CellsLayerGroup> _LayersList;
        private Dictionary<int, TRANSMachine> _MachineDic = new Dictionary<int, TRANSMachine>(); 
#endregion

#region 鼠标事件过程数据
        private Point _MouseLastPt; //上次鼠标位置
        private Point _MouseCurPt; //当前鼠标位置
        private Point _MouseDownPt; //鼠标按下位置
#endregion
        public WareHouseControl()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.AllPaintingInWmPaint, true);
            this._MouseCurPt = new Point(0, 0);
            this._MouseLastPt = new Point(0, 0);
            this._MouseDownPt = new Point(0, 0);
            _LayersList = new List<CellsLayerGroup>();
            
            //事件
            this.Load += WareHouseControl_Load;
           
        }

        private void WareHouseControl_SizeChanged(object sender, EventArgs e)
        {
            //RefreshLayout();
        }
        #region 内部功能函数
        private void RefreshLayout()
        {
            

        }
        #endregion

        private void WareHouseControl_Load(object sender, EventArgs e)
        {
            //创建弹出窗体
            _popPanel = new PopPanel();
            _popPanel.TopLevel= false;
            _popPanel.ControlBox = false;
           //  _popPanel.Parent = this.panelWareCells;
             this.panelWareCells.Controls.Add(_popPanel);
            

            Point OriPt = new Point(_CanvasLeftWidth, _CanvasTopHeight);
#region 按层、行组织
            for (int L = 1; L <= _LayerCount; L++)
            {
                CellsLayerGroup layerCells = new CellsLayerGroup();
                layerCells.ChannelCount = _ChannelCount;
                layerCells.ChannelWidth = _ChannelWidth;
                layerCells.ColumnCount = _ColumnCount;
                layerCells.CellColumnWidth = _CellColumnWidth;
                layerCells.CellRowHeight = _CellRowHeight;
                layerCells.ChannelWidth = _ChannelWidth;
                layerCells.CanvasLeftWidth = _CanvasLeftWidth;
                layerCells.CanvasTopHeight = _CanvasTopHeight;
                int x = _CanvasLeftWidth;
                int y = _CanvasTopHeight + _CellRegionSize.Height + _layerBetweenDist*(L-1);
                layerCells.FillRows(new Point(x, y), L);
                
                if (_CellRegionSize.Width < layerCells.Width)
                    _CellRegionSize.Width = layerCells.Width;
                _CellRegionSize.Height += (layerCells.Height + _layerBetweenDist);
              
               // layerCells.Location = new Point(x, y);
                layerCells.FillRows(new Point(x,y),L);
                _LayersList.Add(layerCells);
            }
            this.panelWareCells.AutoScrollMinSize = new Size(_CanvasLeftWidth + _CellRegionSize.Width+2*_CellColumnWidth, _CanvasTopHeight + _CellRegionSize.Height+3*_CellRowHeight);
            this.PerformLayout();
#endregion
            
            //创建穿梭车
            for(int i=1;i<=_ChannelCount;i++)
            {
                TRANSMachine transMachine = new TRANSMachine();
                transMachine.CellLayer = _LayersList.ElementAt(0);
                transMachine.ChannelIndex = i;
                Rectangle rect = new Rectangle();
                int X = transMachine.CellLayer.Location.X;
                int Y = transMachine.CellLayer.Location.Y + (i - 1) * (transMachine.CellLayer.CellRowHeight * 2 + transMachine.CellLayer.ChannelWidth) + transMachine.CellLayer.CellRowHeight;
                rect.Location = new Point(X,Y);
                rect.Width = transMachine.CellLayer.Width;
                rect.Height = _ChannelWidth;
                transMachine.WorkRect = rect;
                transMachine.MachinePos = new CellPos(1, i * 2 - 1, 10);
                _MachineDic.Add(i, transMachine);
            }
        }
        private void DrawCellsTopTitle(Graphics g)
        {
            Pen pen = new Pen(Color.Azure, 1);
            Point stpt = new Point(_CanvasLeftWidth, _CanvasTopHeight - 5);
            Point endpt = new Point(stpt.X + _CellColumnWidth * (_ColumnCount), stpt.Y);
            g.DrawLine(pen, stpt, endpt);
            Font font = new Font("Arial", 10, FontStyle.Regular);
 
            Pen pen2 = new Pen(Color.DeepSkyBlue, 1);
            SolidBrush fontBrush = new SolidBrush(Color.Azure);
            StringFormat drawingFormat1 = new StringFormat();
            drawingFormat1.Alignment = StringAlignment.Center;

            for (int C = 1; C <= _ColumnCount; C++)
            {
                stpt = new Point(_CanvasLeftWidth + C * _CellColumnWidth, _CanvasTopHeight - 5);
                endpt = new Point(stpt.X, stpt.Y - 8);
                g.DrawLine(pen2, stpt, endpt);
                RectangleF rect = new RectangleF(stpt.X - _CellColumnWidth, endpt.Y - 10, _CellColumnWidth, 20);
                string str = C.ToString();
                g.DrawString(str, font, fontBrush, rect, drawingFormat1);
            }
            pen.Dispose();
            pen2.Dispose();
            font.Dispose();
            fontBrush.Dispose();


        }
   
        private void  panelCells_Paint(object sender, PaintEventArgs e)
        {
            Graphics curG = e.Graphics;
            //内存绘图,解决闪烁

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            currentContext.MaximumBuffer = new Size(this.panelWareCells.Width + 1, this.panelWareCells.Height + 1);

            BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);
            Graphics g = myBuffer.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; //SmoothingMode.HighQuality;//
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.Clear(this.panelWareCells.BackColor);

            g.TranslateTransform(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y);
            foreach (CellsLayerGroup layerGroup in _LayersList)
            {
                if (layerGroup != null)
                {
                    layerGroup.PaintSelf(g);
                }
            }
            for (int i = 1; i <= _ChannelCount; i++)
            {
                _MachineDic[i].PaintSelf(g);
            }
            DrawCellsTopTitle(g);
            myBuffer.Render(curG);
            
           g.Dispose();
            myBuffer.Dispose();//释放资源
        }

        /// <summary>
        /// 根据鼠标位置（相对于panel原点）得单元格
        /// </summary>
        /// <param name="MousePt"></param>
        /// <returns></returns>
        private HouseCell GetCellByMousePt(Point MousePt)
        {
            HouseCell reCell = null;
            foreach (CellsLayerGroup layerGroup in _LayersList)
            {
                if (layerGroup.LayerRect.Contains(MousePt))
                {
                    foreach (CellsRowGroup rowGroup in layerGroup.RowsList)
                    {
                        if (rowGroup.RowRect.Contains(MousePt))
                        {
                            foreach (HouseCell cell in rowGroup.CellsList)
                            {
                                if (cell.CellRect.Contains(MousePt))
                                {
                                    reCell = cell;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            return reCell;
        }
        private void panelWareCells_MouseWheel(object sender, MouseEventArgs e)
        {
          //  this.Focus();
            if ((this.panelWareCells.VerticalScroll.Value - e.Delta )>= this.panelWareCells.VerticalScroll.Minimum &&
                ((this.panelWareCells.VerticalScroll.Value - e.Delta) <= this.panelWareCells.VerticalScroll.Maximum))
            {
                this.panelWareCells.VerticalScroll.Value -= e.Delta;
                this.panelWareCells.Update();
            }

        }

        private void panelWareCells_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panelWareCells_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void panelWareCells_MouseEnter(object sender, EventArgs e)
        {
            this.panelWareCells.Focus();
        }

        private void panelWareCells_MouseDown(object sender, MouseEventArgs e)
        {
            this._MouseDownPt.X = e.X - this.panelWareCells.AutoScrollPosition.X;
            this._MouseDownPt.Y = e.Y - this.panelWareCells.AutoScrollPosition.Y;
           
            HouseCell cellCur = GetCellByMousePt(this._MouseCurPt);
            if(cellCur != null)
            {
                cellCur.CellMouse = CellMouseStatus.MOUSE_DOWN;
                Rectangle redrawRect = new Rectangle(cellCur.ltCornerPos, cellCur.CellSize);

                redrawRect.Inflate(_CellColumnWidth , _CellRowHeight );
                redrawRect.Offset(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y);
                this.panelWareCells.Invalidate(redrawRect);
               // this.panelWareCells.Update();
               // this.pictureBox1.Refresh();
            }
            
        }

        private void panelWareCells_MouseMove(object sender, MouseEventArgs e)
        {
            
            this._MouseLastPt = this._MouseCurPt;
            this._MouseCurPt.X=e.X-this.panelWareCells.AutoScrollPosition.X;
            this._MouseCurPt.Y = e.Y - this.panelWareCells.AutoScrollPosition.Y;
           
            //搜索上次鼠标位置的单元格
            HouseCell cellLast = GetCellByMousePt(this._MouseLastPt);
            HouseCell cellCur = GetCellByMousePt(this._MouseCurPt);

            if ((cellLast != null) && (!cellLast.CellRect.Contains(_MouseCurPt)))
            {
                cellLast.CellMouse = CellMouseStatus.MOUSE_NONE;
                Rectangle redrawRect = new Rectangle(cellLast.ltCornerPos, cellLast.CellSize);

                redrawRect.Inflate(_CellColumnWidth, _CellRowHeight);
                redrawRect.Offset(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y);
                this.panelWareCells.Invalidate(redrawRect);
                // this.panelWareCells.Update();
                // this.panelWareCells.Refresh();
                //触发leave事件
                if (CellMouseLeave != null)
                {
                    CellMouseEventArgs cellE = new CellMouseEventArgs(cellLast.CellCoord, e);
                    CellMouseLeave(cellLast, cellE);
                    this._popPanel.SetDispContent(string.Empty);
                    // this._popPanel.Location = new Point(e.X + 30, e.Y);
                    this._popPanel.Hide();
                }
            }
            if(cellCur != null)
            {
                if(!cellCur.CellRect.Contains(_MouseLastPt))
                {
                    cellCur.CellMouse = CellMouseStatus.MOUSE_ENTER;
                    Rectangle redrawRect = new Rectangle(cellCur.ltCornerPos, cellCur.CellSize);

                    redrawRect.Inflate(_CellColumnWidth, _CellRowHeight);
                    redrawRect.Offset(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y);
                    this.panelWareCells.Invalidate(redrawRect);
                 //   this.panelWareCells.Update();
                    //this.panelWareCells.Refresh();
                    //触发enter事件
                    if(CellMouseEnter != null)
                    {
                        CellMouseEventArgs cellE = new CellMouseEventArgs(cellCur.CellCoord, e);
                        CellMouseEnter(cellCur, cellE);
                        string strDisp = "仓位:(层,行,列):";
                        strDisp += cellCur.CellCoord.L.ToString() + "," + cellCur.CellCoord.R.ToString() + "," + cellCur.CellCoord.C.ToString() ;
                        this._popPanel.SetDispContent(strDisp);
                        int popX = 0, popY = 0;
                        if ((e.X + 30 + this._popPanel.Width - this.panelWareCells.AutoScrollPosition.X) > this.panelWareCells.Width)
                        {
                            popX = e.X - 30 - this._popPanel.Width;
                        
                        }
                        else
                        {
                            popX = e.X;
                        }
                        if((e.Y+this._popPanel.Height-this.panelWareCells.AutoScrollPosition.Y)>this.panelWareCells.Height)
                        {
                            popY = e.Y - this._popPanel.Height-30;
                        }
                        else
                        {
                            popY = e.Y + 30;
                        }
                       this._popPanel.Location = new Point(popX,popY);
                        this._popPanel.Show();
                    }
                    
                }
            }
           if(e.X+_CellColumnWidth>=this.panelWareCells.ClientRectangle.Right)
           {

               this.panelWareCells.AutoScrollPosition = new Point(_CellColumnWidth * 2 - this.panelWareCells.AutoScrollPosition.X, 0);
                    
           }
            if(e.X-_CellColumnWidth <=this.panelWareCells.ClientRectangle.Left)
            {
                this.panelWareCells.AutoScrollPosition = new Point(-_CellColumnWidth * 2 +this.panelWareCells.AutoScrollPosition.X, 0);
            }
            if(e.Y+_CellRowHeight>=this.panelWareCells.ClientRectangle.Bottom)
            {
                this.panelWareCells.AutoScrollPosition = new Point(0,_CellRowHeight * 2 - this.panelWareCells.AutoScrollPosition.Y);
            }
            if(e.Y-CellRowHeight<=this.panelWareCells.ClientRectangle.Top)
            {
                this.panelWareCells.AutoScrollPosition = new Point(0, _CellRowHeight*2 + this.panelWareCells.AutoScrollPosition.Y);
            }
        }
        private void panelWareCells_MouseUp(object sender, MouseEventArgs e)
        {
            HouseCell cellDown = GetCellByMousePt(_MouseDownPt);
            if(cellDown != null)
            {
                HouseCell cellUp = GetCellByMousePt(this._MouseCurPt);
                if(cellUp != null &&(cellUp == cellDown))
                {
                    cellUp.CellMouse = CellMouseStatus.MOUSE_NONE;
                    Rectangle redrawRect = new Rectangle(cellUp.ltCornerPos, cellUp.CellSize);

                    redrawRect.Inflate(_CellColumnWidth, _CellRowHeight);
                    redrawRect.Offset(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y);
                    this.panelWareCells.Invalidate(redrawRect);
                 //   this.panelWareCells.Update();
                    if(CellClick != null)
                    {
                        CellMouseEventArgs cellE = new CellMouseEventArgs(cellUp.CellCoord, e);
                        CellClick(cellUp,cellE);
                    }
                }
            }
            this._MouseDownPt.X = 0;
            this._MouseDownPt.Y = 0;
        }
    }

#region 自定义事件委托
    public delegate void CellMouseEventHandler(object sender,CellMouseEventArgs e);
    public class CellMouseEventArgs:EventArgs
    {
        public readonly CellPos CurCellCoord;
        public readonly MouseEventArgs mouseArgs;
        public CellMouseEventArgs(CellPos cellCoord, MouseEventArgs mouse)
        {
            CurCellCoord = cellCoord;
            mouseArgs = mouse;
        }
    }
#endregion
}