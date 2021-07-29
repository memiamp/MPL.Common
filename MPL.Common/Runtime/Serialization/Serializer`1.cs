using System;
using System.IO;
using System.Runtime.Serialization;
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
            _defaultSerializerOptions = new SerializerOptions();
            _Encoding = Encoding.Default;
        }

        #endregion

        #region Declarations
        #region _Members_
        private static readonly SerializerOptions _defaultSerializerOptions;
        private static Encoding _Encoding;

        #endregion
        #endregion

        #region Methods
        #region _Public_
        /// <summary>
        /// Deserialises the specified data using the default deserialisation options.
        /// </summary>
        /// <param name="data">An array of byte containing the data to deserialise.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(byte[] data)
        {
            return Deserialise(data, _defaultSerializerOptions);
        }
        /// <summary>
        /// Deserialises the specified data.
        /// </summary>
        /// <param name="data">An array of byte containing the data to deserialise.</param>
        /// <param name="serializerOptions">A SerializerOptions containing the serialiser options to use.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(byte[] data, SerializerOptions serializerOptions)
        {
            MemoryStream Data;
            T ReturnValue = default;

            // Create the stream
            Data = new MemoryStream(data);

            try
            {
                // Try to deserialise the data
                ReturnValue = Deserialise(Data, serializerOptions);
            }
            finally
            {
                // Release the stream
                Data.Close();
            }

            return ReturnValue;
        }
        /// <summary>
        /// Deserialises the specified data stream using the default deserialisation options.
        /// </summary>
        /// <param name="data">A Stream containing the data to deserialise.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(Stream data)
        {
            return Deserialise(data, _defaultSerializerOptions);
        }
        /// <summary>
        /// Deserialises the specified data stream.
        /// </summary>
        /// <param name="data">A Stream containing the data to deserialise.</param>
        /// <param name="serializerOptions">A SerializerOptions containing the serialiser options to use.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(Stream data, SerializerOptions serializerOptions)
        {
            object SourceObject;
            DataContractJsonSerializer Serialiser;
            T ReturnValue = default;

            // Create the serialiser
            Serialiser = CreateSerialiser(serializerOptions);

            // Desrialise the object and check it's valid
            SourceObject = Serialiser.ReadObject(data);
            if (SourceObject != null && SourceObject is T castSourceObject)
                ReturnValue = castSourceObject;

            return ReturnValue;
        }
        /// <summary>
        /// Deserialises the specified string data using the default text encoding and the default deserialisation options.
        /// </summary>
        /// <param name="data">A string containing the data to deserialise.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(string data)
        {
            return Deserialise(data, _defaultSerializerOptions);
        }
        /// <summary>
        /// Deserialises the specified string data using the default text encoding.
        /// </summary>
        /// <param name="data">A string containing the data to deserialise.</param>
        /// <param name="serializerOptions">A SerializerOptions containing the serialiser options to use.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(string data, SerializerOptions serializerOptions)
        {
            return Deserialise(data, _Encoding, serializerOptions);
        }
        /// <summary>
        /// Deserialises the specified string data using the specified text encoding and the default deserialisation options.
        /// </summary>
        /// <param name="data">A string containing the data to deserialise.</param>
        /// <param name="encoding">An Encoding that specifies the text encoding to use during desrialisation.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(string data, Encoding encoding)
        {
            return Deserialise(data, encoding, _defaultSerializerOptions);
        }
        /// <summary>
        /// Deserialises the specified string data using the specified text encoding.
        /// </summary>
        /// <param name="data">A string containing the data to deserialise.</param>
        /// <param name="encoding">An Encoding that specifies the text encoding to use during desrialisation.</param>
        /// <param name="serializerOptions">A SerializerOptions containing the serialiser options to use.</param>
        /// <returns>A T desrialised from the data.</returns>
        public static T Deserialise(string data, Encoding encoding, SerializerOptions serializerOptions)
        {
            T ReturnValue = default;

            if (data != null)
            {
                byte[] Data;

                // Convert the string to a byte array and deserialise
                Data = encoding.GetBytes(data);
                ReturnValue = Deserialise(Data, serializerOptions);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Serialises the specified object using the default serialisation options.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <returns>An array of byte containing the serialised object.</returns>
        public static byte[] Serialise(T obj)
        {
            return Serialise(obj, _defaultSerializerOptions);
        }
        /// <summary>
        /// Serialises the specified object.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <param name="serializerOptions">A SerializerOptions containing the serialiser options to use.</param>
        /// <returns>An array of byte containing the serialised object.</returns>
        public static byte[] Serialise(T obj, SerializerOptions serializerOptions)
        {
            DataContractJsonSerializer Serialiser;
            byte[] ReturnValue;
            MemoryStream TargetStream;

            // Create the serialiser
            Serialiser = CreateSerialiser(serializerOptions);

            // Serialise the object
            TargetStream = new MemoryStream();
            Serialiser.WriteObject(TargetStream, obj);
            ReturnValue = TargetStream.ToArray();

            return ReturnValue;
        }

        /// <summary>
        /// Serialises the specified object using the default encoding and the default serialisation options.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(T obj)
        {
            return SerialiseToString(obj, _defaultSerializerOptions);
        }
        /// <summary>
        /// Serialises the specified object using the default encoding.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <param name="serializerOptions">A SerializerOptions containing the serialiser options to use.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(T obj, SerializerOptions serializerOptions)
        {
            return SerialiseToString(obj, _Encoding, serializerOptions);
        }
        /// <summary>
        /// Serialises the specified object using the default serialisation options.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <param name="encoding">An Encoding that specifies the text encoding to use during desrialisation.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(T obj, Encoding encoding)
        {
            return SerialiseToString(obj, encoding, _defaultSerializerOptions);
        }
        /// <summary>
        /// Serialises the specified object.
        /// </summary>
        /// <param name="obj">A T that is the object to serialise.</param>
        /// <param name="encoding">An Encoding that specifies the text encoding to use during desrialisation.</param>
        /// <param name="serializerOptions">A SerializerOptions containing the serialiser options to use.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(T obj, Encoding encoding, SerializerOptions serializerOptions)
        {
            byte[] Data;
            string ReturnValue = null;

            // Get the serialised object and convert to text
            Data = Serialise(obj, serializerOptions);
            if (Data != null && Data.Length > 0)
            {
                ReturnValue = encoding.GetString(Data);
                ReturnValue = ReturnValue.Replace("\0", "");
            }

            return ReturnValue;
        }

        #endregion
        #region _Private_
        private static DataContractJsonSerializer CreateSerialiser(SerializerOptions serializerOptions)
        {
            return new DataContractJsonSerializer(typeof(T), MapSettings(serializerOptions));
        }

        private static DataContractJsonSerializerSettings MapSettings(SerializerOptions serializerOptions)
        {
            DataContractJsonSerializerSettings returnValue;

            returnValue = new DataContractJsonSerializerSettings()
            {
                EmitTypeInformation = serializerOptions.EmitTypeInformation ? EmitTypeInformation.Always : EmitTypeInformation.Never
            };

            if (serializerOptions.DateTimeFormat != null)
                returnValue.DateTimeFormat = serializerOptions.DateTimeFormat;

            return returnValue;
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
                    throw new ArgumentException("The specified encoding is null", nameof(value));
                else
                    _Encoding = value;
            }
        }

        #endregion
    }
}