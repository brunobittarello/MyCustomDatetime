using System;

namespace MyCustomDatetime
{
    class DateCustom
    {
        public int day;
        public int month;
        public int year;
        public int hour;
        public int minute;
        public override string ToString()
        {
            return string.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}", day, month, year, hour, minute);
        }

        public int MaxDayOfTheMonth(int month)
        {
            if (month == 2)
                return 28;

            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                return 31;
            return 30;
        }

        public void AddMinutes(double minutes)
        {
            var daysToAdd = (int)(minutes / 1440);
            var minutesToAdd = (int)(minutes % 1440);

            minute += minutesToAdd;
            hour += (int)(minute / 60);
            if (minute > 60)
            {
                minute = (int)(minute % 60);
            }
            else if (minute < 0)
            {
                hour--;
                minute = 60 - (Math.Abs(minute) % 60);
            }

            if (hour > 24)
            {
                daysToAdd++;
                hour = (int)(hour % 24);
            }
            else if (hour < 0)
            {
                daysToAdd--;
                hour = 24 - (Math.Abs(hour) % 24);
            }
            AddDays(daysToAdd);
        }

        public void AddDays(double days)
        {
            year += (int)(days / 355);
            days = (int)(days % 355);

            var maxDay = MaxDayOfTheMonth(month);
            if (Math.Sign(days) == -1)
                maxDay = MaxDayOfTheMonth((month - 1) == 0 ? 12 : month - 1);
            while (Math.Abs(days) > maxDay)
            {
                AddMonths(Math.Sign(days));
                if (Math.Sign(days) == -1)
                {
                    days += maxDay;
                    maxDay = MaxDayOfTheMonth((month - 1) == 0 ? 12 : month - 1);
                }
                else
                {
                    days -= maxDay;
                    maxDay = MaxDayOfTheMonth(month);
                }
            }

            day += (int)days;
            if (day < 1)
            {
                AddMonths(-1);
                maxDay = MaxDayOfTheMonth(month);
                day = maxDay - Math.Abs(day);
            }
            else if (day > maxDay)
            {
                AddMonths(1);
                maxDay = MaxDayOfTheMonth(month);
                day = day - maxDay;
            }
        }

        public void AddMonths(int months)
        {
            month += months;
            if (month < 1)
            {
                year--;
                month = 12 - month;
            }
            else if (month > 12)
            {
                year++;
                month -= 12;
            }
        }
    }
}
