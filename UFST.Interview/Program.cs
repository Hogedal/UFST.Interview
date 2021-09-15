using System;
using UFST.Interview.Utility;

namespace UFST.Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            UdskrivArbejdsdage(new DateTime(2021, 8, 25), new DateTime(2021, 8, 27)); //3 dage
            UdskrivArbejdsdage(new DateTime(2021, 8, 25), new DateTime(2021, 8, 31)); //5 dage

            Console.ReadKey();
        }

        private static void UdskrivArbejdsdage(DateTime fra, DateTime til)
        {
            Console.WriteLine(
                $"Der er {ArbejdsdageImellem(fra, til)} arbejdsdage imellem " +
                $"{fra.ToString("dd-MM-yyyy")} og {til.ToString("dd-MM-yyyy")}.");
        }

        private static int ArbejdsdageImellem(DateTime fra, DateTime til)
        {
            return HolidayHelper.BusinessDaysBetween(fra, til);
        }
    }
}
