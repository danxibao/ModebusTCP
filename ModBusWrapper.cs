using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;



namespace ModeBus
{
    class ModeBus
    {

        TcpClient tcpClient;
        
        ReadCoils readCoils;
        ReadInputs readInputs;
        ReadHoldingRegisters readHoldingRegisters;
        ReadInputRegisters readInputRegisters;

        WriteSingleCoil writeSingleCoil;
        WriteSingleRegister writeSingleRegister;
        WriteMultipleCoils writeMultipleCoils;
        WriteMultipleRegisters writeMultipleRegisters;
        public bool Open(string ip, int port)
        {
            try
            {
                if(tcpClient!=null)tcpClient.Close();
                tcpClient = new TcpClient();

                tcpClient.Connect(IPAddress.Parse(ip), port);

                readHoldingRegisters = new ReadHoldingRegisters(tcpClient);
                readInputRegisters = new ReadInputRegisters(tcpClient);
                readCoils = new ReadCoils(tcpClient);
                readInputs = new ReadInputs(tcpClient);

                writeSingleCoil = new WriteSingleCoil(tcpClient);
                writeSingleRegister = new WriteSingleRegister(tcpClient);
                writeMultipleCoils = new WriteMultipleCoils(tcpClient);
                writeMultipleRegisters = new WriteMultipleRegisters(tcpClient);

                return true;
            }
            catch (SocketException e)
            {
                string m = string.Format("modbus Client服务器连接错误:{0},ip:{1},port:{2}", e.Message, ip, port);
                
                LogHelper.Log.WriteError(m);
                //throw e;
                return false;
            }
        }

        ~ModeBus()
        {
            tcpClient.Close();
        }
        #region READ
        /// <summary>  
        /// Read contiguous block of 16 bit holding registers. 
        /// </summary>  
        /// <param name="rData">结果</param>  
        /// <param name="id">Address of device to read values from.</param>  
        /// <param name="address">Address to begin reading.</param>
        /// <param name="len">Number of holding registers to read.</param> 
        /// <returns>数据读取结果 是否成功</returns>  
        public bool ReadHoldingRegisters(ref short[] rData, short id, short address, short len)
        {
            try
            {
                return readHoldingRegisters.Read(ref rData, id, address, len);
            }
            catch (SocketException e)
            {
                string m = string.Format("读取错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
        }

        public bool ReadInputRegisters(ref short[] rData, short id, short address, short len)
        {
            try{
                return readInputRegisters.Read(ref rData, id, address, len);
            }
                
            catch (SocketException e)
            {
                string m = string.Format("读取错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
        }

        
        public bool ReadCoils(ref bool[] rData, short id, short address, short len)
        {
            try{
                return readCoils.Read(ref rData, id, address, len);
            }  
            catch (SocketException e)
            {
                string m = string.Format("读取错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
        }

        public bool ReadInputs(ref bool[] rData, short id, short address, short len)
        {
            try
            {
                return readInputs.Read(ref rData, id, address, len);
            }
            catch (SocketException e)
            {
                string m = string.Format("读取错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
        }
        #endregion READ

        #region WRITE
        public bool WriteSingleCoil(short id, short address, bool flag)
        {
            try
            {
                return writeSingleCoil.Write(id, address, flag);
            }
            
            catch (SocketException e)
            {
                string m = string.Format("改写错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
        }

        public bool WriteSingleRegister(short id, short address, short value)
        {
            try{
                return writeSingleRegister.Write(id, address, value);
            }
            catch (SocketException e)
            {
                string m = string.Format("改写错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
        }

        public bool WriteMultipleCoils(short id, short address, short len,bool []data)
        {
            try
            {
                return writeMultipleCoils.Write(id, address, len, data);
            }
            catch (SocketException e)
            {
                string m = string.Format("改写错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
            
        }

        public bool WriteMultipleRegisters(short id, short address, short len, short[] data)
        {
            try
            {
                return writeMultipleRegisters.Write(id, address, len, data);
            }
            catch (SocketException e)
            {
                string m = string.Format("改写错误{0}", e.Message);
                LogHelper.Log.WriteError(m);
                return false;
            }
        }

        #endregion
    }

}
