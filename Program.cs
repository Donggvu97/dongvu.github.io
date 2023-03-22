using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modbustest
{
    internal class Program
    {
        private object serialPort1;

        public object MessageBox { get; }

        static void Main(string[] args)
        {
            //ModbusTcpMasterReadInputs();
            ModbusSerialRtuMasterWriteRegisters();
            frmVirtualCom();
        }



        public static void ModbusTcpMasterReadInputs()
        {
            TcpClient client = new TcpClient("192.168.99.201", 502);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(client);


            // read five input values
            ushort startAddress = 0;
            ushort numInputs = 5;



            while (true)
            {
                ushort[] inputs = master.ReadHoldingRegisters(1, startAddress, numInputs);
                for (int i = 0; i < numInputs; i++)
                {

                    // Console.WriteLine("value: " + inputs[i]);
                    Console.WriteLine("value[" + i + "] = " + inputs[i]);

                }
                Thread.Sleep(2000);
            };
        }
        public static void ModbusSerialRtuMasterWriteRegisters()
            
        {
            using (SerialPort port = new SerialPort("COM1"))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                var adapter = new SerialPortAdapter(port);
                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu((Modbus.IO.IStreamResource)adapter);

                byte slaveId = 1;
                ushort startAddress = 100;
                ushort[] registers = new ushort[] { 1, 2, 3 };

                // write three registers
                master.WriteMultipleRegisters(slaveId, startAddress, registers);
                

            }
        }
    

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
        }
