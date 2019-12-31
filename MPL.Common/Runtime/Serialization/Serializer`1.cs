using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MPL.Common.Runtime.Serialization
{
    /// <summary>
    /// A class that provides serialisation functions.
    /// </summary>
    /// <typeparam name="T">A T that is the class type to be serialised or deserialised.</typeparam>
    public static class Serializer<T>
        where T : class
    {
        #region Constructors
        /// <summary>
        /// Creates a new static instance of the class.
        /// </summary>
        static Serializer()
        {
            _Encoding = Encoding.Default;
        }

        #endregion

        #region Declarations
        #region _Members_
        private static Encoding _Encoding;

        #endregion
        #endregion

        #region Methods
        #region _Public_
        /// <summary>
        /// Deserialises the specified data.
        /// </summary>
        /// <param name="data">An array of byte containing the data to deserialise.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(byte[] data)
        {
            MemoryStream Data;
            T ReturnValue = default(T);

            // Create the stream
            Data = new MemoryStream(data);

            try
            {
                // Try to deserialise the data
                ReturnValue = Deserialise(Data);
            }
            finally
            {
                // Release the stream
                Data.Close();
            }

            return ReturnValue;
        }
        /// <summary>
        /// Deserialises the specified data stream.
        /// </summary>
        /// <param name="data">A Stream containing the data to deserialise.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(Stream data)
        {
            object SourceObject;
            System.Runtime.Serialization.Json.DataContractJsonSerializer Serialiser;
            T ReturnValue = default(T);

            // Create the serialiser
            Serialiser = CreateSerialiser();

            // Desrialise the object and check it's valid
            SourceObject = Serialiser.ReadObject(data);
            if (SourceObject != null && SourceObject is T)
                ReturnValue = (T)SourceObject;

            return ReturnValue;
        }
        /// <summary>
        /// Deserialises the specified string data using the default text encoding.
        /// </summary>
        /// <param name="data">A string containing the data to deserialise.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(string data)
        {
            return Deserialise(data, _Encoding);
        }
        /// <summary>
        /// Deserialises the specified string data using the specified text encoding.
        /// </summary>
        /// <param name="data">A string containing the data to deserialise.</param>
        /// <param name="encoding">An Encoding that specifies the text encoding to use during desrialisation.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(string data, Encoding encoding)
        {
            T ReturnValue = default(T);

            if (data != null)
            {
                byte[] Data;

                // Convert the string to a byte array and deserialise
                Data = encoding.GetBytes(data);
                ReturnValue = Deserialise(Data);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Serialises the specified object.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <returns>An array of byte containing the serialised object.</returns>
        public static byte[] Serialise(T obj)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer Serialiser;
            byte[] ReturnValue = null;
            MemoryStream TargetStream;

            // Create the serialiser
            Serialiser = CreateSerialiser();

            // Serialise the object
            TargetStream = new MemoryStream();
            Serialiser.WriteObject(TargetStream, obj);
            ReturnValue = TargetStream.ToArray();

            return ReturnValue;
        }

        /// <summary>
        /// Serialises the specified object.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(T obj)
        {
            return SerialiseToString(obj, _Encoding);
        }
        /// <summary>
        /// Serialises the specified object.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <param name="encoding">An Encoding that specifies the text encoding to use during desrialisation.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(T obj, Encoding encoding)
        {
            byte[] Data;
            string ReturnValue = null;

            // Get the serialised object and convert to text
            Data = Serialise(obj);
            if (Data != null && Data.Length > 0)
            {
                ReturnValue = encoding.GetString(Data);
                ReturnValue = ReturnValue.Replace("\0", "");
            }

            return ReturnValue;
        }

        #endregion
        #region _Private_
        private static DataContractJsonSerializer CreateSerialiser()
        {
            return new DataContractJsonSerializer(typeof(T));
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the default text encoding used during serialisation and desrialisation processes.
        /// </summary>
        public static Encoding Encoding
        {
            get { return _Encoding; }
            set
            {
                if (value == null)
                    throw new ArgumentException("The specified encoding is null", "value");
                else
                    _Encoding = value;
            }
        }

        #endregion
    }
}