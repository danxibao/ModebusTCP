using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModeBus
{
    public partial class Form1 : Form
    {
        ModeBus Wrapper;
        public Form1()
        {
            InitializeComponent();
        }

        private void connect1_Click(object sender, EventArgs e)
        {
            Wrapper = new ModeBus();
            Wrapper.Open(serverIP.Text, int.Parse(serverPort.Text));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ReadCoils(1, 0, 10);
            
            ReadHoldingRegisters(1, 0, 10);
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            //WriteSingleCoil(1, 3, true);

            
            short[] s = { 12, 34, 56, 78,11,22,33,44 };
            WriteMultipleRegisters(1, 0, 8, s);
            //WriteSingleRegister(1, 5, 456);

            //bool[] b = { true, false, true, false, true, false, true, false, true, false };
            //WriteMultipleCoils(1, 0, 10, b);
            
        }

        private void ReadCoils(short id, short address, short len)
        {
            bool[] data = new bool[2005];
            Wrapper.ReadCoils(ref data, id, address, len);
            foreach (bool b in data)
            {
                RText.Text += b ? 1 : 0;
                RText.Text += ' ';
            }
        }
        private void ReadInputs(short id, short address, short len)
        {
            bool[] data = new bool[2005];
            Wrapper.ReadInputs(ref data, id, address, len);
            foreach (bool b in data)
            {
                RText.Text += b ? 1 : 0;
                RText.Text += ' ';
            }
        }

        private void ReadInputRegisters(short id, short address, short len)
        {
            short[] data = new short[256];
            Wrapper.ReadInputRegisters(ref data, id, address, len);
            foreach (short b in data)
            {
                RText.Text += b;
                RText.Text += ' ';
            }
        }
        private void ReadHoldingRegisters(short id, short address, short len)
        {
            short[] data = new short[256];
            Wrapper.ReadHoldingRegisters(ref data, id, address, len);
            foreach (short b in data)
            {
                RText.Text += b;
                RText.Text += ' ';
            }
        }
        private void WriteSingleCoil(short id, short address, bool flag)
        {
            Wrapper.WriteSingleCoil(id, address, flag);
            RText.Text += flag ? 1 : 0;
        }

        private void WriteSingleRegister(short id, short address, short value)
        {
            Wrapper.WriteSingleRegister(id, address, value);
            RText.Text += value;
            RText.Text += ' ';
        }

        private void WriteMultipleCoils(short id, short address, short len,bool[]data)
        {
            bool b=Wrapper.WriteMultipleCoils(id, address, len,data);
            RText.Text += b.ToString();
            RText.Text += ' ';
        }

        private void WriteMultipleRegisters(short id, short address, short len, short[] data)
        {
            bool b = Wrapper.WriteMultipleRegisters(id, address, len, data);
            RText.Text += b.ToString();
            RText.Text += ' ';
        }


          
    }
}
