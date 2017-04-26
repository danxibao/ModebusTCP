using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ModeBus
{
    class WriteSingleCoil
    {
        TcpClient tcpClient;
        public WriteSingleCoil(TcpClient tcp)
        {
            tcpClient = tcp;
        }

        /// <summary>  
        /// Read contiguous block of 16 bit holding registers. 
        /// </summary>  
        /// <param name="rData">结果</param>  
        /// <param name="id">Address of device to read values from.</param>  
        /// <param name="address">Address to Write.</param>
        /// <param name="len">Number of coils to read.</param> 
        /// <returns>数据读取结果 是否成功</returns>  
        public bool Write(short id, short address, bool flag)
        {
            try
            {
                short m = Convert.ToInt16(new Random().Next(2, 20));


                byte[] bs = Receive(m, id, address, flag);

                return TrimModbus(bs, m, id, flag);
            }
            catch (Exception e)
            {
                LogHelper.Log.WriteError("返回Modbus数据错误" + e.Message);
                return false;
            }
        }


        /// <summary>  
        /// 读取 Modbus  
        ///00 00 00 00 00 0d  01  03  0A 14 00  14 00  14 00  14 00  14 00  
        /// </summary>  
        /// <param name="m">标示</param>  
        /// <param name="id">设备码</param>  
        /// <param name="address">开始地址</param>  
        /// <param name="len">设备数量</param>  
        /// <returns></returns>  
        private byte[] Receive(short m, short id, short address, bool flag)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected) { return null; }

                byte[] data = GetSrcData(m, id, address, flag);

                //00 00 00 00 00 06 01 05 00 00 ff 00  
                tcpClient.Client.Send(data, data.Length, SocketFlags.None);

                int size = 12;

                byte[] rData = new byte[size];

                tcpClient.Client.Receive(rData, size, SocketFlags.None);

                //string t1 = TranBytes(rData);  

                return rData;

            }
            catch (SocketException e)
            {
                if (e.ErrorCode != 10004)
                {
                    LogHelper.Log.WriteError(e.Message);
                }

                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                }

                return null;
            }
        }

        private bool TrimModbus(byte[] d, short m, short id, bool flag)
        {
            int dLen = 12;

            if (d != null && 
                d.Length == dLen &&
                m==(d[0] << 8) + d[1] && 
                id == d[6])
            {
                if (d[10] == 0xff && d[11] == 0x00 && flag)
                    return true;
                else if (d[10] == 0x00 && d[11] == 0x00 && !flag)
                    return true;
            }
            
            return false;
        }


        //发送  
        //00 00 00 00 00 06 01 03 00 00 00 05  
        /// <summary>  
        /// 发送字节数  
        /// </summary>  
        /// <param name="m"></param>  
        /// <param name="id"></param>  
        /// <param name="address"></param> 
        /// <param name="flag"></param>  
        /// <returns></returns>  
        private byte[] GetSrcData(short m, short id, short address, bool flag)
        {
            List<byte> data = new List<byte>(255);

            data.AddRange(ValueHelper.Instance.GetBytes(m));                     //             00 01  
            data.AddRange(new byte[] { 0x00, 0x00 });                            //             00 00  
            data.AddRange(ValueHelper.Instance.GetBytes(Convert.ToInt16(6)));    //字节数       00 06  
            data.Add(Convert.ToByte(id));                                        //路由码       01  
            data.Add(Convert.ToByte(5));                                         //功能码 5-写单个线圈  05  
            data.AddRange(ValueHelper.Instance.GetBytes(address));                   //地址     00 00  
            data.AddRange((flag ? new byte[] { 0xff, 0x00 } : new byte[] { 0x00, 0x00 }));   //符号 high:0xff00 low:0x0000 
            return data.ToArray();
        }
    }

}
