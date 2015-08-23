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
    public enum MouseOpMode
    {
        MOUSE_SELECT = 1,
        MOUSE_DRAG = 2
    }
    // */[ToolboxBitmap(@"E:\资料\图标\示教盒图标\res\位控板.ico")]  
    public partial class WareHouseUserControl : UserControl
    {
        #region 显示控制
        /// <summary>
        /// 缩放比例
        /// </summary>
        public float zoomScale { get; set; }

        public int transX { get; set; }
        public int transY { get; set; }

        private MouseOpMode _mouseOpMode = MouseOpMode.MOUSE_SELECT;
        public MouseOpMode mouseOpMode
        {
            get
            {
                return _mouseOpMode;
            }
            set
            {
                _mouseOpMode = value;
                if (_mouseOpMode == MouseOpMode.MOUSE_DRAG)
                {
                    this.Cursor = Cursors.Hand;
                }
                else if (_mouseOpMode == MouseOpMode.MOUSE_SELECT)
                {
                    this.Cursor = Cursors.Cross;
                }
            }
        }
        #endregion
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
                foreach (CellsLayerGroup layer in _LayersList)
                {
                    layer.ChannelCount = ChannelCount;
                }
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
                foreach (CellsLayerGroup layer in _LayersList)
                {
                    layer.ColumnCount = ColumnCount;
                }
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
                foreach(CellsLayerGroup layer in _LayersList)
                {
                    layer.ChannelWidth = ChannelWidth;
                }
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
        private HouseCell _LastCaptureCell = null;
        private PopPanel _popPanel = null;
        private List<CellsLayerGroup> _LayersList;
        private Dictionary<int, TRANSMachine> _MachineDic = new Dictionary<int, TRANSMachine>();
        #endregion
        #region 鼠标事件过程数据
        private Point _MouseLastPt; //上次鼠标位置
        private Point _MouseCurPt; //当前鼠标位置
        private Point _MouseDownPt; //鼠标按下位置
        #endregion
        #region 重载的方法
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int xStep = 60;
            int yStep = 60;
            switch (keyData)
            {
                case Keys.Left:
                    {
                        translateDisp(xStep, 0);
                        break;
                    }
                case Keys.Right:
                    {
                        translateDisp(-xStep, 0);
                        break;
                    }
                case Keys.Up:
                    {
                        translateDisp(0, yStep);
                        break;
                    }
                case Keys.Down:
                    {
                        translateDisp(0, -yStep);
                        break;
                    }
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        #region 公共方法
        public WareHouseUserControl()
        {
            
            zoomScale = 100;
            transX = 0;
            transY = 0;
            //_LayerCount = LayerCount;
            //_ChannelCount = ChannelCount;
            //_ColumnCount = ColumnCount;
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
            mouseOpMode = MouseOpMode.MOUSE_SELECT;
        }

        /// <summary>
        /// 刷新所有仓位
        /// </summary>
        public void RefreshWarehouse()
        {
            this.panelWareCells.Invalidate();
            //this.panelWareCells.Update();
        }
        public void RefreshCellStoreStatus(int L,int R,int C,CellStoreStatus s)
        {
            if(L<1 || R<1 || C<1)
                return;
            _LayersList[L - 1].RowsList[R - 1].CellsList[C - 1].CellStatus = s;
            
        }
        /// <summary>
        /// 设置货架单元格状态
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="s"></param>
        public void SetCellCaptureStatus(HouseCell cell, CellMouseStatus s)
        {

            if (cell != null)
            {
                cell.CellMouse = s;
                //重绘
                Rectangle redrawRect = new Rectangle(cell.ltCornerPos, cell.CellSize);
                redrawRect.Inflate(_CellColumnWidth, _CellRowHeight);
                rectScale(zoomScale / 100.0f, ref redrawRect);
                rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref redrawRect);
               // redrawRect.Offset(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y);
                this.panelWareCells.Invalidate(redrawRect);
                switch (s)
                {
                    case CellMouseStatus.MOUSE_ENTER:
                        {
                            //触发enter事件
                            if (CellMouseEnter != null)
                            {
                                CellMouseEventArgs cellE = new CellMouseEventArgs(cell.CellCoord);
                                CellMouseEnter(cell, cellE);

                            }
                            break;
                        }
                    case CellMouseStatus.MOUSE_LEAVE:
                        {
                            //触发leave事件
                            if (CellMouseLeave != null)
                            {
                                CellMouseEventArgs cellE = new CellMouseEventArgs(cell.CellCoord);
                                CellMouseLeave(cell, cellE);

                            }
                            break;
                        }
                    case CellMouseStatus.MOUSE_NONE:
                        {
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// 放大缩小,百分比
        /// </summary>
        /// <param name="scale">百分比</param>
        public void zoomDisp(float scale)
        {
            if (scale < 50.0f)
                scale = 50.0f;
            if (scale > 200.0f)
                scale = 200.0f;
            zoomScale = scale;
            int w = (int)(( _CanvasLeftWidth + _CellRegionSize.Width + 2 * _CellColumnWidth)*scale / 100.0f);
            int h = (int)((_CanvasTopHeight + _CellRegionSize.Height + 3 * _CellRowHeight) * scale / 100.0f);
            this.panelWareCells.AutoScrollMinSize = new Size(w,h);
            this.panelWareCells.Refresh();
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="transX"></param>
        /// <param name="transY"></param>
        public void translateDisp(int transX, int transY)
        {
            //纵向
            if ((this.panelWareCells.VerticalScroll.Value - transY) <= this.panelWareCells.VerticalScroll.Minimum)
            {
                this.panelWareCells.VerticalScroll.Value = this.panelWareCells.VerticalScroll.Minimum;
            }
            else if ((this.panelWareCells.VerticalScroll.Value - transY) >= this.panelWareCells.VerticalScroll.Maximum)
            {
                this.panelWareCells.VerticalScroll.Value = this.panelWareCells.VerticalScroll.Maximum;
            }
            else
            {
                this.panelWareCells.VerticalScroll.Value -= transY;
            }

            //横向
            if (this.panelWareCells.HorizontalScroll.Value - transX <= this.panelWareCells.HorizontalScroll.Minimum)
            {
                this.panelWareCells.HorizontalScroll.Value = this.panelWareCells.HorizontalScroll.Minimum;
            }
            else if (this.panelWareCells.HorizontalScroll.Value - transX >= this.panelWareCells.HorizontalScroll.Maximum)
            {
                this.panelWareCells.HorizontalScroll.Value = this.panelWareCells.HorizontalScroll.Maximum;
            }
            else
            {
                this.panelWareCells.HorizontalScroll.Value -= transX;
            }
            this.panelWareCells.Refresh();
        }
        /// <summary>
        /// 刷新小车坐标
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="pos"></param>
        public void RefreshMachineCoord(int MachineNo, TMCoord pos)
        {
            if (pos.L < 1)
                pos.L = 1;
            if (pos.L > _LayerCount)
                pos.L = _LayerCount;

            if (pos.Ch < 1)
                pos.Ch = 1;
            if (pos.Ch > _ChannelCount)
                pos.Ch = _ChannelCount;
            if (pos.C < 1)
                pos.C = 1;
            if (pos.C > _ColumnCount)
                pos.C = _ColumnCount;
            if (_MachineDic.Count > 0)
            {
                TRANSMachine m = _MachineDic[MachineNo];

                if (m != null && (pos != m.MachinePos))
                {
                    m.CellLayer = _LayersList.ElementAt(pos.L - 1);
                    m.MachinePos = pos;
                    this.panelWareCells.Refresh();
                }
            }
        }

        /// <summary>
        /// 得到仓位中心相对于控件原点的位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Point GetCellPosition(CellPos pos)
        {
            Point cellPosition = new Point();
            cellPosition.X = _CanvasLeftWidth + _CellColumnWidth * pos.C - CellColumnWidth / 2 + AutoScrollPosition.X;
            cellPosition.Y = _CanvasTopHeight + _CellRowHeight * pos.R - CellRowHeight / 2 + AutoScrollPosition.Y;

            return cellPosition;
        }
        /// <summary>
        /// 弹出显示仓位信息
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        public void ShowCellinfoTip(CellPos pos, string title, string text)
        {
            HouseCell cell = _LayersList[pos.L - 1].RowsList[pos.R - 1].CellsList[pos.C - 1];
            int x = (int)(cell.ltCornerPos.X *zoomScale / 100.0f) + this.panelWareCells.AutoScrollPosition.X;
            int y = (int)((cell.ltCornerPos.Y + cell.CellSize.Height * 2) * zoomScale / 100.0f )+ this.panelWareCells.AutoScrollPosition.Y;
            this.toolTip1.ToolTipTitle = title;
            this.toolTip1.Show(text, this.panelWareCells, x, y);
        }

        /// <summary>
        /// 隐藏仓位信息
        /// </summary>
        public void HideCellinfoTip()
        {
            if (_LastCaptureCell != null)
            {
                _LastCaptureCell.CellMouse = CellMouseStatus.MOUSE_LEAVE;
                Refresh();
            }
            this.toolTip1.Hide(this);
        }
        #endregion
        #region 方法重载
        #endregion
        #region UI事件
        private void toolStripMenuTrans_Click(object sender, EventArgs e)
        {
            this.mouseOpMode = MouseOpMode.MOUSE_DRAG;
        }

        private void toolStripMenuSelect_Click(object sender, EventArgs e)
        {
            this.mouseOpMode = MouseOpMode.MOUSE_SELECT;
        }
        private void WareHouseControl_Load(object sender, EventArgs e)
        {

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
                int y = _CanvasTopHeight + _CellRegionSize.Height + _layerBetweenDist * (L - 1);
                layerCells.FillRows(new Point(x, y), L);

                if (_CellRegionSize.Width < layerCells.Width)
                    _CellRegionSize.Width = layerCells.Width;
                _CellRegionSize.Height += (layerCells.Height + _layerBetweenDist);

                // layerCells.Location = new Point(x, y);
                //  layerCells.FillRows(new Point(x, y), L);
                _LayersList.Add(layerCells);
            }
            this.panelWareCells.AutoScrollMinSize = new Size(_CanvasLeftWidth + _CellRegionSize.Width + 2 * _CellColumnWidth, _CanvasTopHeight + _CellRegionSize.Height + 3 * _CellRowHeight);

            #endregion
            //创建堆垛机
            for (int i = 1; i <= _ChannelCount; i++)
            {
                TRANSMachine transMachine = new TRANSMachine(i);
                transMachine.CellLayer = _LayersList.ElementAt(2);
                transMachine.ChannelIndex = i;
                Rectangle rect = new Rectangle();
                int X = transMachine.CellLayer.Location.X;
                int Y = transMachine.CellLayer.Location.Y + (i - 1) * (transMachine.CellLayer.CellRowHeight * 2 + transMachine.CellLayer.ChannelWidth) + transMachine.CellLayer.CellRowHeight;
                rect.Location = new Point(X, Y);
                rect.Width = transMachine.CellLayer.Width;
                rect.Height = _ChannelWidth;
                transMachine.WorkRect = rect;
                transMachine.MachinePos = new TMCoord(3, i, 10);
                _MachineDic.Add(i, transMachine);
            }
            //创建弹出窗体
            _popPanel = new PopPanel();
            _popPanel.TopLevel = false;
            _popPanel.ControlBox = true;

            this.panelWareCells.Controls.Add(_popPanel);

            Point OriPt = new Point(_CanvasLeftWidth, _CanvasTopHeight);

            this.PerformLayout();

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
                Rectangle layerRect = layerGroup.LayerRect;
                rectScale(zoomScale / 100.0f, ref layerRect);
                rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref layerRect);
              
                if (layerRect.Contains(MousePt))
                {
                    foreach (CellsRowGroup rowGroup in layerGroup.RowsList)
                    {
                        Rectangle rowRect = rowGroup.RowRect;
                        rectScale(zoomScale / 100.0f, ref rowRect);
                        rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref rowRect);
                      
                        if (rowRect.Contains(MousePt))
                        {
                            foreach (HouseCell cell in rowGroup.CellsList)
                            {
                                Rectangle cellRect = cell.CellRect;
                                rectScale(zoomScale / 100.0f, ref cellRect);
                                rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref cellRect);
                              
                                if (cellRect.Contains(MousePt))
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
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                //ctrl键按下
                float scale = zoomScale;
                scale -= e.Delta / 3;
                zoomDisp(scale);
            }
            else
            {
                if ((this.panelWareCells.VerticalScroll.Value - e.Delta) >= this.panelWareCells.VerticalScroll.Minimum &&
               ((this.panelWareCells.VerticalScroll.Value - e.Delta) <= this.panelWareCells.VerticalScroll.Maximum))
                {
                    this.panelWareCells.VerticalScroll.Value -= e.Delta;
                    this.panelWareCells.Refresh();
                }
            }

        }
        private void panelWareCells_MouseEnter(object sender, EventArgs e)
        {
            this.panelWareCells.Focus();
        }

    
        /// <summary>
        /// 仓库控件鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelWareCells_MouseMove(object sender, MouseEventArgs e)
        {

            this._MouseLastPt = this._MouseCurPt;
            this._MouseCurPt.X = e.X;// -this.panelWareCells.AutoScrollPosition.X;
            this._MouseCurPt.Y = e.Y;// -this.panelWareCells.AutoScrollPosition.Y;
            if (_mouseOpMode == MouseOpMode.MOUSE_SELECT)
            {
                //搜索上次鼠标位置的单元格
                HouseCell cellLast = GetCellByMousePt(this._MouseLastPt);
                HouseCell cellCur = GetCellByMousePt(this._MouseCurPt);
                if (cellLast != null)
                {
                    Rectangle lastRect = cellLast.CellRect;
                   
                    rectScale(zoomScale / 100.0f, ref lastRect);
                    rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref lastRect);
                    if (!lastRect.Contains(_MouseCurPt))
                    {  
                        SetCellCaptureStatus(cellLast, CellMouseStatus.MOUSE_LEAVE);
                    }
                }
                if (cellCur != null)
                {
                    Rectangle curRect = cellCur.CellRect;
                    rectScale(zoomScale / 100.0f, ref curRect);
                    rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref curRect);
                    if (!curRect.Contains(_MouseLastPt))
                    {
                        SetCellCaptureStatus(cellCur, CellMouseStatus.MOUSE_ENTER);
                    }
                }
            }
            else if (_mouseOpMode == MouseOpMode.MOUSE_DRAG)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int transX = _MouseCurPt.X - _MouseLastPt.X;
                    int transY = _MouseCurPt.Y - _MouseLastPt.Y;
                    translateDisp(transX, transY);
                }
            }

        }
        /// <summary>
        /// 仓库控件鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelWareCells_MouseDown(object sender, MouseEventArgs e)
        {
            this._MouseDownPt.X = e.X;
            this._MouseDownPt.Y = e.Y;
            if (_mouseOpMode == MouseOpMode.MOUSE_SELECT && e.Button == MouseButtons.Left)
            {
                HouseCell cellCur = GetCellByMousePt(this._MouseCurPt);
                if (cellCur != null)
                {
                    cellCur.CellMouse = CellMouseStatus.MOUSE_DOWN;
                    Rectangle redrawRect = new Rectangle(cellCur.ltCornerPos, cellCur.CellSize);
                    redrawRect.Inflate(_CellColumnWidth, _CellRowHeight);
                    rectScale(zoomScale / 100.0f, ref redrawRect);
                    rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref redrawRect);
                    this.panelWareCells.Invalidate(redrawRect);
                    if (this.CellClick != null)
                    {
                        CellMouseEventArgs cellE = new CellMouseEventArgs(cellCur.CellCoord);
                        CellClick.Invoke(this, cellE);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.contextMenu1.Show(this, e.X, e.Y);
            }

        }
        /// <summary>
        /// 仓库控件鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelWareCells_MouseUp(object sender, MouseEventArgs e)
        {
            HouseCell cellDown = GetCellByMousePt(_MouseDownPt);
            if (cellDown != null)
            {
                HouseCell cellUp = GetCellByMousePt(this._MouseCurPt);
                if (cellUp != null && (cellUp == cellDown))
                {
                    cellUp.CellMouse = CellMouseStatus.MOUSE_NONE;
                    Rectangle redrawRect = new Rectangle(cellUp.ltCornerPos, cellUp.CellSize);
                    redrawRect.Inflate(_CellColumnWidth, _CellRowHeight);
                    rectTranslate(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y, ref redrawRect);
                    rectScale(zoomScale / 100.0f, ref redrawRect);
                    this.panelWareCells.Invalidate(redrawRect);

                }
            }
            this._MouseDownPt.X = 0;
            this._MouseDownPt.Y = 0;
        }
        private void rectScale(float scale,ref Rectangle rect)
        {
            rect.Location = new Point((int)(rect.Left * scale), (int)(rect.Top * scale));
            rect.Width = (int)(rect.Width * scale);
            rect.Height = (int)(rect.Height * scale);
        }

        private void rectTranslate(int transX, int transY, ref Rectangle rect)
        {
            rect.Location = new Point(rect.Left + transX, rect.Top + transY);
        }
        #endregion
        #region 功能函数
        /// <summary>
        /// 绘制标题栏
        /// </summary>
        /// <param name="g"></param>
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

      
        /// <summary>
        /// 自绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCells_Paint(object sender, PaintEventArgs e)
        {
            Graphics curG = e.Graphics;
            //内存绘图,解决闪烁
           
           
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            currentContext.MaximumBuffer = new Size(this.panelWareCells.Width + 1, this.panelWareCells.Height + 1);

            BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);
            Graphics g = myBuffer.Graphics;
            g.PageUnit = GraphicsUnit.Pixel;
            g.SmoothingMode = SmoothingMode.AntiAlias; //SmoothingMode.HighQuality;//
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.Clear(this.panelWareCells.BackColor);
            g.TranslateTransform(this.panelWareCells.AutoScrollPosition.X, this.panelWareCells.AutoScrollPosition.Y);
            g.ScaleTransform(zoomScale / 100.0f, zoomScale / 100.0f);
    
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
        /// 捕获货架单元格
        /// </summary>
        /// <param name="cellCoord"></param>
        public void CaptureCell(CellPos cellCoord)
        {
            if (_LastCaptureCell != null)
            {
                SetCellCaptureStatus(_LastCaptureCell, CellMouseStatus.MOUSE_LEAVE);
                _LastCaptureCell = null;
            }
            HouseCell cell = _LayersList[cellCoord.L - 1].RowsList[cellCoord.R - 1].CellsList[cellCoord.C - 1];
            if (cell != null)
            {
                SetCellCaptureStatus(cell, CellMouseStatus.MOUSE_ENTER);
                _LastCaptureCell = cell;
            }
        }

        /// <summary>
        /// 释放单元格
        /// </summary>
        /// <param name="cellCoord"></param>
        private void ReleaseCell(CellPos cellCoord)
        {
            HouseCell cell = _LayersList[cellCoord.L - 1].RowsList[cellCoord.R - 1].CellsList[cellCoord.C - 1];
            if (cell != null)
            {
                SetCellCaptureStatus(cell, CellMouseStatus.MOUSE_LEAVE);
            }
        }
        #endregion   
    }
   
#region 自定义事件委托
    public delegate void CellMouseEventHandler(object sender,CellMouseEventArgs e);
    public class CellMouseEventArgs:EventArgs
    {
        public readonly CellPos CurCellCoord;
        //public readonly MouseEventArgs mouseArgs;
        public CellMouseEventArgs(CellPos cellCoord)
        {
            CurCellCoord = cellCoord;
           // mouseArgs = mouse;
        }
    }
#endregion
}