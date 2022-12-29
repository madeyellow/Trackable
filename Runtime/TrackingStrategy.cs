namespace MadeYellow.Trackables
{
    /// <summary>
    /// Determines how exactly <see cref="Trackable{T}"/> should handle change tracking
    /// </summary>
    public enum TrackingStrategy
    {
        /// <summary>
        /// When <see cref="Trackable{T}.value"/> is updated <see cref="Trackable{T}.Commit"/> will be called automatically
        /// </summary>
        Auto,

        /// <summary>
        /// <see cref="Trackable{T}.Commit"/> must be called manually in order to perform change tracking
        /// </summary>
        Manual
    }
}