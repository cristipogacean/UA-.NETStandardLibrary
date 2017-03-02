using System;
using System.Collections.Generic;
using System.Threading;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;


namespace Opc.Ua.Sample.BackgroundServer
{

    // App that reads data over I2C from a ITG3200, 3-Axis MEMS Gyro Angular Rate Sensor
    internal partial class ITG3200State
    {

        #region Private Attributes

        private I2cDevice m_I2CGyro = null;
        private object m_janitor = new object();
        #endregion

        #region Private Constants 

        private const byte GYRO_I2C_ADDR = 0x68;    // I2C address of the ITG3200
        private const byte GYRO_REG_DLPF = 0x16;    // DLPF, Full Scale register
        private const byte GYRO_REG_POWER = 0x3E;   // Power Management register
        private const byte GYRO_REG_X = 0x1D;       // X Axis High data register
        private const byte GYRO_REG_Y = 0x1F;       // Y Axis High data register
        private const byte GYRO_REG_Z = 0x21;       // Z Axis High data register

        #endregion

        #region Private Methods

        public async void InitI2CGyro()
        {            
            string aqs = I2cDevice.GetDeviceSelector();     // Get a selector string that will return all I2C controllers on the system
            var dis = await DeviceInformation.FindAllAsync(aqs);    // Find the I2C bus controller device with our selector string
            if (dis.Count == 0)
            {
                throw new Exception("No I2C controllers were found on the system");
            }

            // Create an I2C Device with our selected bus controller and I2C settings
            var settings = new I2cConnectionSettings(GYRO_I2C_ADDR);
            settings.BusSpeed = I2cBusSpeed.FastMode;
            m_I2CGyro = await I2cDevice.FromIdAsync(dis[0].Id, settings);
            if (m_I2CGyro == null)
            {
                string result = string.Format(
                "Slave address {0} on I2C Controller {1} is currently in use by " +
                    "another application. Please ensure that no other applications are using I2C.",
                settings.SlaveAddress, dis[0].Id);
                throw new Exception(result);
            }
            
            //	Initialize the 3-Axis MEMS Gyro Angular Rate Sensor
            //	For this device, we create 2-byte write buffers
            //	The first byte is the register address we want to write to
            //	The second byte is the contents that we want to write to the register
            
            // 0x01 Power ON's the sensor and clock source is set to PLL with X Gyro reference
            byte[] WriteBuf_Power = new byte[] { GYRO_REG_POWER, 0x01 };
            
            // 0x18 sets gyro full-scale range to ±2000°/sec, low pass filter bandwidth to 256 Hz and internal sample rate to 8 Hz
            byte[] WriteBuf_Dlpf = new byte[] { GYRO_REG_DLPF, 0x18 };

            // Write the register settings
            m_I2CGyro.Write(WriteBuf_Power);
            m_I2CGyro.Write(WriteBuf_Dlpf);            
        }

        internal void ReadDevice()
        {
           
            // Read and format 3-Axis MEMS Gyro Angular Rate Sensor data
            try
            {
                
                ReadI2CGyro();
                m_online.Value = true;
            }
            catch 
            {
                m_gyroX.StatusCode = StatusCodes.BadSensorFailure;
                m_gyroY.StatusCode = StatusCodes.BadSensorFailure;
                m_gyroZ.StatusCode = StatusCodes.BadSensorFailure;
                m_temperature.StatusCode = StatusCodes.BadSensorFailure;
                m_online.Value = false;
            }

        }

        private void ReadI2CGyro()
        {
            byte[] RegAddrBuf = new byte[] { GYRO_REG_X };  // Read data from the register address
            byte[] ReadBuf = new byte[6];                   // We read 6 bytes sequentially to get X-Axis and all 3 two-byte axes registers in one read


            if (m_I2CGyro == null)
            {
                throw new Exception("attempt to road not initialized I2C");
            }
            //	Read from the 3-Axis MEMS Gyro Angular Rate Sensor 
            //	We call WriteRead() so we first write the address of the X-Axis I2C register, then read all 3 axes
            m_I2CGyro.WriteRead(RegAddrBuf, ReadBuf);
            
			//	In order to get the raw 14-bit data values, we need to concatenate two 8-bit bytes from the I2C read for each axis		
            int GYRORawX = (int)((ReadBuf[0] & 0xFF) * 256);
            GYRORawX |= (int)(ReadBuf[1] & 0xFF);
            if (GYRORawX > 32767)
            {
                GYRORawX -= 65536;
            }

            int GYRORawY = (int)((ReadBuf[2] & 0xFF) * 256);
            GYRORawY |= (int)(ReadBuf[3] & 0xFF);
            if (GYRORawY > 32767)
            {
                GYRORawY -= 65536;
            }

            int GYRORawZ = (int)((ReadBuf[4] & 0xFF) * 256);
            GYRORawZ |= (int)(ReadBuf[5] & 0xFF);
            if (GYRORawZ > 32767)
            {
                GYRORawZ -= 65536;
            }

            m_gyroX.Value = GYRORawX;
            m_gyroY.Value = GYRORawY;
            m_gyroZ.Value = GYRORawZ;
            m_temperature.Value = 0;
                       
        }
        #endregion

        public override void Dispose()
        {
            // Cleanup
            if (m_I2CGyro != null)
            { 
                m_I2CGyro.Dispose();
            }
        }

    }
}
