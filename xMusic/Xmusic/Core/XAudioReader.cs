using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Xmusic
{
    public class XAudioReader : AudioFileReader, IInput
    {
        private static readonly EndianHelper _endian = EndianHelper.NewInstance();
        private string _file;
        private long _dataRead;
        //private Stream _fileStream;
        public int DataLen;
        public XAudioReader(string fileName) : base(fileName)
        {
            _file = fileName;
            //_fileStream = File.Open(_file, FileMode.Open, FileAccess.Read);
            _dataRead = 0;
            DataLen = (int)this.Length;
           
        }

        public string FileName
        {
            get
            {
                return _file;
            }
        }

        public WaveFormat IWaveFormat
        {
            get
            {
                return WaveFormat;
            }
        }

        public string OutFileName
        {
            get
            {
                var fileNoExt = Path.GetFileNameWithoutExtension(FileName);
                var filePath = FileName.Replace(fileNoExt, $"{fileNoExt}_out");
                return filePath;
            }
        }

        public bool Eof()
        {
            return _dataRead == DataLen || this.Position >= this.Length;
        }

        public short GetBitsSample()
        {
            return (short)WaveFormat.BitsPerSample;
        }

        public int GetNumChannels()
        {
            return WaveFormat.Channels;
        }

        public int GetSampleRate()
        {
            return WaveFormat.SampleRate;
        }

        public int Read(float[] buffer, int maxElements)
        {

            int bytesPerSample = GetBitsSample() / 8;
            if ((bytesPerSample < 1) || (bytesPerSample > 4))
                throw new InvalidOperationException(string.Format("Only 8/16/24/32 bit sample WAV files supported. Can't open WAV file with {0} bit sample format.",GetSampleRate()));

            int numBytes = maxElements * bytesPerSample;
            long afterDataRead = _dataRead + numBytes;
            if (afterDataRead > DataLen)
            {
                // Don't read more samples than are marked available in header
                numBytes = DataLen - (int)_dataRead;
            }

            // read raw data into temporary buffer
            byte[] temp = new byte[numBytes];
            numBytes = this.Read(temp, 0, numBytes);
            _dataRead += numBytes;

            int numElements = numBytes / bytesPerSample;

            // swap byte ordert & convert to float, depending on sample format
            switch (bytesPerSample)
            {
                case 1:
                    {
                        const double conv = 1.0 / 128.0;
                        for (int i = 0; i < numElements; i++)
                        {
                            buffer[i] = (float)(temp[i] * conv - 1.0);
                        }
                        break;
                    }

                case 2:
                    {
                        var temp2 = (ArrayPtr<short>)temp;
                        const double conv = 1.0 / 32768;
                        for (int i = 0; i < numElements; i++)
                        {
                            short value = temp2[i];
                            buffer[i] = (float)(_endian.Swap16(ref value) * conv);
                        }
                        break;
                    }

                case 3:
                    {
                        const double conv = 1.0 / 8388608;
                        for (int i = 0; i < numElements; i++)
                        {
                            int value = BitConverter.ToInt32(temp, i * 3);
                            value = _endian.Swap32(ref value) & 0x00ffffff;             // take 24 bits
                            value |= ((value & 0x00800000) != 0) ? unchecked((int)0xff000000) : 0;  // extend minus sign bits
                            buffer[i] = (float)(value * conv);
                        }
                        break;
                    }

                case 4:
                    {
                        var temp2 = (ArrayPtr<int>)temp;
                        const double conv = 1.0 / 2147483648;
                        
                        for (int i = 0; i < numElements; i++)
                        {
                            int value = temp2[i];
                            buffer[i] = (float)(_endian.Swap32(ref value) * conv);
                        }
                        break;
                    }
            }

            return numElements;
        }
    }

    public class ArrayPtr<T> : IEnumerable<T>
    {
        private static readonly int SIZEOF_SAMPLETYPE = Marshal.SizeOf(typeof(T));

        private readonly T[] _buffer;
        private int _index;

        private ArrayPtr(T[] buffer, int index)
        {
            _buffer = buffer;
            _index = index;
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        public T this[int index]
        {
            get { return _buffer[_index + index]; }
            set { _buffer[_index + index] = value; }
        }

        /// <summary>
        /// Advances the pointer to the next element.
        /// </summary>
        public static ArrayPtr<T> operator ++(ArrayPtr<T> ptr)
        {
            ptr._index++;
            return ptr;
        }

        /// <summary>
        /// Returns a new Array, pointing the the element, 
        /// <paramref name="index"/> positions after the start of 
        /// <paramref name="ptrArray"/>.
        /// </summary>
        public static ArrayPtr<T> operator +(ArrayPtr<T> ptrArray, int index)
        {
            return new ArrayPtr<T>(ptrArray._buffer, ptrArray._index + index);
        }

        /// <summary>
        /// Performs an implicit conversion from array of T to <see cref="SoundTouch.Utility.ArrayPtr&lt;T&gt;"/>.
        /// </summary>
        public static implicit operator ArrayPtr<T>(T[] buffer)
        {
            return new ArrayPtr<T>(buffer, 0);
        }

        public static explicit operator ArrayPtr<T>(byte[] buffer)
        {
            var temp = new T[buffer.Length / Marshal.SizeOf(typeof(T))];
            Buffer.BlockCopy(buffer, 0, temp, 0, buffer.Length);
            return temp;
        }

        /// <summary>
        /// Returns the first element <paramref name="buffer"/> is pointing to.
        /// </summary>
        public static explicit operator T(ArrayPtr<T> buffer)
        {
            return buffer[0];
        }

        /// <summary>
        /// Returns a copy of the Array, beginning at the offset set by <see cref="ArrayPtr{T}"/>.
        /// </summary>
        public static explicit operator T[] (ArrayPtr<T> buffer)
        {
            var copy = new T[buffer._buffer.Length - buffer._index];
            Array.Copy(buffer._buffer, buffer._index, copy, 0, copy.Length);
            return copy;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return _buffer.Skip(_index).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Copies the specified amount of bytes from source buffer to destination.
        /// </summary>
        /// <param name="to">Destination.</param>
        /// <param name="from">Source.</param>
        /// <param name="byteCount">The amount of bytes to copy.</param>
        public static void CopyBytes(ArrayPtr<T> to, ArrayPtr<T> from, int byteCount)
        {
            Buffer.BlockCopy(from._buffer, from._index * SIZEOF_SAMPLETYPE, to._buffer, to._index * Marshal.SizeOf(typeof(T)), byteCount);
        }

        /// <summary>
        /// Fills the buffer with the specified value.
        /// </summary>
        public static void Fill(ArrayPtr<T> buffer, T value, int count)
        {
            int last = buffer._index + count;
            for (int index = buffer._index; index < last; ++index)
                buffer._buffer[index] = value;
        }
    }

}
