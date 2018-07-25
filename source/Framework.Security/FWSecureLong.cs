using Framework.Model;
using Framework.Security.Cryptography;
using Framework.Security.Mapper;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Framework.Security
{
    /// <summary>
    /// Encapsulates URL safe long encryp-decryption.
    /// </summary>
    [TypeConverter(typeof(FWLongSecureConverter))]
    public sealed class FWSecureLong : FWSecurePrimitive<long>
    {
        static FWSecureLong()
        {
            FWMapperHelper.AddMap<FWSecureLong, long, FWSecureLongInjection>();
            FWMapperHelper.AddMap<long, FWSecureLong, FWSecureLongInjection>();
        }

        /// <summary>
        /// Convert a long to a <see cref="FWSecureLong"/> object.
        /// </summary>
        /// <param name="value">The long value.</param>
        public static implicit operator FWSecureLong(long value)
        {
            var slong = new FWSecureLong() { Value = value };
            return slong;
        }

        /// <summary>
        /// Converts an encrypted string into a <see cref="FWSecureLong"/> object.
        /// </summary>
        /// <param name="value">The string value.</param>
        public static implicit operator FWSecureLong(string value)
        {
            var secure = new FWSecureLong();

            var encryption = FWEncryption.Create(true);
            try
            {
                var svalue = encryption.Decrypt(value, _key);
                long.TryParse(svalue, out long result);
                secure.Value = result;
            }
            catch (Exception)
            { }

            return secure;
        }
    }

    class FWLongSecureConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (FWSecureLong)value.ToString();
        }
    }
}
