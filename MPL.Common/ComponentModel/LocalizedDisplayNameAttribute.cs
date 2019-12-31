using System;
using System.ComponentModel;
using System.Reflection;

namespace MPL.Common.ComponentModel
{
    /// <summary>
     /// A class that defines a localised DisplayNameAttribute.
     /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Event | AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public LocalizedDisplayNameAttribute()
            : base()
        { }

        #endregion

        #region Declarations
        #region _Members_
        private string _DisplayName;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private void LoadDisplayName()
        {
            _DisplayName = null;

            if (ResourceType != null)
            {
                PropertyInfo ResourceProperty;

                ResourceProperty = ResourceType.GetProperty(Name);
                if (ResourceProperty != null)
                {
                    object Value;

                    Value = ResourceProperty.GetValue(null);
                    if (Value != null)
                        _DisplayName = Value.ToString();
                }
            }
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the localised display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                if (_DisplayName == null)
                    LoadDisplayName();

                return _DisplayName;
            }
        }

        /// <summary>
        /// Gets or sets the name of the resource to use.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource file to use.
        /// </summary>
        public Type ResourceType { get; set; }

        #endregion
    }
}