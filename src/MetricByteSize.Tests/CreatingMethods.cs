using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ByteSize.Tests
{
    public class CreatingMethods
    {
        [Fact]
        public void Constructor()
        {
            // Arrange
            double metricByteSize = 1000000000000;

            // Act
            var result = new MetricByteSize(metricByteSize);

            // Assert
            Assert.Equal(8e12, result.Bits);
            Assert.Equal(1e12, result.Bytes);
            Assert.Equal(1e9, result.KiloBytes);
            Assert.Equal(1e6, result.MegaBytes);
            Assert.Equal(1e3, result.GigaBytes);
            Assert.Equal(1, result.TeraBytes);
        }

        [Fact]
        public void FromBitsMethod()
        {
            // Arrange
            long value = 8;

            // Act
            var result = MetricByteSize.FromBits(value);

            // Assert
            Assert.Equal(8, result.Bits);
            Assert.Equal(1, result.Bytes);
        }

        [Fact]
        public void FromBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = MetricByteSize.FromBytes(value);

            // Assert
            Assert.Equal(12, result.Bits);
            Assert.Equal(1.5, result.Bytes);
        }

        [Fact]
        public void FromKiloBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = MetricByteSize.FromKiloBytes(value);

            // Assert
            Assert.Equal(1.5e3, result.Bytes);
            Assert.Equal(1.5, result.KiloBytes);
        }

        [Fact]
        public void FromMegaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = MetricByteSize.FromMegaBytes(value);

            // Assert
            Assert.Equal(1.5e6, result.Bytes);
            Assert.Equal(1.5, result.MegaBytes);
        }

        [Fact]
        public void FromGigaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = MetricByteSize.FromGigaBytes(value);

            // Assert
            Assert.Equal(1.5e9, result.Bytes);
            Assert.Equal(1.5, result.GigaBytes);
        }

        [Fact]
        public void FromTeraBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = MetricByteSize.FromTeraBytes(value);

            // Assert
            Assert.Equal(1.5e12, result.Bytes);
            Assert.Equal(1.5, result.TeraBytes);
        }
    }
}
