using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

/// <summary>
/// 通讯简单工厂模式
/// </summary>
namespace Module_PLCCommFast
{
    /// <summary>
    /// 通讯基类--目前仅支持网口通讯
    /// </summary>
    [Serializable]
    public class CommBase
    {

        #region [公共参数]

        string _CommName = "";
        string _Ip = "127.0.0.1";
        int _Port = 502;

        /// <summary>
        /// 通讯名称
        /// </summary>
        public string CommName
        {
            get { return _CommName; }
            set { _CommName = value; }
        }

        /// <summary>
        /// 默认IP 127.0.0.1
        /// </summary>
        public string IP
        {
            get { return _Ip; }
            set { _Ip = value; }
        }

        /// <summary>
        /// 默认端口 502
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        /// <summary>
        /// 触发信号--主要用于Socket接收消息
        /// </summary>

        public delegate void PushEventHandler(string msg);
        public event PushEventHandler PushMsg; //消息推送
        public string PushData;

        #endregion


        #region [消息推送]

        protected void PushEvent()
        {
            if (PushMsg != null)
            {
                PushMsg(PushData);
            }
        }

        #endregion


        #region [连接与读写方法]

        public virtual bool Connect()
        {
            return true;
        }


        public virtual bool DisConnect()
        {
            return true;
        }

        public virtual bool Write(string msg)
        {
            bool result = false;
            return result;
        }

        public virtual bool ReadBool(string address)
        {
            bool result = false;
            return result;
        }

        public virtual float ReadFloat(string address)
        {
            float result = 0;

            return result;
        }

        public virtual int ReadInt32(string address)
        {
            int result = 0;
            return result;
        }

        public virtual long ReadLong(string address)
        {
            long result = 0;
            return result;
        }

        public virtual short ReadShort(string address)
        {
            short result = 0;
            return result;
        }

        public virtual string ReadString(string address, string length)
        {
            string result = "";
            return result;
        }

        public virtual bool WriteBool(string address, bool value)
        {
            bool result = false;
            return result;
        }

        public virtual bool WriteFloat(string address, float value)
        {
            bool result = false;
            return result;
        }

        public virtual bool WriteInt(string address, int value)
        {
            bool result = false;
            return result;
        }

        public virtual bool WriteLong(string address, long value)
        {
            bool result = false;
            return result;
        }

        public virtual bool WriteShort(string address, short value)
        {
            bool result = false;
            return result;
        }

        public virtual bool WriteString(string address, string value)
        {
            bool result = false;
            return result;
        }

        #endregion
    }
}
