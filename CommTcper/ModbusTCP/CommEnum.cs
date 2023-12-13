using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_PLCCommFast
{

    /// <summary>
    /// 通讯枚举
    /// </summary>
    public enum CommType
    {
        /// <summary>
        /// Socket无协议 服务器
        /// </summary>
        FreeTcpServer = 0,
        /// <summary>
        /// Socket无协议 客户端
        /// </summary>
        FreeTcpClient = 1,
        /// <summary>
        /// Modbus TCP 通用协议
        /// </summary>
        ModbusTCP = 2,
        /// <summary>
        /// 三菱MC协议二进制
        /// </summary>
        McBinary = 3,
        /// <summary>
        /// 欧姆龙Fins TCP
        /// </summary>
        OmronFinsTCP = 4,
        /// <summary>
        /// 欧姆龙Fins UDP
        /// </summary>
        OmronFinsUDP = 5,
        /// <summary>
        /// 西门子 S7系列
        /// </summary>
        SiemensS7 = 6
    }


}
