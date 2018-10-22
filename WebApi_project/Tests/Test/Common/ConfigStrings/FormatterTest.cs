using Helper.Common.ConfigStrings;
using System;
using Xunit;

namespace Test.Common.ConfigStrings
{
    public class FormatterTest
    {
        public void ShortDateFormatGivesCorrectOutput()
        {
            DateTime date = new DateTime(2019,06,27,11,23,43);
            string dateAsString = date.ToString(Formatter.ShortDateFormat);

            Assert.Equal("2019-06-27", dateAsString);
        }
    }
}
