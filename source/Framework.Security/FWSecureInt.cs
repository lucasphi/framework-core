using Framework.Model;
using Framework.Security.Cryptography;
using Framework.Security.Mapper;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Framework.Security
{
    /// <summary>
    /// Encapsulates URL safe int encryp-decryption.
    /// </summary>
    [TypeConverter(typeof(FWIntSecureConverter))]
    public sealed class FWSecureInt : FWSecurePrimitive<int>
    {
        static FWSecureInt()
        {
            FWMapperHelper.AddMap<FWSecureInt, int, FWSecureIntInjection>();
            FWMapperHelper.AddMap<int, FWSecureInt, FWSecureIntInjection>();
        }

        /// <summary>
        /// Convert a int to a <see cref="FWSecureInt"/> object.
        /// </summary>
        /// <param name="value">The int value.</param>
        public static implicit operator FWSecureInt(int value)
        {
            var sint = new FWSecureInt() { Value = value };
            return sint;
        }

        /// <summary>
        /// Converts an encrypted string into a <see cref="FWSecureInt"/> object.
        /// </summary>
        /// <param name="value">The string value.</param>
        public static implicit operator FWSecureInt(string value)
        {
            var secure = new FWSecureInt();

            var encryption = FWEncryption.Create(true);
            try
            {
                var svalue = encryption.Decrypt(value, _key);
                int.TryParse(svalue, out int result);
                secure.Value = result;
            }
            catch (Exception)
            { }

            return secure;
        }
    }

    class FWIntSecureConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (FWSecureInt)value.ToString();
        }
    }
}
