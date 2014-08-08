using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Xunit;

namespace ByteSize.Tests
{
    public class ParsingMethods
    {
        // Base parsing functionality
        [Fact]
        public void Parse()
        {
            string val = "1020KB";
            var expected = BinaryByteSize.FromKibiBytes(1020);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryParse()
        {
            string val = "1020KB";
            var expected = BinaryByteSize.FromKibiBytes(1020);

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.True(resultBool);
            Assert.Equal(expected, resultBinaryByteSize);
        }

        [Fact]
        public void ParseDecimalMB()
        {
            string val = "100.5MB";
            var expected = BinaryByteSize.FromMebiBytes(100.5);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        // Failure modes
        [Fact]
        public void TryParseReturnsFalseOnBadValue()
        {
            string val = "Unexpected Value";

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new BinaryByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingMagnitude()
        {
            string val = "1000";

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new BinaryByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingValue()
        {
            string val = "KB";

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new BinaryByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseWorksWithLotsOfSpaces()
        {
            string val = " 100 KB ";
            var expected = BinaryByteSize.FromKibiBytes(100);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePartialBits()
        {
            string val = "10.5b";

            Assert.Throws<FormatException>(() => { BinaryByteSize.Parse(val); });
        }


        // Parse method throws exceptions
        [Fact]
        public void ParseThrowsOnInvalid()
        {
            string badValue = "Unexpected Value";

            Assert.Throws<FormatException>(() => { BinaryByteSize.Parse(badValue); });
        }

        [Fact]
        public void ParseThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => { BinaryByteSize.Parse(null); });
        }


        // Various magnitudes
        [Fact]
        public void ParseBits()
        {
            string val = "1b";
            var expected = BinaryByteSize.FromBits(1);
            
            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseBytes()
        {
            string val = "1B";
            var expected = BinaryByteSize.FromBytes(1);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseKB()
        {
            string val = "1020KB";
            var expected = BinaryByteSize.FromKibiBytes(1020);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseMB()
        {
            string val = "1000MB";
            var expected = BinaryByteSize.FromMebiBytes(1000);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseGB()
        {
            string val = "805GB";
            var expected = BinaryByteSize.FromGibiBytes(805);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseTB()
        {
            string val = "100TB";
            var expected = BinaryByteSize.FromTebiBytes(100);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }
    }
}
