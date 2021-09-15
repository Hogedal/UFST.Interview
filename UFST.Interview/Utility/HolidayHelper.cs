using System;
using System.Collections.Generic;
using System.Text;

namespace UFST.Interview.Utility
{
    public static class HolidayHelper
    {
        public static List<DateTime> GetDanishPublicHolidays(int year)
        {
            var holidays = new List<DateTime>();
            var easter = FindEasterSunday(year);
            //New years day
            holidays.Add(new DateTime(year, 1, 1));
            //skærtorsdag
            holidays.Add(easter.AddDays(-3));
            //Langfredag
            holidays.Add(easter.AddDays(-2));
            //påskedag
            holidays.Add(easter);
            //2. påskedag
            holidays.Add(easter.AddDays(1));
            //St. Bededag
            holidays.Add(easter.AddDays(26));
            //Kristi himmelfart
            holidays.Add(easter.AddDays(39));
            //pinsedag
            holidays.Add(easter.AddDays(49));
            //2. pinseag
            holidays.Add(easter.AddDays(50));
            //grundlovsdag
            holidays.Add(new DateTime(year, 6, 5));
            //x-mas eve
            holidays.Add(new DateTime(year, 12, 24));
            //x-mas day
            holidays.Add(new DateTime(year, 12, 25));
            //2nd xmas day
            holidays.Add(new DateTime(year, 12, 26));
            //new years eve
            holidays.Add(new DateTime(year, 12, 31));
            return holidays;
        }

        public static DateTime FindEasterSunday(int year)
        {
            //Please dont ask me to explain how this calculation works, its dark catholic witchcraft. Something to do with moon phases?
            int a = year % 19,
            b = year / 100,
            c = year % 100,
            d = b / 4,
            e = b % 4,
            f = (b + 8) / 25,
            g = (b - f + 1) / 3,
            h = (19 * a + b - d - g + 15) % 30,
            j = c / 4,
            k = c % 4,
            l = (32 + 2 * e + 2 * j - h - k) % 7,
            m = (a + 11 * h + 22 * l) / 451,
            n = (h + l - 7 * m + 114) / 31,
            p = (h + l - 7 * m + 114) % 31;

            return new DateTime(year, n, p + 1);
        }
        public static int BusinessDaysBetween(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException("End date must be later than start date");
            }
            var holidays = GetDanishPublicHolidays(start.Year);
            if (start.Year != end.Year)
            {
                holidays.Concat(GetDanishPublicHolidays(end.Year));
            }
            int days = 0;
            DateTime current = start.Date;
            while (current <= end)
            {
                if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(current.Date))
                {
                    ++days;
                }
                current = current.AddDays(1);
            }
            return days;
        }
    }
}
