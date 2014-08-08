using System;

namespace ByteSize
{
    /// <summary>
    /// Deprecated
    /// Use either MetricByteSize or BinaryByteSize
    /// </summary>
    public struct ByteSize : IComparable<ByteSize>, IEquatable<ByteSize>
    {
        public static readonly ByteSize MinValue = ByteSize.FromBits(long.MinValue);
        public static readonly ByteSize MaxValue = ByteSize.FromBits(long.MaxValue);

        public const long BitsInByte = 8;
        public const long BytesInKiloByte = 1024;
        public const long BytesInMegaByte = 1048576;
        public const long BytesInGigaByte = 1073741824;
        public const long BytesInTeraByte = 1099511627776;

        public const string BitSymbol = "b";
        public const string ByteSymbol = "B";
        public const string KiloByteSymbol = "KB";
        public const string MegaByteSymbol = "MB";
        public const string GigaByteSymbol = "GB";
        public const string TeraByteSymbol = "TB";

        public long Bits { get; private set; }
        public double Bytes { get; private set; }
        public double KiloBytes { get; private set; }
        public double MegaBytes { get; private set; }
        public double GigaBytes { get; private set; }
        public double TeraBytes { get; private set; }

        public string LargestWholeNumberSymbol
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.TeraBytes) >= 1)
                    return ByteSize.TeraByteSymbol;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return ByteSize.GigaByteSymbol;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return ByteSize.MegaByteSymbol;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return ByteSize.KiloByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return ByteSize.ByteSymbol;

                return ByteSize.BitSymbol;
            }
        }
        public double LargestWholeNumberValue
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.TeraBytes) >= 1)
                    return this.TeraBytes;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return this.GigaBytes;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return this.MegaBytes;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return this.KiloBytes;

                if (Math.Abs(this.Bytes) >= 1)
                    return this.Bytes;

                return this.Bits;
            }
        }

        public ByteSize(double byteSize)
            : this()
        {
            // Get ceiling because bis are whole units
            Bits = (long)Math.Ceiling(byteSize * BitsInByte);

            Bytes = byteSize;
            KiloBytes = byteSize / BytesInKiloByte;
            MegaBytes = byteSize / BytesInMegaByte;
            GigaBytes = byteSize / BytesInGigaByte;
            TeraBytes = byteSize / BytesInTeraByte;
        }

        public static ByteSize FromBits(long value)
        {
            return new ByteSize(value / (double)BitsInByte);
        }

        public static ByteSize FromBytes(double value)
        {
            return new ByteSize(value);
        }

        public static ByteSize FromKiloBytes(double value)
        {
            return new ByteSize(value * BytesInKiloByte);
        }

        public static ByteSize FromMegaBytes(double value)
        {
            return new ByteSize(value * BytesInMegaByte);
        }

        public static ByteSize FromGigaBytes(double value)
        {
            return new ByteSize(value * BytesInGigaByte);
        }

        public static ByteSize FromTeraBytes(double value)
        {
            return new ByteSize(value * BytesInTeraByte);
        }

        /// <summary>
        /// Converts the value of the current ByteSize object to a string.
        /// The metric prefix symbol (bit, byte, kilo, mega, giga, tera) used is
        /// the largest metric prefix such that the corresponding value is greater
        //  than or equal to one.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.LargestWholeNumberValue, this.LargestWholeNumberSymbol);
        }

        public string ToString(string format)
        {
            if (!format.Contains("#") && !format.Contains("0"))
                format = "#.## " + format;

            Func<string, bool> has = s => format.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1;
            Func<double, string> output = n => n.ToString(format);

            if (has("TB"))
                return output(this.TeraBytes);
            if (has("GB"))
                return output(this.GigaBytes);
            if (has("MB"))
                return output(this.MegaBytes);
            if (has("KB"))
                return output(this.KiloBytes);

            // Byte and Bit symbol look must be case-sensitive
            if (format.IndexOf(ByteSize.ByteSymbol) != -1)
                return output(this.Bytes);

            if (format.IndexOf(ByteSize.BitSymbol) != -1)
                return output(this.Bits);

            return string.Format("{0} {1}", this.LargestWholeNumberValue.ToString(format), this.LargestWholeNumberSymbol);
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            ByteSize other;
            if (value is ByteSize)
                other = (ByteSize)value;
            else
                return false;

            return Equals(other);
        }

        public bool Equals(ByteSize value)
        {
            return this.Bits == value.Bits;
        }

        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        public int CompareTo(ByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        public ByteSize Add(ByteSize bs)
        {
            return new ByteSize(this.Bits + bs.Bits);
        }

        public ByteSize AddBits(long value)
        {
            return new ByteSize(this.Bits + value);
        }

        public ByteSize AddBytes(double value)
        {
            return this + ByteSize.FromBytes(value);
        }

        public ByteSize AddKiloBytes(double value)
        {
            return this + ByteSize.FromKiloBytes(value);
        }

        public ByteSize AddMegaBytes(double value)
        {
            return this + ByteSize.FromMegaBytes(value);
        }

        public ByteSize AddGigaBytes(double value)
        {
            return this + ByteSize.FromGigaBytes(value);
        }

        public ByteSize AddTeraBytes(double value)
        {
            return this + ByteSize.FromTeraBytes(value);
        }

        public ByteSize Subtract(ByteSize bs)
        {
            return new ByteSize(this.Bits - bs.Bits);
        }

        public static ByteSize operator +(ByteSize b1, ByteSize b2)
        {
            return new ByteSize(b1.Bits + b2.Bits);
        }

        public static ByteSize operator ++(ByteSize b)
        {
            return new ByteSize(b.Bits++);
        }

        public static ByteSize operator -(ByteSize b)
        {
            return new ByteSize(-b.Bits);
        }

        public static ByteSize operator --(ByteSize b)
        {
            return new ByteSize(b.Bits--);
        }

        public static bool operator ==(ByteSize b1, ByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        public static bool operator !=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        public static bool operator <(ByteSize b1, ByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        public static bool operator <=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        public static bool operator >(ByteSize b1, ByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        public static bool operator >=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }

        public static bool TryParse(string s, out ByteSize result)
        {
            // Arg checking
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException("s", "String is null or whitespace");

            // Setup the result
            result = new ByteSize();

            // Get the index of the first non-digit character
            s = s.TrimStart(); // Protect against leading spaces

            var num = 0;
            var found = false;

            // Pick first non-digit number
            for (num = 0; num < s.Length; num++)
                if (!(char.IsDigit(s[num]) || s[num] == '.'))
                {
                    found = true;
                    break;
                }

            if (found == false)
                return false;

            int lastNumber = num;

            // Cut the input string in half
            string numberPart = s.Substring(0, lastNumber).Trim();
            string sizePart = s.Substring(lastNumber, s.Length - lastNumber).Trim();

            // Get the numeric part
            double number;
            if (!double.TryParse(numberPart, out number))
                return false;

            // Get the magnitude part
            switch (sizePart)
            {
                case "b":
                    if (number % 1 != 0) // Can't have partial bits
                        return false;

                    result = FromBits((long)number);
                    break;

                case "B":
                    result = FromBytes(number);
                    break;

                case "KB":
                case "kB":
                case "kb":
                    result = FromKiloBytes(number);
                    break;

                case "MB":
                case "mB":
                case "mb":
                    result = FromMegaBytes(number);
                    break;

                case "GB":
                case "gB":
                case "gb":
                    result = FromGigaBytes(number);
                    break;

                case "TB":
                case "tB":
                case "tb":
                    result = FromTeraBytes(number);
                    break;
            }

            return true;
        }

        public static ByteSize Parse(string s)
        {
            ByteSize result;

            if (TryParse(s, out result))
                return result;

            throw new FormatException("Value is not in the correct format");
        }
    }

    /// <summary>
    /// Represents a byte size value.
    /// </summary>
    public struct MetricByteSize : IComparable<MetricByteSize>, IEquatable<MetricByteSize>
    {
        public static readonly MetricByteSize MinValue = MetricByteSize.FromBits(long.MinValue);
        public static readonly MetricByteSize MaxValue = MetricByteSize.FromBits(long.MaxValue);

        public const long BitsInByte = 8; //0123456789012
        public const long BytesInKiloByte = 1000;
        public const long BytesInMegaByte = 1000000;
        public const long BytesInGigaByte = 1000000000;
        public const long BytesInTeraByte = 1000000000000;

        public const string BitSymbol = "b";
        public const string ByteSymbol = "B";
        public const string KiloByteSymbol = "KB";
        public const string MegaByteSymbol = "MB";
        public const string GigaByteSymbol = "GB";
        public const string TeraByteSymbol = "TB";

        public long Bits { get; private set; }
        public double Bytes { get; private set; }
        public double KiloBytes { get; private set; }
        public double MegaBytes { get; private set; }
        public double GigaBytes { get; private set; }
        public double TeraBytes { get; private set; }

        public string LargestWholeNumberSymbol
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.TeraBytes) >= 1)
                    return MetricByteSize.TeraByteSymbol;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return MetricByteSize.GigaByteSymbol;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return MetricByteSize.MegaByteSymbol;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return MetricByteSize.KiloByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return MetricByteSize.ByteSymbol;

                return MetricByteSize.BitSymbol;
            }
        }
        public double LargestWholeNumberValue
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.TeraBytes) >= 1)
                    return this.TeraBytes;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return this.GigaBytes;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return this.MegaBytes;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return this.KiloBytes;

                if (Math.Abs(this.Bytes) >= 1)
                    return this.Bytes;

                return this.Bits;
            }
        }

        public MetricByteSize(double byteSize)
            : this()
        {
            // Get ceiling because bis are whole units
            Bits = (long)Math.Ceiling(byteSize * BitsInByte);

            Bytes = byteSize;
            KiloBytes = byteSize / BytesInKiloByte;
            MegaBytes = byteSize / BytesInMegaByte;
            GigaBytes = byteSize / BytesInGigaByte;
            TeraBytes = byteSize / BytesInTeraByte;
        }

        public static MetricByteSize FromBits(long value)
        {
            return new MetricByteSize(value / (double)BitsInByte);
        }

        public static MetricByteSize FromBytes(double value)
        {
            return new MetricByteSize(value);
        }

        public static MetricByteSize FromKiloBytes(double value)
        {
            return new MetricByteSize(value * BytesInKiloByte);
        }

        public static MetricByteSize FromMegaBytes(double value)
        {
            return new MetricByteSize(value * BytesInMegaByte);
        }

        public static MetricByteSize FromGigaBytes(double value)
        {
            return new MetricByteSize(value * BytesInGigaByte);
        }

        public static MetricByteSize FromTeraBytes(double value)
        {
            return new MetricByteSize(value * BytesInTeraByte);
        }

        /// <summary>
        /// Converts the value of the current MetricByteSize object to a string.
        /// The metric prefix symbol (bit, byte, kilo, mega, giga, tera) used is
        /// the largest metric prefix such that the corresponding value is greater
        //  than or equal to one.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.LargestWholeNumberValue, this.LargestWholeNumberSymbol);
        }

        public string ToString(string format)
        {
            if (!format.Contains("#") && !format.Contains("0"))
                format = "#.## " + format;

            Func<string, bool> has = s => format.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1;
            Func<double, string> output = n => n.ToString(format);

            if (has("TB"))
                return output(this.TeraBytes);
            if (has("GB"))
                return output(this.GigaBytes);
            if (has("MB"))
                return output(this.MegaBytes);
            if (has("KB"))
                return output(this.KiloBytes);

            // Byte and Bit symbol look must be case-sensitive
            if (format.IndexOf(MetricByteSize.ByteSymbol) != -1)
                return output(this.Bytes);

            if (format.IndexOf(MetricByteSize.BitSymbol) != -1)
                return output(this.Bits);

            return string.Format("{0} {1}", this.LargestWholeNumberValue.ToString(format), this.LargestWholeNumberSymbol);
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            MetricByteSize other;
            if (value is MetricByteSize)
                other = (MetricByteSize)value;
            else
                return false;

            return Equals(other);
        }

        public bool Equals(MetricByteSize value)
        {
            return this.Bits == value.Bits;
        }

        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        public int CompareTo(MetricByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        public MetricByteSize Add(MetricByteSize bs)
        {
            return new MetricByteSize(this.Bits + bs.Bits);
        }

        public MetricByteSize AddBits(long value)
        {
            return new MetricByteSize(this.Bits + value);
        }

        public MetricByteSize AddBytes(double value)
        {
            return this + MetricByteSize.FromBytes(value);
        }

        public MetricByteSize AddKiloBytes(double value)
        {
            return this + MetricByteSize.FromKiloBytes(value);
        }

        public MetricByteSize AddMegaBytes(double value)
        {
            return this + MetricByteSize.FromMegaBytes(value);
        }

        public MetricByteSize AddGigaBytes(double value)
        {
            return this + MetricByteSize.FromGigaBytes(value);
        }

        public MetricByteSize AddTeraBytes(double value)
        {
            return this + MetricByteSize.FromTeraBytes(value);
        }

        public MetricByteSize Subtract(MetricByteSize bs)
        {
            return new MetricByteSize(this.Bits - bs.Bits);
        }

        public static MetricByteSize operator +(MetricByteSize b1, MetricByteSize b2)
        {
            return new MetricByteSize(b1.Bits + b2.Bits);
        }

        public static MetricByteSize operator ++(MetricByteSize b)
        {
            return new MetricByteSize(b.Bits++);
        }

        public static MetricByteSize operator -(MetricByteSize b)
        {
            return new MetricByteSize(-b.Bits);
        }

        public static MetricByteSize operator --(MetricByteSize b)
        {
            return new MetricByteSize(b.Bits--);
        }

        public static bool operator ==(MetricByteSize b1, MetricByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        public static bool operator !=(MetricByteSize b1, MetricByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        public static bool operator <(MetricByteSize b1, MetricByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        public static bool operator <=(MetricByteSize b1, MetricByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        public static bool operator >(MetricByteSize b1, MetricByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        public static bool operator >=(MetricByteSize b1, MetricByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }

        public static bool TryParse(string s, out MetricByteSize result)
        {
            // Arg checking
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException("s", "String is null or whitespace");

            // Setup the result
            result = new MetricByteSize();

            // Get the index of the first non-digit character
            s = s.TrimStart(); // Protect against leading spaces

            var num = 0;
            var found = false;

            // Pick first non-digit number
            for (num = 0; num < s.Length; num++)
                if (!(char.IsDigit(s[num]) || s[num] == '.'))
                {
                    found = true;
                    break;
                }

            if (found == false)
                return false;

            int lastNumber = num;

            // Cut the input string in half
            string numberPart = s.Substring(0, lastNumber).Trim();
            string sizePart = s.Substring(lastNumber, s.Length - lastNumber).Trim();

            // Get the numeric part
            double number;
            if (!double.TryParse(numberPart, out number))
                return false;

            // Get the magnitude part
            switch (sizePart)
            {
                case "b":
                    if (number % 1 != 0) // Can't have partial bits
                        return false;

                    result = FromBits((long)number);
                    break;

                case "B":
                    result = FromBytes(number);
                    break;

                case "KB":
                case "kB":
                case "kb":
                    result = FromKiloBytes(number);
                    break;

                case "MB":
                case "mB":
                case "mb":
                    result = FromMegaBytes(number);
                    break;

                case "GB":
                case "gB":
                case "gb":
                    result = FromGigaBytes(number);
                    break;

                case "TB":
                case "tB":
                case "tb":
                    result = FromTeraBytes(number);
                    break;
            }

            return true;
        }

        public static MetricByteSize Parse(string s)
        {
            MetricByteSize result;

            if (TryParse(s, out result))
                return result;

            throw new FormatException("Value is not in the correct format");
        }
    }


    /// <summary>
    /// Represents a byte size value.
    /// </summary>
    public struct BinaryByteSize : IComparable<BinaryByteSize>, IEquatable<BinaryByteSize>
    {
        public static readonly BinaryByteSize MinValue = BinaryByteSize.FromBits(long.MinValue);
        public static readonly BinaryByteSize MaxValue = BinaryByteSize.FromBits(long.MaxValue);

        public const long BitsInByte = 8;
        public const long BytesInKibiByte = 1024;
        public const long BytesInMebiByte = 1048576;
        public const long BytesInGibiByte = 1073741824;
        public const long BytesInTebiByte = 1099511627776;

        public const string BitSymbol = "b";
        public const string ByteSymbol = "B";
        public const string KibiByteSymbol = "KiB";
        public const string MebiByteSymbol = "MiB";
        public const string GibiByteSymbol = "GiB";
        public const string TebiByteSymbol = "TiB";

        public long Bits { get; private set; }
        public double Bytes { get; private set; }
        public double KibiBytes { get; private set; }
        public double MebiBytes { get; private set; }
        public double GibiBytes { get; private set; }
        public double TebiBytes { get; private set; }

        public string LargestWholeNumberSymbol
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.TebiBytes) >= 1)
                    return BinaryByteSize.TebiByteSymbol;

                if (Math.Abs(this.GibiBytes) >= 1)
                    return BinaryByteSize.GibiByteSymbol;

                if (Math.Abs(this.MebiBytes) >= 1)
                    return BinaryByteSize.MebiByteSymbol;

                if (Math.Abs(this.KibiBytes) >= 1)
                    return BinaryByteSize.KibiByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return BinaryByteSize.ByteSymbol;

                return BinaryByteSize.BitSymbol;
            }
        }
        public double LargestWholeNumberValue
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.TebiBytes) >= 1)
                    return this.TebiBytes;

                if (Math.Abs(this.GibiBytes) >= 1)
                    return this.GibiBytes;

                if (Math.Abs(this.MebiBytes) >= 1)
                    return this.MebiBytes;

                if (Math.Abs(this.KibiBytes) >= 1)
                    return this.KibiBytes;

                if (Math.Abs(this.Bytes) >= 1)
                    return this.Bytes;

                return this.Bits;
            }
        }

        public BinaryByteSize(double byteSize)
            : this()
        {
            // Get ceiling because bis are whole units
            Bits = (long)Math.Ceiling(byteSize * BitsInByte);

            Bytes = byteSize;
            KibiBytes = byteSize / BytesInKibiByte;
            MebiBytes = byteSize / BytesInMebiByte;
            GibiBytes = byteSize / BytesInGibiByte;
            TebiBytes = byteSize / BytesInTebiByte;
        }

        public static BinaryByteSize FromBits(long value)
        {
            return new BinaryByteSize(value / (double)BitsInByte);
        }

        public static BinaryByteSize FromBytes(double value)
        {
            return new BinaryByteSize(value);
        }

        public static BinaryByteSize FromKibiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInKibiByte);
        }

        public static BinaryByteSize FromMebiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInMebiByte);
        }

        public static BinaryByteSize FromGibiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInGibiByte);
        }

        public static BinaryByteSize FromTebiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInTebiByte);
        }

        /// <summary>
        /// Converts the value of the current BinaryByteSize object to a string.
        /// The metric prefix symbol (bit, byte, kibi, mebi, gibi, tebi) used is
        /// the largest metric prefix such that the corresponding value is greater
        //  than or equal to one.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.LargestWholeNumberValue, this.LargestWholeNumberSymbol);
        }

        public string ToString(string format)
        {
            if (!format.Contains("#") && !format.Contains("0"))
                format = "#.## " + format;

            Func<string, bool> has = s => format.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1;
            Func<double, string> output = n => n.ToString(format);

            if (has("TiB"))
                return output(this.TebiBytes);
            if (has("GiB"))
                return output(this.GibiBytes);
            if (has("MiB"))
                return output(this.MebiBytes);
            if (has("KiB"))
                return output(this.KibiBytes);

            // Byte and Bit symbol look must be case-sensitive
            if (format.IndexOf(BinaryByteSize.ByteSymbol) != -1)
                return output(this.Bytes);

            if (format.IndexOf(BinaryByteSize.BitSymbol) != -1)
                return output(this.Bits);

            return string.Format("{0} {1}", this.LargestWholeNumberValue.ToString(format), this.LargestWholeNumberSymbol);
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            BinaryByteSize other;
            if (value is BinaryByteSize)
                other = (BinaryByteSize)value;
            else
                return false;

            return Equals(other);
        }

        public bool Equals(BinaryByteSize value)
        {
            return this.Bits == value.Bits;
        }

        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        public int CompareTo(BinaryByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        public BinaryByteSize Add(BinaryByteSize bs)
        {
            return new BinaryByteSize(this.Bits + bs.Bits);
        }

        public BinaryByteSize AddBits(long value)
        {
            return new BinaryByteSize(this.Bits + value);
        }

        public BinaryByteSize AddBytes(double value)
        {
            return this + BinaryByteSize.FromBytes(value);
        }

        public BinaryByteSize AddKibiBytes(double value)
        {
            return this + BinaryByteSize.FromKibiBytes(value);
        }

        public BinaryByteSize AddMebiBytes(double value)
        {
            return this + BinaryByteSize.FromMebiBytes(value);
        }

        public BinaryByteSize AddGibiBytes(double value)
        {
            return this + BinaryByteSize.FromGibiBytes(value);
        }

        public BinaryByteSize AddTebiBytes(double value)
        {
            return this + BinaryByteSize.FromTebiBytes(value);
        }

        public BinaryByteSize Subtract(BinaryByteSize bs)
        {
            return new BinaryByteSize(this.Bits - bs.Bits);
        }

        public static BinaryByteSize operator +(BinaryByteSize b1, BinaryByteSize b2)
        {
            return new BinaryByteSize(b1.Bits + b2.Bits);
        }

        public static BinaryByteSize operator ++(BinaryByteSize b)
        {
            return new BinaryByteSize(b.Bits++);
        }

        public static BinaryByteSize operator -(BinaryByteSize b)
        {
            return new BinaryByteSize(-b.Bits);
        }

        public static BinaryByteSize operator --(BinaryByteSize b)
        {
            return new BinaryByteSize(b.Bits--);
        }

        public static bool operator ==(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        public static bool operator !=(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        public static bool operator <(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        public static bool operator <=(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        public static bool operator >(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        public static bool operator >=(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }

        public static bool TryParse(string s, out BinaryByteSize result)
        {
            // Arg checking
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException("s", "String is null or whitespace");

            // Setup the result
            result = new BinaryByteSize();

            // Get the index of the first non-digit character
            s = s.TrimStart(); // Protect against leading spaces

            var num = 0;
            var found = false;

            // Pick first non-digit number
            for (num = 0; num < s.Length; num++)
                if (!(char.IsDigit(s[num]) || s[num] == '.'))
                {
                    found = true;
                    break;
                }

            if (found == false)
                return false;

            int lastNumber = num;

            // Cut the input string in half
            string numberPart = s.Substring(0, lastNumber).Trim();
            string sizePart = s.Substring(lastNumber, s.Length - lastNumber).Trim();

            // Get the numeric part
            double number;
            if (!double.TryParse(numberPart, out number))
                return false;

            // Get the magnitude part
            switch (sizePart)
            {
                case "b":
                    if (number % 1 != 0) // Can't have partial bits
                        return false;

                    result = FromBits((long)number);
                    break;

                case "B":
                    result = FromBytes(number);
                    break;

                case "KB":
                case "kB":
                case "kb":
                    result = FromKibiBytes(number);
                    break;

                case "MB":
                case "mB":
                case "mb":
                    result = FromMebiBytes(number);
                    break;

                case "GB":
                case "gB":
                case "gb":
                    result = FromGibiBytes(number);
                    break;

                case "TB":
                case "tB":
                case "tb":
                    result = FromTebiBytes(number);
                    break;
            }

            return true;
        }

        public static BinaryByteSize Parse(string s)
        {
            BinaryByteSize result;

            if (TryParse(s, out result))
                return result;

            throw new FormatException("Value is not in the correct format");
        }
    }

}
