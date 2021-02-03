using NUnit.Framework;
using System;

namespace Instagram_Assistant_Tests
{
    [TestFixture]
    public class Tests
    {        
        [Test]
        public void DateToEnd()
        {
            DateTime date = new DateTime();
            TimeSpan ts = new TimeSpan(22, 00, 00);
            date = date.Date + ts;
            string dateString = date.Hour + ":" + date.Minute + ":" + date.Second;
            Assert.AreEqual("22:0:0", dateString);
        }

        [Test]
        public void DateAdding()
        {
           // DateTime date = new DateTime();
           // TimeSpan ts = new TimeSpan(22, 00, 00);
            //date = date + ts;
            var date2 = DateTime.Now.Date.Add(new TimeSpan(22, 00, 0));

            DateTime date1 = new DateTime(2020, 10, 21, 22, 00, 00);

            Assert.AreEqual(date2, date1);
        }

        [Test]
        public void DateToEndBool()
        {
            DateTime date = new DateTime();
            TimeSpan ts = new TimeSpan(22, 00, 00);
            date = date.Date + ts;

            bool dateBool = false;
            if (DateTime.Now.Hour <= date.Hour)
                dateBool = true;

            Assert.IsTrue(dateBool);
        }

        [Test]
        public void ClassName()
        {
            string classn = this.GetType().Name;

            Assert.AreEqual("Tests", classn);
        }

        [Test]
        public void NumberParseToK()
        {
            double number = 2143;
            double result = 0;
            string ret = "";
            if(number > 1000)
            {
                int length = number.ToString().Length;
                if (length == 4)
                    result = number / 1000;
                else if (length == 5)
                    result = number / 10000;
                else if(length == 6)
                    result = number / 100000;
            }
            ret = result.ToString("0.0").Replace(',', '.') +"k" ;
            Assert.AreEqual("2.1k", ret);
        }

        [Test]
        public void DateMinus()
        {
            DateTime date = new DateTime(2020, 10, 23, 9, 00, 00);
            var a = date - DateTime.Now;

            Assert.AreEqual("00:00:00", a);
        }


    }
}