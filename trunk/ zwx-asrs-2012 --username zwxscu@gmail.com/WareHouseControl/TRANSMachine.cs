using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace WareHouseControl
{
    public struct TMCoord
    {
        public TMCoord(int l, int ch, int c)
        { this._L = l; this._Ch = ch; this._C = c; }
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
        /// 巷道序号，从1开始编号
        /// </summary>
        private int _Ch;
        public int Ch
        {
            get
            {
                return _Ch;
            }
            set
            {
                _Ch = value;
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
        public static bool operator!=(TMCoord first,TMCoord second)
        {
            if (first.L == second.L && (first.Ch == second.Ch) && (first.C == second.C))
                return false;
            else
                return true;
           
        }
        public static bool operator == (TMCoord first,TMCoord second)
        {
            if (first.L == second.L && (first.Ch == second.Ch) && (first.C == second.C))
                return true;
            else
                return false;
        }
    }
    /// <summary>
    /// 堆垛机类
    /// </summary>
    class TRANSMachine
    {
#region 属性及字段
        /// <summary>
        /// 小车编号,从1开始
        /// </summary>
        private int _machineNo = 1;
        public int machineNo
        {
            get
            {
                return _machineNo;
            }
        }

        /// <summary>
        /// 所在的货架层对象，通过该对象获得货架单元格的信息
        /// </summary>
        private CellsLayerGroup _CellLayer = null;
        public CellsLayerGroup CellLayer
        {
            get
            {
                return _CellLayer;
            }
            set
            {
                _CellLayer = value;
            }
        }
        
        /// <summary>
        /// 所在巷道编号,从1开始
        /// </summary>
        private int _ChannelIndex = 1;
        public int ChannelIndex
        {
            get
            {
                return _ChannelIndex;
            }
            set
            {
                _ChannelIndex = value;
                if (_ChannelIndex < 1)
                    _ChannelIndex = 1;
            }
        }

        /// <summary>
        /// 堆垛机的活动范围
        /// </summary>
        private Rectangle _WorkRect = new Rectangle();
        public Rectangle WorkRect
        {
            get
            {
                return _WorkRect;
            }
            set
            {
                _WorkRect = value;

            }
        }

        private TMCoord _MachinePos = new TMCoord(1,1,1);
        public TMCoord MachinePos
        {
            get
            {
                return _MachinePos;
            }
            set
            {
                _MachinePos = value;
                Rectangle newRect = new Rectangle();
                newRect.X = CellLayer.Location.X;
                newRect.Y = CellLayer.Location.Y + (CellLayer.CellRowHeight * 2 + CellLayer.ChannelWidth) * (_MachinePos.Ch - 1) + CellLayer.CellRowHeight;
                newRect.Width = CellLayer.Width;
                newRect.Height = CellLayer.ChannelWidth;
                _WorkRect = newRect;
                
            }
        }

        /// <summary>
        /// 轨道颜色
        /// </summary>
        private Color _RailColor = Color.Azure;
        public Color RailColor
        {
            get
            {
                return _RailColor;
            }
            set
            {
                _RailColor = value;
            }
        }

        /// <summary>
        /// 堆垛机本体颜色
        /// </summary>
        private Color _MachineColor = Color.CadetBlue;
        public Color MachineColor
        {
            get
            {
                return _MachineColor;
            }
            set
            {
                _MachineColor = value;
            }
        }
#endregion

        public void PaintSelf(Graphics g)
        {
            PointF CenterPt = new PointF(_WorkRect.X, _WorkRect.Y + _WorkRect.Height / 2.0f);
            int ySteps = 10;
            float yStepHeight = (float)_WorkRect.Height / (float)ySteps;
            //轨道
            float railHeight = 6.0f;//_WorkRect.Height * 0.2f;// 
            SolidBrush railBrush = new SolidBrush(_RailColor);
            Pen pen1 = new Pen(Color.Black, 2.0f);
            RectangleF railRect1 = new RectangleF(CenterPt.X,CenterPt.Y-yStepHeight*ySteps*0.3f,_WorkRect.Width,railHeight);
            g.FillRectangle(railBrush, railRect1);
            PointF stpt = new PointF(railRect1.X, railRect1.Y + 0.5f * railRect1.Height);
            PointF endpt = new PointF(stpt.X + railRect1.Width, stpt.Y);
            g.DrawLine(pen1,stpt,endpt);

            RectangleF railRect2 = new RectangleF();
            railRect2 = railRect1;

            railRect2.Offset(new PointF(0,(CenterPt.Y-railRect1.Y)*2.0f-railHeight));
            g.FillRectangle(railBrush, railRect2);
            stpt.Y =railRect2.Y +0.5f * railRect2.Height;
            endpt.Y = stpt.Y;
             g.DrawLine(pen1,stpt,endpt);

            //车体
            float MachineHeight = yStepHeight * ySteps * 0.8f;
            float MachineWidth = _CellLayer.CellColumnWidth;
            PointF MachineCenterPt = new PointF(CenterPt.X + (MachinePos.C - 1 + 0.5f) * _CellLayer.CellColumnWidth, CenterPt.Y);
            SolidBrush MachineBrush1 = new SolidBrush(_MachineColor);
            RectangleF MachineRect1 = new RectangleF(MachineCenterPt.X - MachineWidth * 0.5f, MachineCenterPt.Y - MachineHeight * 0.5f, MachineWidth, MachineHeight);

            SolidBrush MachineBrush2 = new SolidBrush(Color.Black);
            RectangleF MachineRect2 = new RectangleF();
            MachineRect2 = MachineRect1;
            MachineRect2.Inflate(-3, -3);
            
            
            g.FillRectangle(MachineBrush1, MachineRect1);
            g.FillRectangle(MachineBrush2, MachineRect2);
            
            //画标号
            Font font = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush fontBrush = new SolidBrush(Color.Yellow);
            StringFormat drawingFormat1 = new StringFormat();
            drawingFormat1.Alignment = StringAlignment.Center;
            g.DrawString(_machineNo.ToString(), font, fontBrush, MachineRect2, drawingFormat1);

            font.Dispose();
            fontBrush.Dispose();
            MachineBrush1.Dispose();
            MachineBrush2.Dispose();
            railBrush.Dispose();
            pen1.Dispose();

        }
        public  TRANSMachine(int machineID)
        {
            _machineNo = machineID;
        }
    }
}
