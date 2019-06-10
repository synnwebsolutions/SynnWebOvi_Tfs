using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmusic
{
    public class XAudioWriter : NAudio.Wave.WaveFileWriter, IOutput
    {
        //private Stream _fileStream;
        private static readonly EndianHelper _endian = EndianHelper.NewInstance();
        private int _bytesWritten;
        private short BitsPerSample;
    

        public XAudioWriter(IInput reader) : base(reader.OutFileName,reader.IWaveFormat)
        {
         
            //_fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write);
            BitsPerSample = (short)reader.GetBitsSample();
            
        }

        
        public void Write(float[] buffer, int numElements)
        {
            if (numElements == 0) return;

            int bytesPerSample = BitsPerSample / 8;
            int numBytes = numElements * bytesPerSample;
            byte[] temp = new byte[numBytes];

            switch (bytesPerSample)
            {
                case 1:
                    {
                        for (int i = 0; i < numElements; i++)
                        {
                            temp[i] = (byte)Saturate(buffer[i] * 128.0f + 128.0f, 0f, 255.0f);
                        }
                        break;
                    }

                case 2:
                    {
                        short[] temp2 = new short[temp.Length / 2];
                        for (int i = 0; i < numElements; i++)
                        {
                            short value = (short)Saturate(buffer[i] * 32768.0f, -32768.0f, 32767.0f);
                            temp2[i] = _endian.Swap16(ref value);
                        }
                        Buffer.BlockCopy(temp2, 0, temp, 0, temp.Length);
                        break;
                    }

                case 3:
                    {
                        for (int i = 0; i < numElements; i++)
                        {
                            int value = Saturate(buffer[i] * 8388608.0f, -8388608.0f, 8388607.0f);
                            Buffer.BlockCopy(BitConverter.GetBytes(_endian.Swap32(ref value)), 0, temp, i * 3, 4);
                        }
                        break;
                    }

                case 4:
                    {
                        int[] temp2 = new int[temp.Length / 4];
                        for (int i = 0; i < numElements; i++)
                        {
                            int value = Saturate(buffer[i] * 2147483648.0f, -2147483648.0f, 2147483647.0f);
                            temp2[i] = _endian.Swap32(ref value);
                        }
                        Buffer.BlockCopy(temp2, 0, temp, 0, temp.Length);
                        break;
                    }

                default:
                    break;
            }

            this.Write(temp, 0, numBytes);
            _bytesWritten += numBytes;
        }

        private int Saturate(float value, float minval, float maxval)
        {
            if (value > maxval)
                value = maxval;
            else if (value < minval)
                value = minval;
            return (int)value;
        }
    }

    internal abstract class EndianHelper
    {
        /// <summary>helper-function to swap byte-order of 32bit integer</summary>
        public abstract int Swap32(ref int dwData);

        /// <summary>helper-function to swap byte-order of 16bit integer</summary>
        public abstract short Swap16(ref short wData);

        /// <summary>helper-function to swap byte-order of buffer of 16bit integers</summary>
        public abstract void Swap16Buffer(short[] pData, int dwNumWords);

        public static EndianHelper NewInstance()
        {
            if (BitConverter.IsLittleEndian)
                return new LittleEndianHelper();
            return new BigEndianHelper();
        }
    }

    internal sealed class LittleEndianHelper : EndianHelper
    {
        public override int Swap32(ref int dwData)
        {
            return dwData;
        }

        public override short Swap16(ref short wData)
        {
            return wData;
        }

        public override void Swap16Buffer(short[] pData, int dwNumWords)
        {
        }
    }

    internal sealed class BigEndianHelper : EndianHelper
    {

        public override int Swap32(ref int dwData)
        {
            var data = unchecked((uint)dwData);
            dwData = unchecked((int)(((data >> 24) & 0x000000FF) |
                                      ((data >> 8) & 0x0000FF00) |
                                      ((data << 8) & 0x00FF0000) |
                                      ((data << 24) & 0xFF000000)));
            return dwData;
        }

        public override short Swap16(ref short wData)
        {
            wData = unchecked((short)(((wData >> 8) & 0x00FF) |
                                       ((wData << 8) & 0xFF00)));
            return wData;
        }

        public override void Swap16Buffer(short[] pData, int dwNumWords)
        {
            for (int i = 0; i < dwNumWords; i++)
            {
                pData[i] = Swap16(ref pData[i]);
            }
        }
    }

}
