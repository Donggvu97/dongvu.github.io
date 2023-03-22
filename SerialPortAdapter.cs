using System.IO.Ports;

namespace Modbustest
{
    internal class SerialPortAdapter
    {
        private SerialPort port;

        public SerialPortAdapter(SerialPort port)
        {
            this.port = port;
        }
    }
}