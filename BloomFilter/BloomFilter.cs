using Murmur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    public class BloomFilter
    {
        private readonly byte[] _filter;
        private readonly int _iterationCount;
        private Murmur32 _hasher = MurmurHash.Create32();

        public BloomFilter(int sizeInBytes, int iterationCount)
        {
            _filter = new byte[sizeInBytes];
            _iterationCount = iterationCount;
        }

        public void AddWord(string word)
        {
            byte[] data = Encoding.UTF8.GetBytes(word);

            for (int i = 0; i < _iterationCount; i++)
            {
                byte[] hash = _hasher.ComputeHash(data);
                long index = BitConverter.ToUInt32(hash, 0) % _filter.Length;
                SetBit(index);
                data = hash;
            }
        }

        public bool CheckWord(string word)
        {
            byte[] data = Encoding.UTF8.GetBytes(word);

            for (int i = 0; i < _iterationCount; i++)
            {
                byte[] hash = _hasher.ComputeHash(data);
                long index = BitConverter.ToUInt32(hash, 0) % _filter.Length;
                if (!GetBit(index)) return false;
                data = hash;
            }

            return true;
        }

        private bool GetBit(long filterIndex)
        {
            long byteIndex = filterIndex / 8;
            int bitInByteIndex = (int)(filterIndex % 8);
            byte mask = (byte)((long)1 << (7 - bitInByteIndex));
            return (_filter[byteIndex] & mask) != 0;
        }

        private void SetBit(long filterIndex)
        {
            long byteIndex = filterIndex / 8;
            int bitInByteIndex = (int)(filterIndex % 8);
            byte mask = (byte)((long)1 << (7 - bitInByteIndex));
            _filter[byteIndex] |= mask;
        }
    }
}
