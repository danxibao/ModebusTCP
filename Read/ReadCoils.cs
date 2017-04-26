using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ModeBus
{
    class ReadCoils
    {
        TcpClient tcpClient;
        public ReadCoils(TcpClient tcp)
        {
            tcpClient = tcp;
        }

        /// <summary>  
        /// Read contiguous block of 16 bit holding registers. 
        /// </summary>  
        /// <param name="rData">结果</param>  
        /// <param name="id">Address of device to read values from.</param>  
        /// <param name="address">Address to begin reading.</param>
        /// <param name="len">Number of coils to read.</param> 
        /// <returns>数据读取结果 是否成功</returns>  
        public bool Read(ref bool[] rData, short id, short address, short len)
        {
            try
            {
                short m = Convert.ToInt16(new Random().Next(2, 20));
                rData = null;
                //m = 0x3030;
                

                byte[] bs = Receive(m, id, address, len);
                byte[] b = TrimModbus(bs, m, id, len);

                if (b == null) { return false; }

                List<bool> data = new List<bool>(2005);
                
                for (int i = 0; i < b.Length; i++)
                {
                    
                        int temp = b[i];
                        for (int j = 0; j < 8; j++)
                        {
                            data.Add(Convert.ToBoolean(temp & 1));
                            temp = temp >> 1;
                        }
                            
                }

                rData = data.Take(len).ToArray();

                return true;
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
        private byte[] Receive(short m, short id, short address, short len)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected) { return null; }

                byte[] data = GetSrcData(m, id, address, len);

                //00 00 00 00 00 06 01 01 00 00 00 05  
                tcpClient.Client.Send(data, data.Length, SocketFlags.None);

                int size = len + 9;

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

        private byte[] TrimModbus(byte[] d, short m, short id, short len)
        {
            int size = Convert.ToInt32(len);
            int dLen = size + 9;

            if (d == null || d.Length != dLen || m != (d[0] << 8) + d[1] || id != Convert.ToInt16(d[6]))
            {
                return null;
            }
            byte[] n = new byte[size];
            Array.Copy(d, 9, n, 0, size);
            return n;
        }


        //发送  
        //00 00 00 00 00 06 01 03 00 00 00 05  
        /// <summary>  
        /// 发送字节数  
        /// </summary>  
        /// <param name="m"></param>  
        /// <param name="id"></param>  
        /// <param name="address"></param> 
        /// <param name="len"></param>  
        /// <returns></returns>  
        private byte[] GetSrcData(short m, short id, short address, short len)
        {
            List<byte> data = new List<byte>(255);

            data.AddRange(ValueHelper.Instance.GetBytes(m));                     //             00 01  
            data.AddRange(new byte[] { 0x00, 0x00 });                            //             00 00  
            data.AddRange(ValueHelper.Instance.GetBytes(Convert.ToInt16(6)));    //字节数       00 06  
            data.Add(Convert.ToByte(id));                                        //路由码       01  
            data.Add(Convert.ToByte(1));                                         //功能码 1-读  01  
            data.AddRange(ValueHelper.Instance.GetBytes(address));                   //开始地址     00 00  
            data.AddRange(ValueHelper.Instance.GetBytes(len));                   //设备数量     00 05  
            return data.ToArray();
        }
    }

}
