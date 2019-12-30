using System;
using System.Security.Cryptography;

namespace MPL.Common
{
    /// <summary>
    /// A class that provides random number generation functionality.
    /// </summary>
    public static class RandomNumberGenerator
    {
        #region Constructors
        static RandomNumberGenerator()
        { }

        #endregion

        #region Declarations
        #region _Members_
        private static RNGCryptoServiceProvider _RNG;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private static RNGCryptoServiceProvider GetRNG()
        {
            if (_RNG == null)
                ResetRNG();

            return _RNG;
        }

        private static void ResetRNG()
        {
            _RNG = new RNGCryptoServiceProvider();
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Gets random bytes of the specified length.
        /// </summary>
        /// <param name="size">An int indicating the number of random bytes to return.</param>
        /// <returns>An array of byte containing the result.</returns>
        public static byte[] GetBytes(int size)
        {
            byte[] ReturnValue;

            ReturnValue = new byte[size];
            NextBytes(ReturnValue);

            return ReturnValue;
        }

        /// <summary>
        /// Gets random bytes of the specified length, returned as a string.
        /// </summary>
        /// <param name="size">An int indicating the number of random bytes to return.</param>
        /// <param name="isLowerCase">A bool that indicates whether the return the results in lower case.</param>
        /// <returns>An array of byte containing the result.</returns>
        public static string GetBytesAsString(int size, bool isLowerCase = true)
        {
            byte[] Data;
            string ReturnValue = string.Empty;

            Data = GetBytes(size);
            foreach (byte Item in Data)
                ReturnValue += Item.ToString("x2");
            if (!isLowerCase)
                ReturnValue = ReturnValue.ToUpper();

            return ReturnValue;
        }

        /// <summary>
        /// Gets a SHA256 hash for the specified parameters.
        /// </summary>
        /// <param name="data">A string containing the data to hash.</param>
        /// <param name="gameRandom">A string containing the game random.</param>
        /// <returns>A string containing the hash.</returns>
        public static string GetHash(string data)
        {
            SHA256Managed Hasher;
            byte[] Output;
            string ReturnValue = string.Empty;
            byte[] Source;

            // Get the hash
            Hasher = new SHA256Managed();
            Source = System.Text.Encoding.Default.GetBytes(data);
            Output = Hasher.ComputeHash(Source);

            // Prepare the output
            foreach (byte Item in Output)
                ReturnValue += Item.ToString("x2");

            return ReturnValue;
        }

        /// <summary>
        /// Fills the specified buffer with random bytes.
        /// </summary>
        /// <param name="buffer">An array of byte that will be filled with random bytes.</param>
        public static void NextBytes(byte[] buffer)
        {
            GetRNG().GetBytes(buffer);
            if (buffer[0] % 16 == 0)
                ResetRNG();
        }

        /// <summary>
        /// Gets a random double.
        /// </summary>
        /// <returns>A double that is the result.</returns>
        public static double NextDouble()
        {
            byte[] Data;
            double ReturnValue;
            UInt64 Temp;

            Data = GetBytes(8);
            Temp = BitConverter.ToUInt64(Data, 0) / (1 << 11);
            ReturnValue = Temp / (Double)(1UL << 53);

            return ReturnValue;
        }

        /// <summary>
        /// Gets a random float.
        /// </summary>
        /// <returns>A float that is the result.</returns>
        public static float NextFloat()
        {
            return (float)NextDouble();
        }

        /// <summary>
        /// Gets a random int.
        /// </summary>
        /// <returns>An int that is the result.</returns>
        public static int NextInt()
        {
            byte[] Data;
            int ReturnValue;

            Data = GetBytes(4);
            ReturnValue = BitConverter.ToInt32(Data, 0);

            return ReturnValue;
        }
        /// <summary>
        /// Gets a random int between the specified range.
        /// </summary>
        /// <param name="min">An int that is the minimum number in the range.</param>
        /// <param name="max">An int that is the maximum number in the range.</param>
        /// <returns>An int that is the result.</returns>
        public static int NextInt(int min, int max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("minValue");
            if (min == max) return min;
            Int64 diff = max - min;
            while (true)
            {

                byte[] _uint32Buffer = GetBytes(4);
                UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);

                Int64 maxV = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = maxV % diff;
                if (rand < maxV - remainder)
                {
                    return (Int32)(min + (rand % diff));
                }
            }
        }

        #endregion
        #endregion  
    }
}