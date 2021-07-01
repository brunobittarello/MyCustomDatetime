using System;

namespace MyCustomDatetime
{
    class DateHelper
    {
        public string error { get; private set; }

        public string ChangeDate(string date, char op, long value)
        {
            error = "";
            value = Math.Abs(value);

            if (VerifyOperation(op) == false)
            {
                error = "Invalid operator.";
                return date;
            }

            var dateC = new DateCustom();
            if (TryParseToDate(out dateC, date) == false)
            {
                error = "Couldn't convert into a date.";
                return date;
            }

            if (ValidateDate(dateC) == false)
            {
                error = "Invalid date.";
                return date;
            }

            value = (op == '+') ? value : -value;
            dateC.AddMinutes(value);

            return dateC.ToString();
        }
        
        bool VerifyOperation(char op)
        {
            if (op != '+' && op != '-')
                return false;
            return true;
        }

        bool TryParseToDate(out DateCustom date, string dateString)
        {
            //It could be a regular expression
            date = new DateCustom();

            if (dateString.Length != 16)
                return false;

            var subStr = dateString.Substring(0, 2);
            if (int.TryParse(subStr, out date.day) == false)
                return false;

            subStr = dateString.Substring(2, 1);
            if (subStr != "/")
                return false;

            subStr = dateString.Substring(3, 2);
            if (int.TryParse(subStr, out date.month) == false)
                return false;

            subStr = dateString.Substring(5, 1);
            if (subStr != "/")
                return false;

            subStr = dateString.Substring(6, 4);
            if (int.TryParse(subStr, out date.year) == false)
                return false;

            subStr = dateString.Substring(10, 1);
            if (subStr != " ")
                return false;

            subStr = dateString.Substring(11, 2);
            if (int.TryParse(subStr, out date.hour) == false)
                return false;

            subStr = dateString.Substring(13, 1);
            if (subStr != ":")
                return false;

            subStr = dateString.Substring(14, 2);
            if (int.TryParse(subStr, out date.minute) == false)
                return false;

            return true;
        }

        bool ValidateDate(DateCustom date)
        {
            if (date.month < 1 || date.month > 12)
                return false;

            var maxDay = date.MaxDayOfTheMonth(date.month);
            if (date.day < 1 || date.day > maxDay)
                return false;

            if (date.hour < 0 || date.hour > 23)
                return false;

            if (date.minute < 0 || date.minute > 60)
                return false;

            return true;
        }
    }
}
