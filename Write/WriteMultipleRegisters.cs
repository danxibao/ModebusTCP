using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ModeBus
{
    class WriteMultipleRegisters
    {
        TcpClient tcpClient;
        public WriteMultipleRegisters(TcpClient tcp)
        {
            tcpClient = tcp;
        }

        /// <summary>  
        /// Read contiguous block of 16 bit holding registers. 
        /// </summary>  
        /// <param name="rData">结果</param>  
        /// <param name="id">Address of device to read values from.</param>  
        /// <param name="address">Address to Write.</param>
        /// <param name="value"></param> 
        /// <returns>数据读取结果 是否成功</returns>  
        public bool Write(short id, short address, short len, short[] value)
        {
            try
            {
                short m = Convert.ToInt16(new Random().Next(2, 20));


                byte[] bs = Receive(m, id, address, len, value);

                return TrimModbus(bs, m, id, len);
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
        /// <param name="value">设备数量</param>  
        /// <returns></returns>  
        private byte[] Receive(short m, short id, short address, short len,short[] value)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected) { return null; }

                byte[] data = GetSrcData(m, id, address, len, value);

                //00 00 00 00 00 06 01 06 00 00 ff 00  
                tcpClient.Client.Send(data, data.Length, SocketFlags.None);

                int size = 15;

                byte[] rData = new byte[size];
                
                //tcpClient.Client.Receive(rData, size, SocketFlags.None);
                tcpClient.Client.Receive(rData);
                
                string str = "";
                foreach (byte b in rData)
                    str += " " + b;
                System.Windows.Forms.MessageBox.Show(str);

                /*
                byte[] Data = new byte[12];
                for (int i = 0; i < 12; i++)
                    Data[i] = rData[i + 3];
                */

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

        private bool TrimModbus(byte[] d, short m, short id, short len)
        {
            int dLen = 12;

            if (d != null &&
                d.Length == dLen &&
                m == (d[0] << 8) + d[1] &&
                id == Convert.ToInt16(d[6]))
            {
                byte[] temp = new byte[] { d[11], d[10] };
                return (BitConverter.ToInt16(temp, 0) == len);

            }
            return false;
        }


  
        /// <summary>  
        /// 发送字节数  
        /// </summary>  
        /// <param name="m"></param>  
        /// <param name="id"></param>  
        /// <param name="address"></param> 
        /// <param name=""></param>  
        /// <returns></returns>  
        private byte[] GetSrcData(short m, short id, short address, short len, short[] value)
        {
            List<byte> data = new List<byte>(255);

            data.AddRange(ValueHelper.Instance.GetBytes(m));                     //             00 01  
            data.AddRange(new byte[] { 0x00, 0x00 });                            //             00 00  
            int N = len * 2;
            data.AddRange(ValueHelper.Instance.GetBytes(Convert.ToInt16(N+7)));    //字节数       00 06  
            data.Add(Convert.ToByte(id));                                        //路由码       01  
            data.Add(Convert.ToByte(16));                                         //功能码 16-写多个寄存器  10  
            data.AddRange(ValueHelper.Instance.GetBytes(address));        //开始地址     00 00 
            data.AddRange(ValueHelper.Instance.GetBytes(len));      //寄存器数量 00 05

            data.Add(Convert.ToByte(N));//字节数 0A  
            for (int i = 0; i < len; i++)
            {
                data.AddRange(ValueHelper.Instance.GetBytes(value[i]));   //值 00 00
            }
                
            return data.ToArray();
        }
    }

}
