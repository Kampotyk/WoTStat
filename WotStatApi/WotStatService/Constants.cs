using System.Collections.Generic;

namespace WotStat
{
    public static class Constants
    {
        public enum Badge
        {
            None = 0,
            Third = 1,
            Second = 2,
            First = 3,
            Master = 4
        }

        public const double DesiredWinPercent = 50.0;
        public const double MaxWinPercent = 100.0;
        public const double GraphStep = 5.0;

        public static Dictionary<Badge, string> BadgeIcons = new Dictionary<Badge, string>
        {
            { Badge.None, @"Resourses\empty.png" },
            { Badge.Third, @"Resourses\third.png" },
            { Badge.Second, @"Resourses\second.png" },
            { Badge.First, @"Resourses\first.png" },
            { Badge.Master, @"Resourses\master.png" }
        };

        public static Dictionary<int, string> GunMarksIcons = new Dictionary<int, string>
        {
            { 0, @"Resourses\empty.png" },
            { 1, @"Resourses\one_star.png" },
            { 2, @"Resourses\two_stars.png" },
            { 3, @"Resourses\three_stars.png" }
        };
    }
}
