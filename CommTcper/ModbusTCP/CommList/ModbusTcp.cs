
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication;
using HslCommunication.ModBus;
using HslCommunication.Core;

namespace Module_PLCCommFast
{
    [Serializable]
    public class ModbusTcp : CommBase
    {

        public ModbusTcpNet modbusTcp;

        /// <summary>
        /// 站别
        /// </summary>
        byte Station = 1;

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsModbusConnect;

        DataFormat _DataFormat = DataFormat.CDAB;

        /// <summary>
        /// 解析格式
        /// </summary>
        public DataFormat DataFormat
        {
            get { return _DataFormat; }
            set { _DataFormat = value; }
        }

        /// <summary>
        /// 根据属性名初始化参数
        /// </summary>
        /// <param name="propName">属性名</param>
        /// <param name="paramValue">属性值（String）</param>
        /// <returns>初始化状态</returns>
        public bool SetParamValue(string propName, string paramValue)
        {
            try
            {
                switch (propName)
                {
                    case "DataFormat":
                        _DataFormat = (DataFormat)System.Enum.Parse(typeof(DataFormat), paramValue);
                        break;
                    case "IP":
                        IP = paramValue;
                        break;
                    case "Port":
                        Port = int.Parse(paramValue);
                        break;
                    case "CommName":
                        CommName = paramValue;
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public override bool Connect()
        {
            try
            {
                modbusTcp = new ModbusTcpNet();
                modbusTcp.ConnectClose();

                if (!IsModbusConnect)
                {
                    modbusTcp = new ModbusTcpNet(IP, Port, Station);
                    modbusTcp.AddressStartWithZero = true; //首选地址从0开始
                    modbusTcp.ConnectTimeOut = 6000;//超时6秒

                    modbusTcp.DataFormat = _DataFormat;

                    OperateResult connect = modbusTcp.ConnectServer();
                    if (connect.IsSuccess)
                    {
                        UCommStatus.SetStatus(EnumCommStatus.Opened);
                        PushData = "Modbus连接成功！";
                        IsModbusConnect = true;
                        PushEvent();
                        return true;
                    }
                    else
                    {
                        PushData = "Modbus连接失败！";
                        PushEvent();
                        return false;
                    }
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                PushData = "Modbus连接失败：" + ex.Message;
                PushEvent();
                return false;
            }
        }

        public override bool DisConnect()
        {
            try
            {
                if (IsModbusConnect)
                {
                    UCommStatus.SetStatus(EnumCommStatus.Closed);
                    OperateResult result = modbusTcp.ConnectClose();
                    if (result == OperateResult.CreateSuccessResult())
                    {
                        IsModbusConnect = false;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    IsModbusConnect = false;
                    return true;
                }
            }
            catch (Exception ex)
            {
                IsModbusConnect = false;
                PushData = "Modbus关闭失败：" + ex.Message;
                PushEvent();
                return true;
            }

        }

        public override bool ReadBool(string address)
        {
            bool result;
            if (modbusTcp.ReadBool(address).IsSuccess)
            {
                result = modbusTcp.ReadBool(address).Content;
            }
            else
            {
                result = false;
                PushData = modbusTcp.ReadBool(address).Message;
                PushEvent();
            }
            return result;
        }

        public override float ReadFloat(string address)
        {
            float result;
            if (modbusTcp.ReadFloat(address).IsSuccess)
            {
                result = modbusTcp.ReadFloat(address).Content;
            }
            else
            {
                result = float.MaxValue;
                PushData = modbusTcp.ReadFloat(address).Message;
                PushEvent();
            }
            return result;
        }

        public override int ReadInt32(string address)
        {
            int result;
            if (modbusTcp.ReadInt32(address).IsSuccess)
            {
                result = modbusTcp.ReadInt32(address).Content;
            }
            else
            {
                result = int.MaxValue;
                PushData = modbusTcp.ReadInt32(address).Message;
                PushEvent();
            }
            return result;
        }

        public override long ReadLong(string address)
        {
            long result;
            if (modbusTcp.ReadInt64(address).IsSuccess)
            {
                result = modbusTcp.ReadInt64(address).Content;
            }
            else
            {
                result = long.MaxValue;
                PushData = modbusTcp.ReadInt64(address).Message;
                PushEvent();
            }
            return result;
        }

        public override short ReadShort(string address)
        {
            short result;
            if (modbusTcp.ReadInt16(address).IsSuccess)
            {
                result = modbusTcp.ReadInt16(address).Content;
            }
            else
            {
                result = short.MaxValue;
                PushData = modbusTcp.ReadInt16(address).Message;
                PushEvent();
            }
            return result;
        }

        public override string ReadString(string address, string length)
        {
            string result;
            try
            {
                if (modbusTcp.ReadString(address, ushort.Parse(length)).IsSuccess)
                {
                    result = modbusTcp.ReadString(address, ushort.Parse(length)).Content;
                }
                else
                {
                    result = "Null";
                    PushData = modbusTcp.ReadInt16(address).Message;
                    PushEvent();
                }
            }
            catch (Exception ex)
            {
                result = "Error:" + ex.ToString();
                throw;
            }
            return result;
        }

        public override bool WriteBool(string address, bool value)
        {
            bool result;
            if (modbusTcp.Write(address, value).IsSuccess)
            {
                result = true;
            }
            else
            {
                result = false;
                PushData = (modbusTcp.Write(address, value).Message);
                PushEvent();
            }
            return result;
        }

        public override bool WriteFloat(string address, float value)
        {
            bool result;
            if (modbusTcp.Write(address, value).IsSuccess)
            {
                result = true;
            }
            else
            {
                result = false;
                PushData = modbusTcp.Write(address, value).Message;
                PushEvent();
            }
            return result;
        }

        public override bool WriteInt(string address, int value)
        {
            bool result;
            if (modbusTcp.Write(address, value).IsSuccess)
            {
                result = true;
            }
            else
            {
                result = false;
                PushData = modbusTcp.Write(address, value).Message;
                PushEvent();
            }
            return result;
        }

        public override bool WriteLong(string address, long value)
        {
            bool result;
            if (modbusTcp.Write(address, value).IsSuccess)
            {
                result = true;
            }
            else
            {
                result = false;
                PushData = modbusTcp.Write(address, value).Message;
                PushEvent();
            }
            return result;
        }

        public override bool WriteShort(string address, short value)
        {
            bool result;
            if (modbusTcp.Write(address, value).IsSuccess)
            {
                result = true;
            }
            else
            {
                result = false;
                PushData = modbusTcp.Write(address, value).Message;
                PushEvent();
            }
            return result;
        }

        public override bool WriteString(string address, string value)
        {
            bool result;
            if (modbusTcp.Write(address, value).IsSuccess)
            {
                result = true;
            }
            else
            {
                result = false;
                PushData = modbusTcp.Write(address, value).Message;
                PushEvent();
            }
            return result;
        }
    }
}
