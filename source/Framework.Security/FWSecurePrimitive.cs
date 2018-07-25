using Framework.Security.Cryptography;
using System;

namespace Framework.Security
{
    /// <summary>
    /// Abstract class for URL safe encryp-decryption.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public abstract class FWSecurePrimitive<TValue>
        where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
    {
        /// <summary>
        /// Encription key.
        /// </summary>
        protected static readonly string _key = "dns#ksli3kdFk@df";

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Converts a <see cref="FWSecurePrimitive&lt;TValue&gt;"/> into a string.
        /// </summary>
        /// <param name="value">The <see cref="FWSecurePrimitive&lt;TValue&gt;"/> object.</param>
        public static implicit operator string(FWSecurePrimitive<TValue> value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Converts a <see cref="FWSecurePrimitive&lt;TValue&gt;"/> to its value type.
        /// </summary>
        /// <param name="value">The <see cref="FWSecurePrimitive&lt;TValue&gt;"/> object.</param>
        public static implicit operator TValue(FWSecurePrimitive<TValue> value)
        {
            return value.Value;
        }

        /// <summary>
        /// Returns the value encrypted.
        /// </summary>
        /// <returns>The encrypted value.</returns>
        public override string ToString()
        {
            var encryption = FWEncryption.Create(true);
            return encryption.Encrypt(Value.ToString(), _key);
        }
    }
}
