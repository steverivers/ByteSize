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
            var expected = MetricByteSize.FromKiloBytes(1020);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryParse()
        {
            string val = "1020KB";
            var expected = MetricByteSize.FromKiloBytes(1020);

            MetricByteSize resultMetricByteSize;
            var resultBool = MetricByteSize.TryParse(val, out resultMetricByteSize);

            Assert.True(resultBool);
            Assert.Equal(expected, resultMetricByteSize);
        }

        [Fact]
        public void ParseDecimalMB()
        {
            string val = "100.5MB";
            var expected = MetricByteSize.FromMegaBytes(100.5);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        // Failure modes
        [Fact]
        public void TryParseReturnsFalseOnBadValue()
        {
            string val = "Unexpected Value";

            MetricByteSize resultMetricByteSize;
            var resultBool = MetricByteSize.TryParse(val, out resultMetricByteSize);

            Assert.False(resultBool);
            Assert.Equal(new MetricByteSize(), resultMetricByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingMagnitude()
        {
            string val = "1000";

            MetricByteSize resultMetricByteSize;
            var resultBool = MetricByteSize.TryParse(val, out resultMetricByteSize);

            Assert.False(resultBool);
            Assert.Equal(new MetricByteSize(), resultMetricByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingValue()
        {
            string val = "KB";

            MetricByteSize resultMetricByteSize;
            var resultBool = MetricByteSize.TryParse(val, out resultMetricByteSize);

            Assert.False(resultBool);
            Assert.Equal(new MetricByteSize(), resultMetricByteSize);
        }

        [Fact]
        public void TryParseWorksWithLotsOfSpaces()
        {
            string val = " 100 KB ";
            var expected = MetricByteSize.FromKiloBytes(100);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePartialBits()
        {
            string val = "10.5b";

            Assert.Throws<FormatException>(() => { MetricByteSize.Parse(val); });
        }


        // Parse method throws exceptions
        [Fact]
        public void ParseThrowsOnInvalid()
        {
            string badValue = "Unexpected Value";

            Assert.Throws<FormatException>(() => { MetricByteSize.Parse(badValue); });
        }

        [Fact]
        public void ParseThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => { MetricByteSize.Parse(null); });
        }


        // Various magnitudes
        [Fact]
        public void ParseBits()
        {
            string val = "1b";
            var expected = MetricByteSize.FromBits(1);
            
            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseBytes()
        {
            string val = "1B";
            var expected = MetricByteSize.FromBytes(1);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseKB()
        {
            string val = "1020KB";
            var expected = MetricByteSize.FromKiloBytes(1020);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseMB()
        {
            string val = "1000MB";
            var expected = MetricByteSize.FromMegaBytes(1000);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseGB()
        {
            string val = "805GB";
            var expected = MetricByteSize.FromGigaBytes(805);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseTB()
        {
            string val = "100TB";
            var expected = MetricByteSize.FromTeraBytes(100);

            var result = MetricByteSize.Parse(val);

            Assert.Equal(expected, result);
        }
    }
}
