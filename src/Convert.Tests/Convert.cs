using Xunit;


namespace ByteSize.Tests
{
    public class Convert
    {
        [Fact]
        public void FromMetricBytesToBinaryBytes()
        {
            // Arrange
            double kibiBytes = 2;
            double kilobytes = 2.048;

            //Act
            var binaryBytes = new BinaryByteSize().AddKibiBytes(kibiBytes);
            var metricBytes = new MetricByteSize().AddKiloBytes(kilobytes);

            //Assert
            Assert.Equal(binaryBytes.Bytes,metricBytes.Bytes);
            
        }


    }
}
