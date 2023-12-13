using AppComm;
using AppComm.Entity;
using CommTcper.SRWeb;
using LabExpEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;
using Module_PLCCommFast;
using System.Diagnostics;

namespace CommTcper
{

    public partial class Frm_Main : Form
    {
        private string _labelPath { get; set; }
        private Queue _queue_1 { get; set; }
        private BarTender.Application btApp { get; set; }
        private string _label_1 { get; set; }
        private string _label_2 { get; set; }

        private string _serIPForPrint_1 { get; set; }
        private string _serPortForPrint_1 { get; set; }
        private string _serIPForPrint_2 { get; set; }
        private string _serPortForPrint_2 { get; set; }

        private string _printer_1 { get; set; }
        private string _printer_2 { get; set; }

        private string _codeString { get; set; }

        private string _currentOrderNo { get; set; }

        private string _productionLine { get; set; }

        private string _webServiceUri { get; set; }
        private bool _save { get; set; }
        private int _synumber { get; set; }
        private AutoPastingBarcodeServicePortTypeClient Client { get; set; }

        //新增
        /// <summary>
        /// 用户名
        /// </summary>
        private string _userId { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        private string _workOrderId { get; set; }
        /// <summary>
        /// 车间名
        /// </summary>
        private string _facilityId { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        private string _eqpId { get; set; }

        private string _dataFormat { get; set; }
        private string _address { get; set; }
        ModbusTcp modbusTcp = new ModbusTcp();
        private string _ScanIP { get; set; }
        private string _ScanPort { get; set; }
        public Frm_Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //dgv_sytm = new DataGridViewX();
            InitApp();
            Client = new AutoPastingBarcodeServicePortTypeClient();
            _currentOrderNo = "";
            btn_qhgd.Enabled = false;
            modbusTcp.PushMsg += ShowMsgScan;
            ModbusDataReadingLoopAsync();


        }

        private void InitApp()
        {


            tb_nbstop.Enabled = tb_number.Enabled = tb_number.Enabled = tb_gdh.Enabled = tb_serip.Enabled = tb_serport.Enabled = tb_ptserip.Enabled = tb_ptserport.Enabled = false;
            bt_cancel.Enabled = bt_Rest.Enabled = comboBox1.Enabled = btn_dyjl.Enabled = bt_can.Enabled = btn_qdgd.Enabled = btn_connptser.Enabled = bt_cancel.Enabled = false;
            _save = false;
            tb_serip.Text = ConfigurationSettings.AppSettings["SerIP"];
            tb_serport.Text = ConfigurationSettings.AppSettings["SerPort"];
            tb_ptserip.Text = ConfigurationSettings.AppSettings["PtSerIP"];
            tb_ptserport.Text = ConfigurationSettings.AppSettings["PtSerPort"];
            _serIPForPrint_1 = ConfigurationSettings.AppSettings["SerIPForPrint1"];
            _serPortForPrint_1 = ConfigurationSettings.AppSettings["SerPortForPrint1"];
            _serIPForPrint_2 = ConfigurationSettings.AppSettings["SerIPForPrint2"];
            _serPortForPrint_2 = ConfigurationSettings.AppSettings["SerPortForPrint2"];
            _printer_1 = ConfigurationSettings.AppSettings["Printer1"];
            _printer_2 = ConfigurationSettings.AppSettings["Printer2"];
            _productionLine = ConfigurationSettings.AppSettings["ProductionLine"];
            _webServiceUri = ConfigurationSettings.AppSettings["WebServiceUri"];
            _ScanIP = tb_ScanIp.Text;
            _ScanPort = tb_ScanPort.Text;
            _labelPath = string.Concat(Application.StartupPath, "\\labels");
            _queue_1 = new Queue();
            //新增
            _address = "5";
            _dataFormat = cb_DataFormat.Text;

            //  btApp = new BarTender.Application(); ;
            if (!Directory.Exists(_labelPath))
                Directory.CreateDirectory(_labelPath);
            _label_1 = string.Concat(_labelPath, "\\标签1.btw");
            _label_2 = string.Concat(_labelPath, "\\标签2.btw");
            if (Init())
            {
                ShowSerMsg("等待指令");
                ShowPtSerMsg("等待指令");
                ShowSerMsgForPrinter_1(string.Concat(_serIPForPrint_1, ":", _serPortForPrint_1, ",等待打印机连接"));
                ShowSerMsgForPrinter_2(string.Concat(_serIPForPrint_2, ":", _serPortForPrint_2, ",等待打印机连接"));
            }
            else
            {
                ShowSerMsg("连接不上服务器,请确认后重启程序");
                ShowPtSerMsg("连接不上服务器,请确认后重启程序");
                ShowSerMsgForPrinter_1("暂未启动进程,请确认后重启程序");
                ShowSerMsgForPrinter_2("暂未启动进程,请确认后重启程序");
            }
            if (ScanCodeInit())
            {
                ShowMsgScan("扫码枪连接成功！");
                ShowMsgScan("等待指令");
            }
            else
            {
                ShowMsgScan("扫码枪连接失败！");
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ck_dis.Checked = CheckBbSp();
                    Thread.Sleep(1000);
                }
            });

        }

        //新增PLC客户端
        //异步取消令牌
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private async Task ModbusDataReadingLoopAsync()
        {
             modbusTcp.IP = tb_MoIp.Text;
            modbusTcp.Port = int.Parse(tb_MoPort.Text);
            modbusTcp.DataFormat = (HslCommunication.Core.DataFormat)Enum.Parse(typeof(HslCommunication.Core.DataFormat), _dataFormat);
            modbusTcp.Connect();

            while (true)
            {
                try
                {
                    int data = await ReadModbusDataAsync(_cancellationTokenSource.Token); // 调用异步读取方法
                    if (data == int.MaxValue || data == -1)
                    {
                        _cancellationTokenSource.Cancel();
                        modbusTcp.IsModbusConnect = false;
                        ShowMsgScan("PLC连接异常");
                        return;
                    }
                    if (data == 1)
                    {
                        modbusTcp.WriteInt(_address, 0);
                        if (string.IsNullOrEmpty(ConstData.CurrenScanCode)) //过账流程
                        {
                            ShowMsgScan("上传设备号：" + ConstData.CurrenScanCode);
                            ShowMsgScan("过账流程");

                            // string _postBack = Client.applyForBarcode(new ApbApplyForDTO() { userId = _userId, facilityId = _facilityId, eqpId = _eqpId, lotId = ConstData.CurrenScanCode });

                            //    NewGetBarcode getBarcode = CTool.getWorkOrderIdByEqpId(_postBack);
                            //    if (getBarcode.success)
                            //    {
                            //        ShowSerMsg("过账成功," + getBarcodeWork.msg);
                            //    }
                            //    else{ShowSerMsg("过账异常," + getBarcodeWork.msg);}
                            ConstData.CurrenScanCode = string.Empty;
                        }
                        else
                        {
                            ShowMsgScan("扫码枪指令异常"+ ConstData.CurrenScanCode);
                        }
                    }
                    await Task.Delay(200); // 轮询200ms
                }
                catch (OperationCanceledException)
                {
                    return;
                }

            }
        }

        private async Task<int> ReadModbusDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                int result = await Task.Run(() =>
                {

                    int data = modbusTcp.ReadInt32(_address);
                    return data;
                });

                return result;


            }
            catch (Exception ex)
            {

                return -1;
            }
        }
        //扫码枪客户端
        private bool ScanCodeInit()
        {
            try
            {

                MixCilentCxPo.Init();
                MixCilentCxPo.Connect(_ScanIP, _ScanPort);
                Thread th2 = new Thread(new ThreadStart(() => { while (true) ScanComm(MixCilentCxPo.Receive()); }));
                th2.IsBackground = true;
                th2.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ScanComm(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                ShowMsgScan("扫码枪通信异常");
                return;
            }
            string[] result = str.Split(',');
            if (result[0].Equals("OK"))
            {
                ConstData.CurrenScanCode = result[1];
                ShowMsgScan("扫码枪接收指令：" + str);
                MixCilentCxPo.Send("OK");
            }
            else
            {
                ShowMsgScan("扫码枪失败" + str);
            }
        }
        //-------------------------------------

        private bool Init()
        {

            try
            {
                MixCilentCx.Init();
                MixCilentCx.Connect(tb_serip.Text, tb_serport.Text);
                Thread th = new Thread(new ThreadStart(() => { while (true) ProcReqCommand(MixCilentCx.Receive()); }));
                th.IsBackground = true;
                th.Start();
                if (th.IsBackground == true)
                {
                    tb_serip.Enabled = false;
                }

                ServerCx.InitRecx(_serIPForPrint_1, _serPortForPrint_1,
                (p) =>
                {
                    ShowSerMsgForPrinter_1("状态:" + p);
                },
                (q) =>
                {
                    ProcReqCommandForPrinter_1(q);
                });

                ServerCxT.InitRecx(_serIPForPrint_2, _serPortForPrint_2,
                (p) =>
                {
                    ShowSerMsgForPrinter_2("状态:" + p);
                },
                (q) =>
                {
                    ProcReqCommandForPrinter_2(q);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btn_startser_Click(object sender, EventArgs e)
        {
            bt_cancel.Enabled = true;
            try
            {
                Init();

                ShowSerMsg("重新连接中");
                bt_cancel.Enabled = false;
            }
            catch { }
        }
        //新增扫码枪消息显示
        private object _Lockobj = new object();
        private void ShowMsgScan(string str)
        {
            lock (_Lockobj)
            {
                try
                {
                    if (tb_Scan.Lines.GetUpperBound(0) >= 1000)
                        tb_Scan.Clear();
                    tb_Scan.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]->" + str + Environment.NewLine);
                }
                catch
                {

                }
            }
        }
        private void ShowSerMsgForPrinter_1(string str)
        {
            if (tb_prtgamsg.Lines.GetUpperBound(0) >= 1000)
                tb_prtgamsg.Clear();
            tb_prtgamsg.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]->" + str + Environment.NewLine);
        }

        private void ShowSerMsgForPrinter_2(string str)
        {
            if (tb_prtgbmsg.Lines.GetUpperBound(0) >= 1000)
                tb_prtgbmsg.Clear();
            tb_prtgbmsg.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]->" + str + Environment.NewLine);
        }

        private void ProcReqCommandForPrinter_1(string str)
        {
            if (str.Contains("警报: 打印队列作业已完"))
            {
                ConstData.CurrentPrinterMessage_1 = "0";
            }
            else if (str.Contains("PAPER OUT"))
            {
                ConstData.CurrentPrinterMessage_1 = "4";
            }
            else if (str.Contains("RIBBON OUT"))
            {
                ConstData.CurrentPrinterMessage_1 = "5";
            }
            else
            {
                ConstData.CurrentPrinterMessage_1 = "6";
            }
            ShowSerMsgForPrinter_1(string.Concat("收到GA打印机消息:原始->", str, ",转换后->", ConstData.CurrentPrinterMessage_1));
        }

        private void ProcReqCommandForPrinter_2(string str)
        {
            if (str.Contains("警报: 打印队列作业已完"))
            {
                ConstData.CurrentPrinterMessage_2 = "0";
            }
            else if (str.Contains("PAPER OUT"))
            {
                ConstData.CurrentPrinterMessage_2 = "4";
            }
            else if (str.Contains("RIBBON OUT"))
            {
                ConstData.CurrentPrinterMessage_2 = "5";
            }
            else
            {
                ConstData.CurrentPrinterMessage_2 = "6";
            }
            ShowSerMsgForPrinter_2(string.Concat("收到GB打印机消息:原始->", str, ",转换后->", ConstData.CurrentPrinterMessage_2));
        }

        private void ShowSerMsg(string str)
        {
            if (tb_sermsg.Lines.GetUpperBound(0) >= 1000)
                tb_sermsg.Clear();
            tb_sermsg.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]->" + str + Environment.NewLine);
        }

        private void ProcReqCommand(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                ShowSerMsg("指令码接收异常:服务器似乎已经关闭了连接,请重启程序");

                Thread.Sleep(1000);
                return;
            }
            if (tb_nbstop.Text == lb_yz.Text)
            {
                MessageBox.Show("已打印数量到达截止线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (str.Equals("1"))
            {
                if (_currentOrderNo == "")
                {
                    ShowSerMsg("当前无订单号,不可打印");
                    return;
                }

                ShowSerMsg("处理指令码:" + str + ",动作:向MES请求条码");
                TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                string _resMesCode = string.Empty;
                DataTable dt = DbIns.SysDb.ExecuteSql("select * from messn limit 1;").Result as DataTable;
                DataTable dt1 = DbIns.SysDb.ExecuteSql("select 1 from messn;").Result as DataTable;
                int _number = Convert.ToInt32(tb_number.Text);
                if (dt1.Rows.Count > _number)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _resMesCode = dt.Rows[0][0].ToString();
                    }
                    ShowSerMsg("success");
                }
                else
                {

                    ////GetBarcodesRet _getBarcodesRet = CTool.applyForBarcode(_webServiceUri, _currentOrderNo, _productionLine);
                    ////List<string> _ls = new List<string>();
                    ////_ls.Add("XXX0011111111111111119");
                    ////_ls.Add("XXX0011111111111111118");
                    ////_ls.Add("XXX0011111111111111117");
                    ////_ls.Add("XXX0011111111111111122");
                    ////_ls.Add("XXX0011111111111111123");
                    ////_ls.Add("XXX0011111111111111124");
                    ////bool _checkFlag = false;
                    ////foreach (string _sl in _ls)
                    ////{
                    ////    dt = DbIns.SysDb.ExecuteSql("select 1 from mesprint where sn='" + _sl + "';").Result as DataTable;
                    ////    if (null != dt && null != dt.Rows && dt.Rows.Count > 0)
                    ////    {
                    ////        _checkFlag = true;
                    ////        break;
                    ////    }
                    ////}
                    ////if (_checkFlag)
                    ////{
                    ////    MessageBox.Show("获取的条码有部分已经被打印,请和MES确认!", "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ////    ShowSerMsg("获取的条码有部分已经被打印,请和MES确认!");
                    ////    return;
                    ////}
                    ///
                    //新增
                    //string _postBack = Client.applyForBarcode(new ApbApplyForDTO() { userId = _userId, facilityId = _facilityId, eqpId = _eqpId, workOrderId = _currentOrderNo, requestQty = 50 });

                    //NewGetBarcode getBarcode = CTool.GetBarCode(_postBack);
                    //if (getBarcode.success)
                    //{
                    //    if (getBarcode.result == null)
                    //    {
                    //        ShowSerMsg("请求失败," + getBarcode.msg);
                    //    }
                    //    else
                    //    {
                    //        string _guid = Guid.NewGuid().ToString();
                    //        ShowSerMsg("获取MES条码成功," + getBarcode.msg);
                    //        IList<string> _ls = (IList<string>)getBarcode.result;
                    //        foreach (string _sl in _ls)
                    //        {
                    //            DbIns.SysDb.ExecuteSql(string.Format("insert into messn(sn,orderno,batchno) values('{0}','{1}','{2}');", _sl, _currentOrderNo, _guid));
                    //            // DbIns.SysDb.ExecuteSql(string.Format("insert into mesbak(sn,orderno,batchno) values('{0}','{1}','{2}');", _sl, _currentOrderNor, _guid));


                    //        }
                    //        dt = DbIns.SysDb.ExecuteSql("select * from messn limit 1;").Result as DataTable;
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            _resMesCode = dt.Rows[0][0].ToString();
                    //        }
                    //    }

                    //}

                }
                if (string.IsNullOrEmpty(_resMesCode))
                {
                    ShowSerMsg("取得MES条码异常:暂无可用的条码,请重试");
                    return;
                }

                ConstData.CurrentMesCode = _resMesCode;

                ShowSerMsg("取得MES条码:" + _resMesCode + ",动作:打印条码");
                //时间戳
                TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts3 = ts2.Subtract(ts1).Duration();

                ShowSerMsg(string.Concat("分", ts3.Minutes, "秒", ts3.Seconds));
                //打印机1
                BarTender.Format btFormatPrint = btApp.Formats.Open(_label_1, false, string.Empty);
                btFormatPrint.PrintSetup.Printer = _printer_1;
                //btFormatPrint.PrintSetup.NumberSerializedLabels = 1;                
                btFormatPrint.PrintSetup.IdenticalCopiesOfLabel = 1;
                btFormatPrint.SetNamedSubStringValue("sn", _resMesCode);
                btFormatPrint.PrintOut(false, false);
                //打印机2
                BarTender.Format btFormatPrint2 = btApp.Formats.Open(_label_2, false, string.Empty);
                btFormatPrint2.PrintSetup.Printer = _printer_2;
                //btFormatPrint.PrintSetup.NumberSerializedLabels = 1;                
                btFormatPrint2.PrintSetup.IdenticalCopiesOfLabel = 1;
                btFormatPrint2.SetNamedSubStringValue("sn", _resMesCode);
                btFormatPrint2.PrintOut(false, false);

                Thread.Sleep(50);

                ConstData.PrevCurrentPrinterMessage_1 = ConstData.CurrentPrinterMessage_1;
                ConstData.PrevCurrentPrinterMessage_2 = ConstData.CurrentPrinterMessage_2;

                ShowPtSerMsg("发送MES条码:" + _resMesCode);
                MixCilentCx.Send(string.Concat("S,", _resMesCode));
                Thread.Sleep(100);

                Thread.Sleep(50);
                ShowSerMsg("读取到打印状态码(GA)并发送:" + ConstData.CurrentPrinterMessage_1);
                Thread.Sleep(50);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GA,", ConstData.CurrentPrinterMessage_1));
                Thread.Sleep(50);
                Thread.Sleep(50);
                //阻塞线程1
                MixCilentCx.Send(string.Concat("L,1"));
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GA,", ConstData.CurrentPrinterMessage_1));
                Thread.Sleep(50);
                Thread.Sleep(50);
                ShowSerMsg("读取到打印状态码(GB)并发送:" + ConstData.CurrentPrinterMessage_2);
                Thread.Sleep(50);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GB,", ConstData.CurrentPrinterMessage_2));

                Thread.Sleep(100);
                //阻塞线程2
                Thread.Sleep(50);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("Z,2"));
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GB,", ConstData.CurrentPrinterMessage_2));
                //打印机出码时间
                TimeSpan ts4 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts5 = ts4.Subtract(ts3).Duration();
                ShowSerMsg(string.Concat("出码时间" + "分", ts5.Minutes, "秒", ts5.Seconds));
                DbIns.SysDb.ExecuteSql("insert into mesprint(sn,orderno,batchno,ptsts,pts) select sn,orderno,batchno,'" + ConstData.CurrentPrinterMessage_1 + "','" + _printer_1 + "' from messn where sn='" + _resMesCode + "';");
                DbIns.SysDb.ExecuteSql("insert into mesprint(sn,orderno,batchno,ptsts,pts) select sn,orderno,batchno,'" + ConstData.CurrentPrinterMessage_2 + "','" + _printer_2 + "' from messn where sn='" + _resMesCode + "';");
                DbIns.SysDb.ExecuteSql("delete from messn where sn='" + _resMesCode + "';");
                string pms = " and tm between '" + DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 20:00:00' ";
                if (!CheckBb())
                {
                    if (!ck_dis.Checked)
                    {
                        pms = " and tm between '" + DateTime.Now.ToString("yyyy-MM-dd") + " 20:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59' ";
                    }
                    else
                    {
                        pms = " and tm between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00' ";
                    }
                }
                DataTable _dtp = DbIns.SysDb.ExecuteSql("select 1 from mesprint where orderno='" + _currentOrderNo + "' " + pms + ";").Result as DataTable;
                DataTable _dtinner = DbIns.SysDb.ExecuteSql("select sn 条码,orderno 订单号 from messn where orderno='" + _currentOrderNo + "';").Result as DataTable;
                lb_yz.Text = (_dtp.Rows.Count / 2).ToString();
                lb_sy.Text = _dtinner.Rows.Count.ToString();
                if (tb_nbstop.Text == lb_yz.Text)
                {
                    MessageBox.Show("已打印数量到达截止线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //dgv_sytm.DataSource = _dtinner;
            }
            else if (str.Equals("2"))
            {
                ShowSerMsg("处理指令码:" + str + ",动作:GA打印机重复打印");
                if (string.IsNullOrEmpty(ConstData.CurrentMesCode))
                {
                    ShowSerMsg("未找到上次发送的条码");
                    return;
                }

                BarTender.Format btFormatPrint = btApp.Formats.Open(_label_1, false, string.Empty);

                btFormatPrint.PrintSetup.Printer = _printer_1;
                btFormatPrint.SetNamedSubStringValue("sn", ConstData.CurrentMesCode);
                btFormatPrint.PrintOut(false, false);

                Thread.Sleep(50);

                ShowPtSerMsg("发送MES条码:" + ConstData.CurrentMesCode);
                Thread.Sleep(100);
                MixCilentCx.Send(string.Concat("S,", ConstData.CurrentMesCode));
                ShowSerMsg("读取到打印状态码(GA)并发送:" + ConstData.CurrentPrinterMessage_1);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("S,", ConstData.CurrentMesCode));
                Thread.Sleep(100);
                MixCilentCx.Send(string.Concat("GA,", ConstData.CurrentPrinterMessage_1));
                Thread.Sleep(100);
                //阻塞线程1
                Thread.Sleep(50);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("L,1"));
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GA,", ConstData.CurrentPrinterMessage_1));
                DbIns.SysDb.ExecuteSql("update mesprint set utm=current_timestamp,ptms=ptms+1,ptsts='" + ConstData.CurrentPrinterMessage_1 + "' where sn='" + ConstData.CurrentMesCode + "' and pts='" + _printer_1 + "';");
            }
            else if (str.Equals("3"))
            {
                ShowSerMsg("处理指令码:" + str + ",动作:GB打印机重复打印");
                if (string.IsNullOrEmpty(ConstData.CurrentMesCode))
                {
                    ShowSerMsg("未找到上次发送的条码");
                    return;
                }

                BarTender.Format btFormatPrint2 = btApp.Formats.Open(_label_2, false, string.Empty);
                btFormatPrint2.PrintSetup.Printer = _printer_2;
                //btFormatPrint.PrintSetup.NumberSerializedLabels = 1;                
                btFormatPrint2.PrintSetup.IdenticalCopiesOfLabel = 1;
                btFormatPrint2.SetNamedSubStringValue("sn", ConstData.CurrentMesCode);
                btFormatPrint2.PrintOut(false, false);

                Thread.Sleep(50);

                ShowPtSerMsg("发送MES条码:" + ConstData.CurrentMesCode);
                MixCilentCx.Send(string.Concat("S,", ConstData.CurrentMesCode));
                Thread.Sleep(100);
                ShowSerMsg("读取到打印状态码(GB)并发送:" + ConstData.CurrentPrinterMessage_2);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GB,", ConstData.CurrentPrinterMessage_2));
                Thread.Sleep(50);
                Thread.Sleep(50);
                Thread.Sleep(100);
                //阻塞线程2
                MixCilentCx.Send(string.Concat("Z,2"));
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GB,", ConstData.CurrentPrinterMessage_2));
                DbIns.SysDb.ExecuteSql("update mesprint set utm=current_timestamp,ptms=ptms+1,ptsts='" + ConstData.CurrentPrinterMessage_2 + "' where sn='" + ConstData.CurrentMesCode + "' and pts='" + _printer_2 + "';");
            }
            else if (str.Equals("4"))
            {
                ShowSerMsg("处理指令码:" + str + ",动作:GA和GB打印机都重复打印");
                ShowSerMsg("GA打印中");
                if (string.IsNullOrEmpty(ConstData.CurrentMesCode))
                {
                    ShowSerMsg("未找到上次发送的条码");
                    return;
                }

                BarTender.Format btFormatPrint = btApp.Formats.Open(_label_1, false, string.Empty);

                btFormatPrint.PrintSetup.Printer = _printer_1;
                btFormatPrint.SetNamedSubStringValue("sn", ConstData.CurrentMesCode);
                btFormatPrint.PrintOut(false, false);
                Thread.Sleep(100);
                Thread.Sleep(50);

                ShowPtSerMsg("发送MES条码:" + ConstData.CurrentMesCode);
                MixCilentCx.Send(string.Concat("S,", ConstData.CurrentMesCode));
                Thread.Sleep(100);
                ShowSerMsg("读取到打印状态码(GA)并发送:" + ConstData.CurrentPrinterMessage_1);


                MixCilentCx.Send(string.Concat("GA,", ConstData.CurrentPrinterMessage_1));
                Thread.Sleep(100);
                //阻塞线程1
                MixCilentCx.Send(string.Concat("L,1"));
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GA,", ConstData.CurrentPrinterMessage_1));

                ShowSerMsg("GB打印中");
                if (string.IsNullOrEmpty(ConstData.CurrentMesCode))
                {
                    ShowSerMsg("未找到上次发送的条码");
                    return;
                }

                BarTender.Format btFormatPrint2 = btApp.Formats.Open(_label_2, false, string.Empty);
                btFormatPrint2.PrintSetup.Printer = _printer_2;
                //btFormatPrint.PrintSetup.NumberSerializedLabels = 1;                
                btFormatPrint2.PrintSetup.IdenticalCopiesOfLabel = 1;
                btFormatPrint2.SetNamedSubStringValue("sn", ConstData.CurrentMesCode);
                btFormatPrint2.PrintOut(false, false);

                Thread.Sleep(50);

                ShowPtSerMsg("发送MES条码:" + ConstData.CurrentMesCode);
                MixCilentCx.Send(string.Concat("S,", ConstData.CurrentMesCode));
                ShowSerMsg("读取到打印状态码(GB)并发送:" + ConstData.CurrentPrinterMessage_2);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GB,", ConstData.CurrentPrinterMessage_2));
                Thread.Sleep(100);
                //阻塞线程2
                Thread.Sleep(50);
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("Z,2"));
                Thread.Sleep(50);
                MixCilentCx.Send(string.Concat("GB,", ConstData.CurrentPrinterMessage_2));
                DbIns.SysDb.ExecuteSql("update mesprint set utm=current_timestamp,ptms=ptms+1,ptsts='" + ConstData.CurrentPrinterMessage_1 + "' where sn='" + ConstData.CurrentMesCode + "' and pts='" + _printer_1 + "';");
                DbIns.SysDb.ExecuteSql("update mesprint set utm=current_timestamp,ptms=ptms+1,ptsts='" + ConstData.CurrentPrinterMessage_2 + "' where sn='" + ConstData.CurrentMesCode + "' and pts='" + _printer_2 + "';");
            }
            else
            {
                ShowSerMsg("无效指令码:" + str + ",动作:丢弃");
            }
        }

        private void btn_connptser_Click(object sender, EventArgs e)
        {
            try
            {
                btn_connptser.Enabled = false;
                string str = "mes条码";
                MixCilentCx.Send(str);
                ShowPtSerMsg("发送成功:" + str);
                btn_connptser.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowPtSerMsg("发送失败:" + ex.Message);
                btn_connptser.Enabled = true;
            }
        }

        private void ShowPtSerMsg(string str)
        {
            if (tb_ptsermsg.Lines.GetUpperBound(0) >= 1000)
                tb_ptsermsg.Clear();
            tb_ptsermsg.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]->" + str + Environment.NewLine);
        }
        private void btn_qhgd_Click(object sender, EventArgs e)
        {

            DataTable dt = DbIns.SysDb.ExecuteSql("select 1 from messn;").Result as DataTable;
            if (null != dt && null != dt.Rows && dt.Rows.Count > 0)
            {
                MessageBox.Show("订单号还有未打印的条码!", "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bt_can.Enabled = true;
            tb_gdh.Enabled = true;
            btn_qhgd.Enabled = false;
            btn_qdgd.Enabled = true;
            comboBox1.Enabled = true;
            _currentOrderNo = "";

        }

        private void btn_qdgd_Click(object sender, EventArgs e)
        {
            if ("" == tb_gdh.Text)
            {
                MessageBox.Show("订单号不可为空,请修改!", "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_currentOrderNo != "" && _currentOrderNo == tb_gdh.Text)
            {
                MessageBox.Show("订单号不可与之前的一致,请修改!", "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = DbIns.SysDb.ExecuteSql("select orderno from messn;").Result as DataTable;
            if (null != dt && null != dt.Rows && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != tb_gdh.Text)
                {
                    MessageBox.Show("订单号[" + dt.Rows[0][0].ToString() + "]还有未打印的条码,请先退还条码给MES或者输入订单号[" + dt.Rows[0][0].ToString() + "]继续打印!", "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            _currentOrderNo = tb_gdh.Text;
            tb_gdh.Enabled = false;
            btn_qhgd.Enabled = true;
            btn_qdgd.Enabled = false;
            _save = true;

            string pms = " and tm between '" + DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 20:00:00' ";
            if (!CheckBb())
            {
                if (!ck_dis.Checked)
                {
                    pms = " and tm between '" + DateTime.Now.ToString("yyyy-MM-dd") + " 20:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59' ";
                }
                else
                {
                    pms = " and tm between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00' ";
                }
            }
            DataTable _dtp = DbIns.SysDb.ExecuteSql("select 1 from mesprint where orderno='" + _currentOrderNo + "' " + pms + ";").Result as DataTable;
            DataTable _dtinner = DbIns.SysDb.ExecuteSql("select sn 条码,orderno 订单号 from messn where orderno='" + _currentOrderNo + "';").Result as DataTable;
            lb_yz.Text = (_dtp.Rows.Count / 2).ToString();
            lb_sy.Text = _dtinner.Rows.Count.ToString();
            if (tb_nbstop.Text == lb_yz.Text)
            {
                MessageBox.Show("已打印数量到达截止线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



        }

        private bool CheckBb()
        {
            TimeSpan start = new TimeSpan(8, 0, 0);
            TimeSpan end = new TimeSpan(20, 0, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                return true;
            }
            return false;
        }

        private bool CheckBbSp()
        {
            TimeSpan start = new TimeSpan(0, 0, 0);
            TimeSpan end = new TimeSpan(8, 0, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                return true;
            }
            return false;
        }

        private void btn_thtm_Click(object sender, EventArgs e)
        {
            if ("" == _currentOrderNo)
            {
                MessageBox.Show("订单号为空,不可退还条码!", "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("订单号[" + _currentOrderNo + "],确定要取消当前订单(请确认系统当前打印已经完成),数量:" + lb_sy.Text + "?", "通讯程序", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //新增
                DataTable _dtinner = DbIns.SysDb.ExecuteSql("select sn from messn where orderno='" + _currentOrderNo + "';").Result as DataTable;
                if (_dtinner.Rows.Count > 0)
                {
                    if (MessageBox.Show("确认清除当前剩余条码集合", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        DbIns.SysDb.ExecuteSql("delete from messn;");
                        ShowPtSerMsg("清除成功");
                    }
                    else
                    {
                        ShowPtSerMsg("取消清除动作");
                    }

                }
                //foreach (DataRow _drin in _dtinner.Rows)
                //{
                //    _lsCs.Add(_drin[0].ToString());
                //}
                //新增
                if (_dtinner.Rows.Count == 0)
                {
                    //取消投批
                    DbIns.SysDb.ExecuteSql("delete from workOrder where workOrderid='" + _currentOrderNo + "';");
                    Orderinit();

                    //  string _postBack = Client.applyForBarcode(new ApbApplyForDTO() { userId = _userId, facilityId = _facilityId, eqpId = _eqpId, workOrderId = ConstData.WorckOrder });

                    //    NewGetBarcodeWork getBarcodeWork = CTool.getWorkOrderIdByEqpId(_postBack);
                    //    if (getBarcodeWork.success)
                    //    {
                    //        if (!getBarcodeWork.msg.Equals("取消投批成功"))
                    //        {
                    //            MessageBox.Show("获取MES条码错误," + getBarcodeWork.msg, "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //            ShowSerMsg("获取MES条码错误," + getBarcodeWork.msg);
                    //            return;
                    //        }
                    //        else
                    //        {
                    //            ShowSerMsg("取消成功," + getBarcodeWork.msg);
                    // DbIns.SysDb.ExecuteSql("delete from messn;");
                    //        }
                }

                //lb_yz.Text = "0";
                lb_sy.Text = "0";
                _currentOrderNo = "";
                comboBox1.Text = "";
                tb_gdh.Enabled = true;
                tb_gdh.Clear();
                btn_qdgd.Enabled = true;
                btn_qhgd.Enabled = false;
                comboBox1.Enabled = true;
            }
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult Dt = MessageBox.Show("请确认操作是否完成,\r\n否则将造成数据丢失!\r\n确定是否要关闭程序?", "通讯程序", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Dt == DialogResult.OK)
            {
                e.Cancel = false;
                this.Dispose();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btn_dyjl_Click(object sender, EventArgs e)
        {
            Frm_Query fq = new Frm_Query();
            fq.Show();
        }

        public bool DataTableToTXT(DataTable vContent, string vOutputFilePath)
        {
            StringBuilder sTxtContent;
            try
            {
                if (File.Exists(vOutputFilePath))
                    File.Delete(vOutputFilePath);

                sTxtContent = new StringBuilder();

                foreach (DataColumn col in vContent.Columns)
                {
                    sTxtContent.Append(col.ColumnName);
                    sTxtContent.Append("\t");
                }
                sTxtContent.Append("\r\n");

                foreach (DataRow row in vContent.Rows)
                {
                    for (int i = 0; i < vContent.Columns.Count; i++)
                    {
                        sTxtContent.Append(row[i].ToString().Trim());
                        sTxtContent.Append(i == vContent.Columns.Count - 1 ? "\r\n" : "\t");
                    }
                }
                File.WriteAllText(vOutputFilePath, sTxtContent.ToString(), Encoding.Unicode);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void Frm_Main_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = true;
                notifyIcon_rp.Visible = true;
            }
        }

        private void notifyIcon_rp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState != FormWindowState.Normal)
            {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Normal)
            {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认操作是否完成,\r\n否则将造成数据丢失!\r\n确定是否要关闭程序?", "退出程序", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

        private void bt_dlu_Click(object sender, EventArgs e)
        {
            Pm pm = new Pm();
            if (pm.ShowDialog() == DialogResult.OK)
            {
                tb_serip.Enabled = tb_serport.Enabled = tb_ptserip.Enabled = tb_ptserport.Enabled = false;
                bt_Rest.Enabled = btn_qdgd.Enabled = btn_connptser.Enabled = btn_dyjl.Enabled = true;
                comboBox1.Enabled = tb_nbstop.Enabled = tb_number.Enabled = tb_number.Enabled = tb_gdh.Enabled = true;
                bt_dlu.BackColor = System.Drawing.Color.Green;
            }

        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (!_save)
            {
                MessageBox.Show("请先确定订单号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            comboBox1.Enabled = bt_Rest.Enabled = tb_nbstop.Enabled = tb_number.Enabled = tb_gdh.Enabled = tb_serip.Enabled = tb_serport.Enabled = tb_ptserip.Enabled = tb_ptserport.Enabled = false;
            bt_can.Enabled = comboBox1.Enabled = btn_dyjl.Enabled = bt_can.Enabled = btn_qdgd.Enabled = btn_connptser.Enabled = bt_cancel.Enabled = false;
            bt_dlu.BackColor = System.Drawing.Color.Transparent;
            Class2.__result = false;

        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            Orderinit();
        }
        //新增
        private object obj = new object();
        /// <summary>
        /// 初始化订单号集合
        /// </summary>
        private void Orderinit()
        {
            lock (obj)
            {
                BeginInvoke(new Action(() =>
                {

                    try
                    {
                        comboBox1.Items.Clear();
                        DataTable _order = DbIns.SysDb.ExecuteSql("select workOrderid from workOrder;").Result as DataTable;
                        if (_order.Rows.Count > 0)
                        {
                            foreach (DataRow data in _order.Rows)
                            {
                                comboBox1.Items.Add(data[0].ToString());
                            }
                        }
                        else
                        {
                            comboBox1.Items.Clear();
                            comboBox1.Text = string.Empty;
                            tb_gdh.Text = string.Empty;
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }

                }));

            }
        }



        ////请求订单号
        private void button1_Click_1(object sender, EventArgs e)

        {

            //    string _postBack = Client.applyForBarcode(new ApbApplyForDTO() { userId = _userId, facilityId = _facilityId, eqpId = _eqpId });

            //    NewGetBarcodeWork getBarcodeWork = CTool.getWorkOrderIdByEqpId(_postBack);
            //    if (getBarcodeWork.success)
            //    {
            //        if (getBarcodeWork.result == null)
            //        {
            //            MessageBox.Show("获取MES条码错误," + getBarcodeWork.msg, "通讯程序", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            ShowSerMsg("获取MES条码错误," + getBarcodeWork.msg);
            //            return;
            //        }
            //        else
            //        {
            //            ShowSerMsg("获取MES条码成功," + getBarcodeWork.msg);
            //            IList<string> _ls = (IList<string>)getBarcodeWork.result;
            //            foreach (string _sl in _ls)
            //            {
            //                 DbIns.SysDb.ExecuteSql(string.Format("insert into workOrder (workOrderid) values('{0}');", _sl));
            //
            //            }
            //        }
            //    }
            IList<string> _ls = new List<string> { "12", "242", "2422" };
            foreach (string _sl in _ls)
            {
                DbIns.SysDb.ExecuteSql(string.Format("insert into workOrder (workOrderid) values('{0}');", _sl));
            }
            Orderinit();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_gdh.Text = comboBox1.Text;
        }

        private void bt_connMo_Click(object sender, EventArgs e)
        {
            if (modbusTcp.IsModbusConnect)
            {
                ShowSerMsg("PLC通信正在运行中请勿重新连接");
                return;
            }
            _cancellationTokenSource = new CancellationTokenSource();
             ModbusDataReadingLoopAsync();
            if (modbusTcp.IsModbusConnect)
            {
                ShowMsgScan("重新连接PLC成功");
            }
        }
               

        private void bt_connScan_Click(object sender, EventArgs e)
        {
            ScanCodeInit();
            ShowMsgScan("重新连接扫码枪");
        }
    }
    //public class DataGridViewX : DataGridView
    //{
    //    protected override void OnPaint(PaintEventArgs e)
    //    {
    //        try
    //        {
    //            base.OnPaint(e);
    //        }
    //        catch
    //        {
    //            Invalidate();
    //        }
    //    }
    //}
}
