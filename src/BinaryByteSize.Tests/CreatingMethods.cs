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
            double binaryByteSize = 1099511627776;

            // Act
            var result = new BinaryByteSize(binaryByteSize);

            // Assert
            Assert.Equal(8.796093022208e12, result.Bits);
            Assert.Equal(1099511627776, result.Bytes);
            Assert.Equal(1073741824, result.KibiBytes);
            Assert.Equal(1048576, result.MebiBytes);
            Assert.Equal(1024, result.GibiBytes);
            Assert.Equal(1, result.TebiBytes);
        }

        [Fact]
        public void FromBitsMethod()
        {
            // Arrange
            long value = 8;

            // Act
            var result = BinaryByteSize.FromBits(value);

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
            var result = BinaryByteSize.FromBytes(value);

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
            var result = BinaryByteSize.FromKibiBytes(value);

            // Assert
            Assert.Equal(1536, result.Bytes);
            Assert.Equal(1.5, result.KibiBytes);
        }

        [Fact]
        public void FromMegaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = BinaryByteSize.FromMebiBytes(value);

            // Assert
            Assert.Equal(1572864, result.Bytes);
            Assert.Equal(1.5, result.MebiBytes);
        }

        [Fact]
        public void FromGigaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = BinaryByteSize.FromGibiBytes(value);

            // Assert
            Assert.Equal(1610612736, result.Bytes);
            Assert.Equal(1.5, result.GibiBytes);
        }

        [Fact]
        public void FromTeraBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = BinaryByteSize.FromTebiBytes(value);

            // Assert
            Assert.Equal(1649267441664, result.Bytes);
            Assert.Equal(1.5, result.TebiBytes);
        }
    }
}
