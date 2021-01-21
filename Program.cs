using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                var db = new SeedingContext();

                Location one = new Location();
                //one.LocationId = 1;
                one.Name = "Front Door";
                db.AddLocation(one);
                Location two = new Location();
                //two.LocationId = 2;
                two.Name = "Back Door";
                db.AddLocation(two);
                Location three = new Location();
                //three.LocationId = 3;
                three.Name = "Family Room";
                db.AddLocation(three);
                Location[] locations = {one, two, three};




                int count = 0;
                DateTime beginning = DateTime.Today.AddMonths(-6);
                DateTime currentDay = beginning;
                do{
                    for(int i = rnd.Next(6); i >= 0; i--){
                        count++;
                        DateTime newDate = getRandomTime(currentDay);
                        Location location = locations[rnd.Next(3)];
                        Event newEvent = new Event();
                        newEvent.TimeStamp = newDate;
                        newEvent.LocationId = location.LocationId;
                        Console.WriteLine(newDate + " - " + location.Name);

                        db.AddEvent(newEvent);
                    }
                    currentDay = currentDay.AddDays(1);
                }while(currentDay != DateTime.Today);
                

                

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
        static readonly Random rnd = new Random();
        static DateTime getRandomTime(DateTime start){
            long timeLong = LongRandom(start.Ticks, (start.Ticks +863990000000) , rnd);
            DateTime time = new DateTime(timeLong);
            return time;

        }
        static long LongRandom(long min, long max, Random rand) { //Yeah, I totally found this long generator online. Hope you don't mind
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}