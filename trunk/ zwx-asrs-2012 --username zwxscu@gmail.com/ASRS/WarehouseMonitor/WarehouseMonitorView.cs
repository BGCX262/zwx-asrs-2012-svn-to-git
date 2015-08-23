using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WareHouseControl;
using System.Threading;
namespace ASRS
{
    public delegate void delegateUIRefreshPos(int MachineIndex,TMCoord  pos);
    public delegate void delegateUIRefreshMachineStatus(int MachineIndex,string strStatus);
    public delegate void delegateUIRefreshCellStatus(int L, int R, int C, CellStoreStatus s);
    public delegate void delegateUIRefreshTask(DataTable dt);
    public delegate void delegateUIRefreshInst(int vehicleIndex,DataTable dt);
    public partial class WarehouseMonitorView : viewBase,IWarehouseMonitorView
    {
        private ASRSModel _model = ASRSModel.GetInstance();
        #region  手动增加的界面部分
        private MyPanel panelLeftHouseOut = null;
        private Button buttonHouseoutAdd = null;
        private Button buttonHouseoutDel = null;
        private Button buttonHouseoutClear = null;
        private Button buttonHouseoutClose = null;
        private Button buttonHouseoutApply = null;
        private System.Windows.Forms.DataGridView dataGridViewHouseout = null;
        private DataTable _dataTableHouseout = null;
 
        /// <summary>
        /// 手动出库是否允许
        /// </summary>
        private bool _mannualHouseOutEnable = false;

        /// <summary>
        /// 手动模式按钮图片
        /// </summary>
        private Image _imageModeMannual = null;

        /// <summary>
        /// 自动模式按钮图片
        /// </summary>
        private Image _imageModeAuto = null;

        /// <summary>
        /// 启动按钮图片
        /// </summary>
        private Image _imageStart = null;

        /// <summary>
        /// 暂停按钮图片
        /// </summary>
        private Image _imagePause = null;

        /// <summary>
        /// 停止按钮图片
        /// </summary>
        private Image _imageStop = null;

        /// <summary>
        /// 停止按钮禁用提示图片
        /// </summary>
        private Image _imageStopBusy = null;
        /// <summary>
        /// 左侧收缩量(点击收缩按钮时）
        /// </summary>
        private int _leftShrink = 107;

        /// <summary>
        /// 顶部收缩量（点击收缩按钮时)
        /// </summary>
        private int _topShrink = 60;
        /// <summary>
        /// 顶部收缩标志：1:原来大小，2：收缩
        /// </summary>
        private byte _topShrinkClass = 1;

        /// <summary>
        /// 左侧工具栏收缩标志：1:原来大小，2：收缩
        /// </summary>
        private byte _leftShrinkClass = 1;
        #endregion
        /// <summary>
        /// 父窗口的接口
        /// </summary>
        public IASRSViewComn IParentView { get; set; }
        public WarehouseMonitorView(IASRSViewComn IParent)
        {
            //启动线程
            //Thread loadThread = new Thread(LoadThreadWork);
          //  loadThread.Start();
            InitializeComponent();
            CreateHouseoutPanel();
           
          
            IParentView = IParent;
           
            //启动线程退出
          //  loadThread.Abort();
        }
        #region 接口实现
        public event EventHandler eventStartPause;
        public event EventHandler eventExit;
        public event EventHandler eventInit;
        public event EventHandler eventPause;
        public event EventHandler eventStop;
        public event EventHandler eventSwitchMode;
        public event EventHandler<WareHouseAttrModifyEventArgs> eventWHouseAttrModify;
        /// <summary>
        /// 仓位信息显示事件
        /// </summary>
        public event EventHandler<CellEventArgs> eventCellPopupDisp;

        /// <summary>
        /// 单仓位出库请求，设置某仓位手动出库时，先发送此事件以求证是否允许
        /// </summary>
       public event EventHandler<CellEventArgs> eventHouseoutRequire;

        /// <summary>
        /// 手动出库应用的事件，包含了出库仓位的列表
        /// </summary>
        public event EventHandler<CellHouseoutListEventArgs> eventHouseoutApply;

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="mes"></param>
        public void OutputMessage(string[] mes)
        {
            IParentView.OutputMessage(mes);
        }
        /// <summary>
        /// 跨线程托管：刷新小车位置
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="pos"></param>
        private void ThreadUIRefreshMachineCoord(int MachineNo, TMCoord pos)
        {
            this.wareHouseControl1.RefreshMachineCoord(MachineNo, pos);
            if(MachineNo == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("层:{0},巷道:1,列:{1}", pos.L, pos.C);
                this.labelMachinePos1.Text = str.ToString();
            }
            else if(MachineNo == 2)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("层:{0},巷道:2,列:{1}", pos.L, pos.C);
                this.labelMachinePos2.Text = str.ToString();
            }
        }
        public void RefreshWarehouse()
        {
            this.wareHouseControl1.RefreshWarehouse();
        }
        /// <summary>
        /// 刷新小车位置
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="pos"></param>
        public void RefreshMachineCoord(int MachineNo, TMCoord  pos)
        {
            if(this.wareHouseControl1.InvokeRequired)
            {
                this.Invoke(new delegateUIRefreshPos(ThreadUIRefreshMachineCoord), MachineNo, pos);
            }
            else
            {
                ThreadUIRefreshMachineCoord(MachineNo, pos);
            }
        }

        /// <summary>
        /// 跨线程托管：刷新小车状态
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="strStatus"></param>
        private  void ThreadUIRefreshMachineStatus(int MachineNo,string strStatus)
        {
            if(MachineNo == 1)
            {
                this.labelCurMachineStatus1.Text = strStatus;
            }
            else if(MachineNo == 2)
            {
                this.labelCurMachineStatus2.Text = strStatus;
            }
        }

        /// <summary>
        /// 刷新小车状态
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="strStatus"></param>
        public void RefreshMachineStatus(int MachineNo, string strStatus)
        {
            if(this.wareHouseControl1.InvokeRequired)
            {
                this.Invoke(new delegateUIRefreshMachineStatus(ThreadUIRefreshMachineStatus), MachineNo, strStatus);
            }
            else
            {
                ThreadUIRefreshMachineStatus(MachineNo, strStatus);
            }
        }

        private void ThreadUIRefreshMachineTask(int MachineNo,string strTask)
        {
            if(MachineNo == 1)
            {
                this.labelCurTask1.Text = strTask;
            }
            else if(MachineNo == 2)
            {
                this.labelCurTask2.Text = strTask;
            }
        }
        
        /// <summary>
        /// 刷新小车任务显示
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="strTask"></param>
        public void RefreshMachineTask(int MachineNo,string strTask)
        {
            if (this.wareHouseControl1.InvokeRequired)
            {
                this.Invoke(new delegateUIRefreshMachineStatus(ThreadUIRefreshMachineTask), MachineNo, strTask);
            }
            else
            {
                ThreadUIRefreshMachineTask(MachineNo, strTask);
            }
        }
        private void ThreadUIRefreshCellStatus(int L, int R, int C, CellStoreStatus s)
        {

            this.wareHouseControl1.RefreshCellStoreStatus(L, R, C, s);
        }

        /// <summary>
        /// 刷新仓位状态
        /// </summary>
        /// <param name="L"></param>
        /// <param name="R"></param>
        /// <param name="C"></param>
        /// <param name="s"></param>
        public void RefreshHouseCellStatus(int L, int R, int C, CellStoreStatus s)
        {
            if(this.wareHouseControl1.InvokeRequired)
            {
                this.Invoke(new delegateUIRefreshCellStatus(ThreadUIRefreshCellStatus), L, R, C, s);
            }
            else
            {
                ThreadUIRefreshCellStatus(L, R, C, s);
            }
        }

        /// <summary>
        /// 仓位信息弹出式显示
        /// </summary>
        /// <param name="cellInfo"></param>
        public void PopupDispCellinfo(CellPos pos,CellDispInfo cellInfo)
        {
            
            this.wareHouseControl1.ShowCellinfoTip(pos, cellInfo.titleText, cellInfo.contentText);
        }

        /// <summary>
        /// 刷新工作模式：手动或自动
        /// </summary>
        /// <param name="mode"></param>
       public  void RefreshWorkMode(WorkMode mode)
        {
           if(mode == WorkMode.MODE_AUTO)
           {
               this.pictureBoxWorkmode.Image = _imageModeAuto;
               this.labelMode.Text = "自动";
           }
           else if(mode == WorkMode.MODE_MANUAL)
           {
               this.pictureBoxWorkmode.Image = _imageModeMannual;
               this.labelMode.Text = "手动";
           }
        }

       /// <summary>
       /// 刷新生产线状态
       /// </summary>
       /// <param name="lineStatus">生产线状态:启动，暂停，停止</param>
       public void RefreshLineStatus(EnumLineRunStatus lineStatus)
       {
           if(lineStatus == EnumLineRunStatus.LINE_RUN)
           {
               this.pictureBoxStart.Image = _imageStart;
               this.labelStartPause.Text = "运行";
               this.pictureBoxStop.Image = _imageStop;
               this.pictureBoxStop.Enabled = true;
           }
           else if(lineStatus == EnumLineRunStatus.LINE_PAUSE)
           {
               this.pictureBoxStart.Image = _imagePause;
               this.labelStartPause.Text = "暂停";
               this.pictureBoxStop.Image = _imageStop;
               this.pictureBoxStop.Enabled = true;
           }
           else if(lineStatus == EnumLineRunStatus.LINE_STOP)
           {
               this.pictureBoxStop.Image = _imageStop;
               this.pictureBoxStop.Image = _imageStopBusy;
               this.pictureBoxStop.Enabled = false;
           }
       }

       /// <summary>
       /// 增加出库列表项，对出库请求的应答，若通过则会调用此接口，实现列表项增加
       /// </summary>
       /// <param name="houseID"></param>
       /// <param name="cellL"></param>
       /// <param name="cellR"></param>
       /// <param name="cellC"></param>
       public void HouseoutListitemAdd(int houseID, int cellL, int cellR, int cellC)
       {
           DataRow row = _dataTableHouseout.NewRow();
           row["仓位ID"] = houseID;
           row["层号"] = cellL;
           row["行号"] = cellR;
           row["列号"] = cellC;
           _dataTableHouseout.Rows.Add(row);
       }

       /// <summary>
       /// 弹出提示框，一般用来提示，或者需要用户的确认
       /// </summary>
       /// <param name="titleText">标题信息</param>
       /// <param name="contentText">正文信息</param>
       /// <param name="buttonNum">显示按钮个数：1：只有确认按钮，
       /// 2："是，否"两个按钮，3："是，否，取消"3个按钮</param>
      ///< return> 0:确定，1：是，2：否，3：取消 </return>
       public int ShowReMessageBox(string titleText, string contentText, int buttonNum)
       {
           if(buttonNum == 1)
           {
               MessageBox.Show(contentText, titleText, MessageBoxButtons.OK);
           }
           else if(buttonNum == 2)
           {
               MessageBox.Show(contentText, titleText, MessageBoxButtons.YesNo);
           }
           else if(buttonNum == 3)
           {
               MessageBox.Show(contentText, titleText, MessageBoxButtons.YesNoCancel);
           }
           return 0;
       }

       /// <summary>
       /// 入库列表请求结果应答
       /// </summary>
       /// <param name="re"> 0:列表指令成功添加，>0：第一个有误的列表项序号</param>
       public void ResHouseoutListApply(int re)
       {
           if(re>0)
           {
               StringBuilder strBuild = new StringBuilder();
               strBuild.AppendFormat("出库指令列表项{0}有误!", re);
               MessageBox.Show(strBuild.ToString());
           }
           else if(re == 0)
           {
               _dataTableHouseout.Rows.Clear();
               MessageBox.Show("出库指令列表已经生效!");
           }
       }
       /// <summary>
       /// 显示初始化进度
       /// </summary>
       /// <param name="progress"></param>
       public void DispInitProgress(int progress)
       {
           IParentView.ShowStatusbarProgressbar(true);
           IParentView.DispStatusbarProgressInfo(progress);
           if(progress >=100)
           {
               this.pictureBoxStart.Enabled = true;
             //  this.pictureBoxStop.Enabled = false;
               this.pictureBoxWorkmode.Enabled = true;
               IParentView.ShowStatusbarProgressbar(false);
           }
       }

       /// <summary>
       /// 显示状态信息
       /// </summary>
       /// <param name="index"></param>
       /// <param name="str"></param>
       public void DispStatusbarInfo(int index, string str)
       {
           IParentView.DispStatusbarLabelInfo(index, str);
       }

        private void ThreadUIRefreshTaskList(DataTable dt)
        {
            this.dataGridViewTask.DataSource = dt;
        }
       /// <summary>
       /// 刷新任务列表
       /// </summary>
       /// <param name="dt"></param>
       public void RefreshTaskDisp(DataTable dt)
       {
          if(this.dataGridViewTask.InvokeRequired)
          {
              this.Invoke(new delegateUIRefreshTask(ThreadUIRefreshTaskList), dt);
          }
       }
       private void ThreadUIRefreshInstList(int machineIndex,DataTable dt)
       {
           if (machineIndex == 1)
           {
               this.dataGridViewInst1.DataSource = dt;
           }
           else if (machineIndex == 2)
           {
               this.dataGridViewInst2.DataSource = dt;
           }
       }
       /// <summary>
       /// 摔性能指令列表
       /// </summary>
       /// <param name="machineIndex">小车编号，从1开始</param>
       public void RefreshInstDisp(int machineIndex,DataTable dt)
       {
           if(this.dataGridViewInst1.InvokeRequired || this.dataGridViewInst2.InvokeRequired)
           {
               this.Invoke(new delegateUIRefreshInst(ThreadUIRefreshInstList), machineIndex, dt);
           }
       }
        #endregion
        #region 事件触发

        /// <summary>
        /// 触发eventStart事件
        /// </summary>
        private void OnStartPause()
        {
            if(eventStartPause != null)
            {
                eventStartPause.Invoke(this, null);
            }
           
        }

        /// <summary>
        /// 触发初始化事件
        /// </summary>
        public void OnInit()
        {
            if(eventInit != null)
            {
                eventInit.Invoke(this, null);
            }
        }
        /// <summary>
        /// 触发eventExit事件
        /// </summary>
        public void OnExit()
        {
            if(eventExit != null)
            {
                eventExit.Invoke(this, null);
            }
        }
   
        /// <summary>
        /// 触发停止事件
        /// </summary>
        private void OnStop()
        {
            if (eventStop != null)
            {
                eventStop.Invoke(this, null);
            }
        }

        /// <summary>
        /// 触发切换工作模式事件
        /// </summary>
        private void OnSwitchWorkMode()
        {
            if(eventSwitchMode != null)
            {
                eventSwitchMode.Invoke(this,null);
            }
        }

        /// <summary>
        /// 触发仓位信息弹出显示事件
        /// </summary>
        /// <param name="pos"></param>
        private void OnCellPopupDisp(CellPos pos)
        {
            if(eventSwitchMode != null)
            {
                CellEventArgs eArgs = new CellEventArgs(pos);
                eventCellPopupDisp.Invoke(this, eArgs);
            }
        }

        /// <summary>
        /// 触发入库请求查询事件
        /// </summary>
        /// <param name="pos"></param>
        private void OnHouseoutRequire(CellPos pos)
        {
            if(eventHouseoutRequire != null)
            {
                CellEventArgs eArgs = new CellEventArgs(pos);
                eventHouseoutRequire.Invoke(this, eArgs);
            }
            
        }

        /// <summary>
        /// 触发入库列表生效事件
        /// </summary>
        /// <param name="cellList"></param>
        private void OnHouseoutListApply(IList<CellPos> cellList)
        {
            if(eventHouseoutApply != null)
            {
                CellHouseoutListEventArgs eArgs = new CellHouseoutListEventArgs(cellList);
                eventHouseoutApply.Invoke(this, eArgs);
            }
        }
        #endregion
        #region UI事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehouseMonitorView_Load(object sender, EventArgs e)
        {
           ASRSModel model = ASRSModel.GetInstance();
            
            //this.Hide();
            //加载控制台按钮图片资源
            try
            {
                string picPath = ASRSModel.appBaseDirectory + @"..\Resources\autoMode.ico";
                _imageModeAuto = Image.FromFile(picPath);
                this.pictureBoxWorkmode.Image = _imageModeAuto;
                picPath = ASRSModel.appBaseDirectory + @"..\Resources\mannualMode.ico";
                _imageModeMannual = Image.FromFile(picPath);
                picPath = ASRSModel.appBaseDirectory + @"..\Resources\start1.ico";
                _imageStart = Image.FromFile(picPath);
                this.pictureBoxStart.Image = _imageStart;
                picPath = ASRSModel.appBaseDirectory + @"..\Resources\pause1.ico";
                _imagePause = Image.FromFile(picPath);
                picPath = ASRSModel.appBaseDirectory + @"..\Resources\stop1.ico";
                _imageStop = Image.FromFile(picPath);
                picPath = ASRSModel.appBaseDirectory + @"..\Resources\stopBusy.ico";
                _imageStopBusy = Image.FromFile(picPath);
                this.pictureBoxStop.Image = _imageStopBusy;

            }
            catch (System.Exception e1)
            {
         
                MessageBox.Show("加载控制台位图资源失败：" + e1.Message);
                OnExit();
            }
            //this.wareHouseControl1.LayerCount = model.wareHouseSet.layerCount;
            //this.wareHouseControl1.ChannelCount = model.wareHouseSet.channelCount;
            //this.wareHouseControl1.ColumnCount = model.wareHouseSet.columnCount;
        
            OnInit(); //UI加载完成后触发初始化事件
            //快速定位控件填充数据
            for (int L = 1; L <= this.wareHouseControl1.LayerCount; L++)
            {
                this.comboBoxLayerlist.Items.Add(L);
            }
            for (int R = 1; R <= this.wareHouseControl1.ChannelCount * 2; R++)
            {
                this.comboBoxRowlist.Items.Add(R);
            }
            for (int C = 1; C <= this.wareHouseControl1.ColumnCount; C++)
            {
                this.comboBoxColumnList.Items.Add(C);
            }
            this.comboBoxLayerlist.SelectedIndex = 0;
            this.comboBoxRowlist.SelectedIndex = 0;
            this.comboBoxColumnList.SelectedIndex = 0;

            this.pictureBoxStart.Enabled = false;
            this.pictureBoxStop.Enabled = false;
            this.pictureBoxWorkmode.Enabled = false;
            
            //主窗口显示状态：最大化
            this.WindowState = FormWindowState.Maximized;
            
        }

        private void WarehouseMonitorView_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnExit();
        }

        /// <summary>
        /// 添加手动出库指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHouseoutAdd_Click(object sender, EventArgs e)
        {
            int L = int.Parse(this.comboBoxLayerlist.Text);
            if (L < 1)
            {
                L = 1;
                this.comboBoxLayerlist.Text = L.ToString();
            }
            if (L > this.wareHouseControl1.LayerCount)
            {
                L = this.wareHouseControl1.LayerCount;
                this.comboBoxLayerlist.Text = L.ToString();
            }
            int R = int.Parse(this.comboBoxRowlist.Text);
            if (R < 1)
            {
                R = 1;
                this.comboBoxRowlist.Text = R.ToString();
            }
            if (R > this.wareHouseControl1.ChannelCount * 2)
            {
                R = this.wareHouseControl1.ChannelCount * 2;
                this.comboBoxRowlist.Text = R.ToString();
            }
            int C = int.Parse(this.comboBoxColumnList.Text);// (int)this.comboBoxColumnList.SelectedItem;
            if (C < 1)
            {
                C = 1;
                this.comboBoxColumnList.Text = C.ToString();
            }
            if (C > this.wareHouseControl1.ColumnCount)
            {
                C = this.wareHouseControl1.ColumnCount;
                this.comboBoxColumnList.Text = C.ToString();
            }
            CellPos pos = new CellPos(L, R, C);
            OnHouseoutRequire(pos);
        }

        /// <summary>
        /// 删除手动出库指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHouseoutDel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 清除所有手动出库指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHouseoutClearAll_Click(object sender, EventArgs e)
        {
            _dataTableHouseout.Rows.Clear();
        }

        /// <summary>
        /// 手动出库列表应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHouseoutListApply_Click(object sender, EventArgs e)
        {
            List<CellPos> cellList = new List<CellPos>();
            foreach (DataRow row in _dataTableHouseout.Rows)
            {

                CellPos pos = new CellPos((int)row["层号"], (int)row["行号"], (int)row["列号"]);
                cellList.Add(pos);
            }
            OnHouseoutListApply(cellList);
        }
  

        /// <summary>
        /// 仓库监控控件鼠标进入货架单元的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warehouse_mouseEnter(object sender, CellMouseEventArgs e)
        {
            this.comboBoxLayerlist.SelectedItem = e.CurCellCoord.L;
            this.comboBoxRowlist.SelectedItem = e.CurCellCoord.R;
            this.comboBoxColumnList.SelectedItem = e.CurCellCoord.C;
        }
      
     

        
        
        /// <summary>
        /// 查看指定单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCellView_Click(object sender, EventArgs e)
        {
            int L = int.Parse(this.comboBoxLayerlist.Text);
            if (L < 1)
            {
                L = 1;
                this.comboBoxLayerlist.Text = L.ToString();
            }
            if (L > this.wareHouseControl1.LayerCount)
            {
                L = this.wareHouseControl1.LayerCount;
                this.comboBoxLayerlist.Text = L.ToString();
            }
            int R = int.Parse(this.comboBoxRowlist.Text);
            if (R < 1)
            {
                R = 1;
                this.comboBoxRowlist.Text = R.ToString();
            }
            if (R > this.wareHouseControl1.ChannelCount * 2)
            {
                R = this.wareHouseControl1.ChannelCount * 2;
                this.comboBoxRowlist.Text = R.ToString();
            }
            int C = int.Parse(this.comboBoxColumnList.Text);// (int)this.comboBoxColumnList.SelectedItem;
            if (C < 1)
            {
                C = 1;
                this.comboBoxColumnList.Text = C.ToString();
            }
            if (C > this.wareHouseControl1.ColumnCount)
            {
                C = this.wareHouseControl1.ColumnCount;
                this.comboBoxColumnList.Text = C.ToString();
            }
            // wareHouseControl1_DisplayCell(new CellPos(L, R, C));
            CellPos pos = new CellPos(L, R, C);
            this.wareHouseControl1.CaptureCell(pos);
            OnCellPopupDisp(pos);
        }

  
        /// <summary>
        /// 货架单元格的鼠标进入事件,触发仓位信息弹出显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wareHouseControl1_CellMouseEnter(object sender, CellMouseEventArgs e)
        {
            //wareHouseControl1_DisplayCell(e.CurCellCoord);
            OnCellPopupDisp(e.CurCellCoord);
        }

        /// <summary>
        /// 货架单元格鼠标离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wareHouseControl1_CellMouseLeave(object sender, CellMouseEventArgs e)
        {
            this.wareHouseControl1.HideCellinfoTip();
        }

        /// <summary>
        /// 货架单元格鼠标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wareHouseControl1_CellClick(object sender, CellMouseEventArgs e)
        {
            this.comboBoxLayerlist.SelectedIndex = e.CurCellCoord.L - 1;
            this.comboBoxRowlist.SelectedIndex = e.CurCellCoord.R - 1;
            this.comboBoxColumnList.SelectedIndex = e.CurCellCoord.C - 1;
        }

        /// <summary>
        /// 取消查看仓位信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCellViewCancel_Click(object sender, EventArgs e)
        {
            this.wareHouseControl1.HideCellinfoTip();
        }

        private void pictureBoxWorkmode_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxWorkmode.BackColor = Color.LightSteelBlue;
            this.pictureBoxWorkmode.Width += 4;
            this.pictureBoxWorkmode.Height += 4;
        }

        private void pictureBoxWorkmode_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxWorkmode.BackColor = Color.Transparent;
            this.pictureBoxWorkmode.Width -= 4;
            this.pictureBoxWorkmode.Height -= 4;
        }

        private void pictureBoxWorkmode_Click(object sender, EventArgs e)
        {

            OnSwitchWorkMode();

        }

        private void pictureBoxStart_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxStart.BackColor = Color.LightSteelBlue;
            this.pictureBoxStart.Width += 4;
            this.pictureBoxStart.Height += 4;
        }

        private void pictureBoxStart_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxStart.BackColor = Color.Transparent;
            this.pictureBoxStart.Width -= 4;
            this.pictureBoxStart.Height -= 4;
        }

        private void pictureBoxStart_Click(object sender, EventArgs e)
        {
            OnStartPause();
        }

        private void pictureBoxStop_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxStop.BackColor = Color.LightSteelBlue;
            this.pictureBoxStop.Width += 4;
            this.pictureBoxStop.Height += 4;
        }

        private void pictureBoxStop_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxStop.BackColor = Color.Transparent;
            this.pictureBoxStop.Width -= 4;
            this.pictureBoxStop.Height -= 4;
        }

        private void pictureBoxStop_Click(object sender, EventArgs e)
        {
            OnStop();
        }
#endregion
        #region 公共功能函数
        public void zoomDisp(float scale)
        {
            this.wareHouseControl1.zoomDisp(scale);
        }
        /// <summary>
        /// 显示手动出库界面
        /// </summary>
        public void ShowHouseoutPanel()
        {
           
            
        }
        #endregion
        #region 私有功能函数
        /// <summary>
        /// 加载对话框提示
        /// </summary>
        private void LoadThreadWork()
        {
            //
            LoadForm fm = new LoadForm();
            fm.labelTipText = "正在初始化仓位信息...";
            fm.ShowDialog();

        }
        /// <summary>
        /// 创建手动出库panel界面
        /// </summary>
        private void CreateHouseoutPanel()
        {
            //数据表
            _dataTableHouseout = new DataTable("dataTableHouseout");
            _dataTableHouseout.Columns.AddRange(new DataColumn[] { 
                                                                                    new DataColumn("仓位ID",typeof(int)),
                                                                                    new DataColumn("层号",typeof(int)),
                                                                                    new DataColumn("行号",typeof(int)),
                                                                                    new DataColumn("列号",typeof(int))});

            this.panelLeftHouseOut = new MyPanel();
            this.panelLeftHouseOut.BorderStyle = BorderStyle.FixedSingle;
            this.panelLeftHouseOut.SuspendLayout();
            this.panelLeftHouseOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeftHouseOut.AutoScroll = true;
            this.panelLeftHouseOut.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
          
           // this.panelLeft.Controls.Add(this.panelLeftHouseOut);
            this.panelLeftHouseOut.Location = new System.Drawing.Point(0, 145);
            this.panelLeftHouseOut.Size = new System.Drawing.Size(124, 265);
            this.panelLeftHouseOut.Name = "panelLeftHouseOut";

            //buttonHouseoutAdd
            this.buttonHouseoutAdd = new Button();
            this.panelLeftHouseOut.Controls.Add(this.buttonHouseoutAdd);
            this.buttonHouseoutAdd.Location = new System.Drawing.Point(1, 2);
            this.buttonHouseoutAdd.Name = "buttonHouseoutAdd";
            this.buttonHouseoutAdd.Size = new System.Drawing.Size(40, 25);
            this.buttonHouseoutAdd.Text = "添加";
            this.buttonHouseoutAdd.UseVisualStyleBackColor = true;
            this.buttonHouseoutAdd.Click += buttonHouseoutAdd_Click;

            //buttonHouseoutDel
            this.buttonHouseoutDel = new Button();
            this.panelLeftHouseOut.Controls.Add(this.buttonHouseoutDel);
            this.buttonHouseoutDel.Location = new System.Drawing.Point(42, 2);
            this.buttonHouseoutDel.Name = "buttonHouseoutDel";
            this.buttonHouseoutDel.Size = new System.Drawing.Size(40, 25);
            this.buttonHouseoutDel.Text = "删除";
            this.buttonHouseoutDel.UseVisualStyleBackColor = true;
            this.buttonHouseoutDel.Click += buttonHouseoutDel_Click;
            // //buttonHouseoutClear
            this.buttonHouseoutClear = new Button();
            this.panelLeftHouseOut.Controls.Add(this.buttonHouseoutClear);
            this.buttonHouseoutClear.Location = new System.Drawing.Point(83, 2);
            this.buttonHouseoutClear.Name = "buttonHouseoutClear";
            this.buttonHouseoutClear.Size = new System.Drawing.Size(40, 25);
            this.buttonHouseoutClear.Text = "清空";
            this.buttonHouseoutClear.UseVisualStyleBackColor = true;
            this.buttonHouseoutClear.Click += buttonHouseoutClearAll_Click;

            //buttonHouseoutApply
            this.buttonHouseoutApply = new Button();
            this.buttonHouseoutApply.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.panelLeftHouseOut.Controls.Add(this.buttonHouseoutApply);
            this.buttonHouseoutApply.Location = new Point(2, 230);
            this.buttonHouseoutApply.Name = "buttonHouseoutApply";
            this.buttonHouseoutApply.Size = new System.Drawing.Size(40, 30);
            this.buttonHouseoutApply.Text = "应用";
            this.buttonHouseoutApply.Click += buttonHouseoutListApply_Click;
            //buttonHouseoutClose
            this.buttonHouseoutClose = new Button();
            this.buttonHouseoutClose.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.panelLeftHouseOut.Controls.Add(this.buttonHouseoutClose);
            this.buttonHouseoutClose.Location = new System.Drawing.Point(80, 230);
            this.buttonHouseoutClose.Name = "buttonHouseoutClose";
            this.buttonHouseoutClose.Size = new System.Drawing.Size(40, 30);
            this.buttonHouseoutClose.Text = "返回";
            this.buttonHouseoutClose.UseVisualStyleBackColor = true;
          

            //dataGridViewHouseout 

            dataGridViewHouseout = new DataGridView();
            this.panelLeftHouseOut.Controls.Add(dataGridViewHouseout);
            this.dataGridViewHouseout.DataSource = _dataTableHouseout;
            this.dataGridViewHouseout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHouseout.Enabled = false;
            this.dataGridViewHouseout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))); ;
            this.dataGridViewHouseout.Location = new System.Drawing.Point(1, 30);
            this.dataGridViewHouseout.Name = "dataGridView2";
            this.dataGridViewHouseout.RowTemplate.Height = 23;
            this.dataGridViewHouseout.Size = new System.Drawing.Size(122, 200);
            this.dataGridViewHouseout.RowHeadersVisible = false;
            foreach (DataGridViewColumn col in this.dataGridViewHouseout.Columns)
            {
                col.Width = 30;
            }

            this.panelLeftHouseOut.ResumeLayout(false);
            this.panelLeftHouseOut.PerformLayout();
            this.panelLeftHouseOut.Hide();
        }
        protected override object CreatePresenter()
        {
            return new WarehouseMonitorPresenter(this);
        }
        #endregion


        private void expandableSplitter1_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            if (this.splitContainer1.Panel1Collapsed == false)
            {
                expandableSplitter1.Left = 200;
                splitContainer1.Panel1Collapsed = true;
            }
            else
            {
                expandableSplitter1.Left = 25;
                splitContainer1.Panel1Collapsed = false;
            }
        }
    }
}
