namespace ReckonMe.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public static class RowSizes
    {
        /// <summary>
        /// The large row height double
        /// </summary>
        public static readonly double LargeRowHeightDouble = 60;
        /// <summary>
        /// The medium row height double
        /// </summary>
        public static readonly double MediumRowHeightDouble = 44;
        /// <summary>
        /// The small row height double
        /// </summary>
        public static readonly double SmallRowHeightDouble = 30;

        /// <summary>
        /// Gets the large row height int.
        /// </summary>
        /// <value>
        /// The large row height int.
        /// </value>
        public static int LargeRowHeightInt => (int)LargeRowHeightDouble;
        /// <summary>
        /// Gets the medium row height int.
        /// </summary>
        /// <value>
        /// The medium row height int.
        /// </value>
        public static int MediumRowHeightInt => (int)MediumRowHeightDouble;
        /// <summary>
        /// Gets the small row height int.
        /// </summary>
        /// <value>
        /// The small row height int.
        /// </value>
        public static int SmallRowHeightInt => (int)SmallRowHeightDouble;
    }
}