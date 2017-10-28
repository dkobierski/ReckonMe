namespace ReckonMe.Constants
{
    public static class RowSizes
    {
        public static readonly double LargeRowHeightDouble = 60;
        public static readonly double MediumRowHeightDouble = 44;
        public static readonly double SmallRowHeightDouble = 30;

        public static int LargeRowHeightInt => (int)LargeRowHeightDouble;
        public static int MediumRowHeightInt => (int)MediumRowHeightDouble;
        public static int SmallRowHeightInt => (int)SmallRowHeightDouble;
    }
}