using System;
using System.Runtime.Serialization;

namespace MPL.Common.Runtime.Serialization
{
    /// <summary>
    /// A class that defines options for the serializer.
    /// </summary>
    public class SerializerOptions
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public SerializerOptions()
            : this(null, false)
        { }

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="dateTimeFormat">A DateTimeFormat indicating the format to use when serialising DateTime types</param>
        /// <param name="emitTypeInformation">A bool that indicates whether to emit type information for custom types.</param>
        public SerializerOptions(DateTimeFormat dateTimeFormat, bool emitTypeInformation)
        {
            DateTimeFormat = dateTimeFormat;
            EmitTypeInformation = emitTypeInformation;
        }

        /// <summary>
        /// Gets or sets the format to use when serialisiing DateTime types.
        /// </summary>
        public DateTimeFormat DateTimeFormat { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether to emit type information.
        /// </summary>
        public bool EmitTypeInformation { get; set; }
    }
}