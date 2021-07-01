using System;

namespace MyCustomDatetime
{
    class Program
    {
        public const bool DEBUG = true;
        public const bool SHOW_ERRORS = false;

        static void Main(string[] args)
        {
            var helper = new DateHelper();
            ShowChangeDate(helper.ChangeDate("01/03/2010 23:00", '+', 4000), helper.error);
            Debug(2010, 3, 1, 23, 0, 4000);
            ShowChangeDate(helper.ChangeDate("01/03/2010 23:00", '-', 4000), helper.error);
            Debug(2010, 3, 1, 23, 0, -4000);

            ShowChangeDate(helper.ChangeDate("01/03/2010 23:00", '+', 40000), helper.error);
            Debug(2010, 3, 1, 23, 0, 40000);
            ShowChangeDate(helper.ChangeDate("01/03/2010 23:00", '-', 40000), helper.error);
            Debug(2010, 3, 1, 23, 0, -40000);

            ShowChangeDate(helper.ChangeDate("01/03/2010 23:00", '+', 400000), helper.error);
            Debug(2010, 3, 1, 23, 0, 400000);
            ShowChangeDate(helper.ChangeDate("01/03/2010 23:00", '-', 400000), helper.error);
            Debug(2010, 3, 1, 23, 0, -400000);

            ShowChangeDate(helper.ChangeDate("24/10/2016 18:46", '+', 123456), helper.error);
            Debug(2016, 10, 24, 18, 46, 123456);
            ShowChangeDate(helper.ChangeDate("24/10/2016 18:46", '-', 123456), helper.error);
            Debug(2016, 10, 24, 18, 46, -123456);

            //Failure tests
            ShowChangeDate(helper.ChangeDate("bruno", '-', 4000), helper.error);
            ShowChangeDate(helper.ChangeDate("4000", '+', 4000), helper.error);
            ShowChangeDate(helper.ChangeDate("99/03/2010 23:00", '+', 4000), helper.error);
            ShowChangeDate(helper.ChangeDate("01/99/2010 23:00", '+', 4000), helper.error);
            ShowChangeDate(helper.ChangeDate("01/03/2010 99:00", '+', 4000), helper.error);
            ShowChangeDate(helper.ChangeDate("01/03/2010 23:99", '+', 4000), helper.error);
            ShowChangeDate(helper.ChangeDate("01/03/2010 23:00", '?', 4000), helper.error);


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        
        //The visual output of the results
        static void ShowChangeDate(string result, string error)
        {
            Console.WriteLine(result);
            if (SHOW_ERRORS && string.IsNullOrEmpty(error) == false)
                Console.WriteLine("\t" + error);
            Console.WriteLine("");
        }

        //To verify if the operation is right
        static void Debug(int y, int m, int d, int h, int min, double diff)
        {
            if (DEBUG == false)
                return;
            ////commented to avoid the rule that doesn't allow DateTime(it is only used here and only for debug)
            //var dateValue = new DateTime(y, m, d, h, min, 0);
            //dateValue = dateValue.AddMinutes(diff);
            //Console.WriteLine("\tDebug result: " + dateValue);
        }
   } 
}
