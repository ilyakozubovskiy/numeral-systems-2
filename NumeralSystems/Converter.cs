using System;

namespace NumeralSystems
{
    /// <summary>
    /// Converts a string representations of a numbers to its integer equivalent.
    /// </summary>
    public static class Converter
    {
        public static int ParseFromOctal(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int value = 0;
            for (int i = source.Length - 1, count = 1; i >= 0; i--, count *= 8)
            {
                if (source[i] >= '0' && source[i] <= '7')
                {
                    value += (source[i] - 48) * count;
                }
                else
                {
                    throw new ArgumentException($"{nameof(source)} does not represent a number in octal numeral system.");
                }
            }

            return value;
        }

        public static int ParseFromDecimal(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int value = 0;
            for (int i = source.Length - 1, count = 1; i >= 0; i--, count *= 10)
            {
                if (source[i] >= '0' && source[i] <= '9')
                {
                    value += (source[i] - 48) * count;
                }
                else if (source[i] == '-')
                {
                    value *= -1;
                }
                else
                {
                    throw new ArgumentException($"{nameof(source)} does not represent a number in decimal numeral system.");
                }
            }

            return value;
        }

        public static int ParseFromHex(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int value = 0;
            for (int i = source.Length - 1, count = 1; i >= 0; i--, count *= 16)
            {
                if ((source[i] - 48) < 10)
                {
                    value += (source[i] - 48) * count;
                }
                else if (source[i] >= 'A' && (source[i] <= 'F'))
                {
                    value += (source[i] - 55) * count;
                }
                else if (source[i] >= 'a' && (source[i] <= 'f'))
                {
                    value += (source[i] - 87) * count;
                }
                else
                {
                    throw new ArgumentException($"{nameof(source)} does not represent a number in hex numeral system.");
                }
            }

            return value;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-octal alphabetic characters).
        /// Valid octal alphabetic characters: 0,1,2,3,4,5,6,7.
        /// </exception>
        public static int ParsePositiveFromOctal(this string source)
        {
            int value = ParseFromOctal(source);
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(source)} does not represent a positive number in the octal numeral system.");
            }

            return value;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-decimal alphabetic characters).
        /// Valid decimal alphabetic characters: 0,1,2,3,4,5,6,7,8,9.
        /// </exception>
        public static int ParsePositiveFromDecimal(this string source)
        {
            int value = ParseFromDecimal(source);
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(source)} does not represent a positive number in the decimal numeral system.");
            }

            return value;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-hex alphabetic characters).
        /// Valid hex alphabetic characters: 0,1,2,3,4,5,6,7,8,9,A(or a),B(or b),C(or c),D(or d),E(or e),F(or f).
        /// </exception>
        public static int ParsePositiveFromHex(this string source)
        {
            int value = ParseFromHex(source);
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(source)} does not represent a positive number in the hex numeral system.");
            }

            return value;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParsePositiveByRadix(this string source, int radix)
        {
            return radix switch
            {
                8 => ParsePositiveFromOctal(source),
                10 => ParsePositiveFromDecimal(source),
                16 => ParsePositiveFromHex(source),
                _ => throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.")
            };
        }

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A signed decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParseByRadix(this string source, int radix)
        {
            return radix switch
            {
                8 => ParseFromOctal(source),
                10 => ParseFromDecimal(source),
                16 => ParseFromHex(source),
                _ => throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.")
            };
        }

        public static bool TryParseFromOctal(this string source, out int value)
        {
            value = 0;
            if (source == null)
            {
                return false;
            }

            for (int i = source.Length - 1, count = 1; i >= 0; i--, count *= 8)
            {
                if (source[i] >= '0' && source[i] <= '7')
                {
                    value += (source[i] - 48) * count;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryParseFromDecimal(this string source, out int value)
        {
            value = 0;
            if (source == null)
            {
                return false;
            }

            for (int i = source.Length - 1, count = 1; i >= 0; i--, count *= 10)
            {
                if (source[i] >= '0' && source[i] <= '9')
                {
                   value += (source[i] - 48) * count;
                }
                else if (source[i] == '-')
                {
                    value *= -1;
                }
                else
                {
                   return false;
                }
            }

            return true;
        }

        public static bool TryParseFromHex(this string source, out int value)
        {
            value = 0;
            if (source == null)
            {
                return false;
            }

            for (int i = source.Length - 1, count = 1; i >= 0; i--, count *= 16)
            {
                if ((source[i] - 48) < 10)
                {
                    value += (source[i] - 48) * count;
                }
                else if (source[i] >= 'A' && (source[i] <= 'F'))
                {
                    value += (source[i] - 55) * count;
                }
                else if (source[i] >= 'a' && (source[i] <= 'f'))
                {
                    value += (source[i] - 87) * count;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromOctal(this string source, out int value)
        {
            if (TryParseFromOctal(source, out value) && value > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">a positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromDecimal(this string source, out int value)
        {
            if (TryParseFromDecimal(source, out value) && value > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">a positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromHex(this string source, out int value)
        {
            if (TryParseFromHex(source, out value) && value > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">a positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParsePositiveByRadix(this string source, int radix, out int value)
        {
            if (!(radix == 8 || radix == 10 || radix == 16))
            {
                throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.");
            }

            value = 0;

            if (radix == 8 && TryParsePositiveFromOctal(source, out value))
            {
                return true;
            }

            if (radix == 10 && TryParsePositiveFromDecimal(source, out value))
            {
                return true;
            }

            if (radix == 16 && TryParsePositiveFromHex(source, out value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">a positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParseByRadix(this string source, int radix, out int value)
        {
            if (!(radix == 8 || radix == 10 || radix == 16))
            {
                throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.");
            }

            value = 0;

            if (radix == 8 && TryParseFromOctal(source, out value))
            {
                return true;
            }

            if (radix == 10 && TryParseFromDecimal(source, out value))
            {
                return true;
            }

            if (radix == 16 && TryParseFromHex(source, out value))
            {
                return true;
            }

            return false;
        }
    }
}
