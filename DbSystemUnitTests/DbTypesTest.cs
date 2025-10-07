using DbSystemLibrary;

namespace DbSystemUnitTests
{
    public class DbTypesTest
    {
        [Theory]
        [InlineData("08:00-09:30", true)]
        [InlineData("8:00 - 17:00", true)]
        [InlineData("09:30\t12:00", true)]

        [InlineData("18:00-07:59", false)]
        [InlineData("aa:bb-11:00", false)]
        [InlineData("10:00", false)]
        public void Column_IsValid_TimeInvl_Works(string input, bool expected)
        {
            var col = new Column("Shift", DbTypeEnum.TimeInvl);

            var actual = col.IsValid(input);

            Assert.Equal(expected, actual);
        }
    }
}