using System;

namespace CRUD_CS.DB.TypesFunctionalExtensions
{
    public static class MySQL_FEX
    {
        //Numeric Functions
        public static void AVG(this double x) { }
        public static void AVG(this int x) { }
        public static void AVG(this float x) { }
        public static void CEIL(this double x) { }
        public static void CEIL(this int x) { }
        public static void CEIL(this float x) { }
        public static double FLOOR(this double x) => x;
        public static int FLOOR(this int x) => x;
        public static float FLOOR(this float x) => x;

        // Date Functions
        public static int DATEDIFF(this DateTime time, DateTime x) => 0;
    }

}
