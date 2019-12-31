using System;
using System.Reflection;
using System.Text;

namespace MPL.Common.Runtime.Serialization
{
    /// <summary>
    /// A class that provides serialisation functions.
    /// </summary>
    public static class Serializer
    {
        #region Constructors
        /// <summary>
        /// Creates a new static instance of the class.
        /// </summary>
        static Serializer()
        {
            _BindingFlags = BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public;
            _Encoding = Encoding.Default;
        }

        #endregion

        #region Declarations
        #region _Members_
        private static readonly BindingFlags _BindingFlags;
        private static Encoding _Encoding;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private static Type GetGenericSerialiser(object obj)
        {
            Type ReturnValue;

            ReturnValue = typeof(Serializer<>);
            ReturnValue = ReturnValue.MakeGenericType(obj.GetType());

            return ReturnValue;
        }

        private static object InvokeGenericMember(object obj, string memberName, object parameter)
        {
            return InvokeGenericMember(obj, memberName, new object[] { parameter });
        }
        private static object InvokeGenericMember(object obj, string memberName, object[] parameters)
        {
            Type ObjectSerialiser;
            object ReturnValue;

            ObjectSerialiser = GetGenericSerialiser(obj);
            ReturnValue = ObjectSerialiser.InvokeMember(memberName, _BindingFlags, null, null, parameters);

            return ReturnValue;
        }

        private static object InvokeMember(Type serialiserType, string memberName, object parameter)
        {
            return InvokeMember(serialiserType, memberName, new object[] { parameter });
        }
        private static object InvokeMember(Type serialiserType, string memberName, object[] parameters)
        {
            object ReturnValue;

            ReturnValue = serialiserType.InvokeMember(memberName, _BindingFlags, null, null, parameters);

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Serialises the specified object.
        /// </summary>
        /// <param name="jsonObject">An object that is the object to serialise.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(object jsonObject)
        {
            return SerialiseToString(jsonObject, _Encoding);
        }
        /// <summary>
        /// Serialises the specified object.
        /// </summary>
        /// <param name="jsonObject">An object that is the object to serialise.</param>
        /// <param name="encoding">An Encoding that specifies the text encoding to use during desrialisation.</param>
        /// <returns>A string containing the serialised object.</returns>
        public static string SerialiseToString(object jsonObject, Encoding encoding)
        {
            object Result;
            string ReturnValue = null;

            Result = InvokeGenericMember(jsonObject, "SerialiseToString", new object[] { jsonObject, encoding });
            if (Result != null)
                ReturnValue = Result.ToString();

            return ReturnValue;
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