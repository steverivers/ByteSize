using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ByteSize.Tests
{
    public class ToStringMethod
    {
        [Fact]
        public void ReturnsLargestMetricSuffix()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10.5);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal("10.5 KiB", result);
        }

        [Fact]
        public void ReturnsDefaultNumberFormat()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10.5);

            // Act
            var result = b.ToString("KiB");

            // Assert
            Assert.Equal("10.5 KiB", result);
        }

        [Fact]
        public void ReturnsProvidedNumberFormat()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10.1234);

            // Act
            var result = b.ToString("#.#### KiB");

            // Assert
            Assert.Equal("10.1234 KiB", result);
        }

        [Fact]
        public void ReturnsBits()
        {
            // Arrange
            var b = BinaryByteSize.FromBits(10);

            // Act
            var result = b.ToString("##.#### b");

            // Assert
            Assert.Equal("10 b", result);
        }

        [Fact]
        public void ReturnsBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromBytes(10);

            // Act
            var result = b.ToString("##.#### B");

            // Assert
            Assert.Equal("10 B", result);
        }

        [Fact]
        public void ReturnsKiloBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10);

            // Act
            var result = b.ToString("##.#### KiB");

            // Assert
            Assert.Equal("10 KiB", result);
        }

        [Fact]
        public void ReturnsMegaBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromMebiBytes(10);

            // Act
            var result = b.ToString("##.#### MiB");

            // Assert
            Assert.Equal("10 MiB", result);
        }

        [Fact]
        public void ReturnsGigaBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromGibiBytes(10);

            // Act
            var result = b.ToString("##.#### GiB");

            // Assert
            Assert.Equal("10 GiB", result);
        }

        [Fact]
        public void ReturnsTeraBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromTebiBytes(10);

            // Act
            var result = b.ToString("##.#### TiB");

            // Assert
            Assert.Equal("10 TiB", result);
        }

        [Fact]
        public void ReturnsSelectedFormat()
        {
            // Arrange
            var b = BinaryByteSize.FromTebiBytes(10);

            // Act
            var result = b.ToString("0.0 TiB");

            // Assert
            Assert.Equal("10.0 TiB", result);
        }

        [Fact]
        public void ReturnsLargestMetricPrefixLargerThanZero()
        {
            // Arrange
            var b = BinaryByteSize.FromMebiBytes(.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("512 KiB", result);
        }

        [Fact]
        public void ReturnsLargestMetricPrefixLargerThanZeroForNegativeValues()
        {
            // Arrange
            var b = BinaryByteSize.FromMebiBytes(-.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("-512 KiB", result);
        }
    }
}
