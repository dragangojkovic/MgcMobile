namespace MBoxMobile.Interfaces
{
    public interface IDisplay
    {
        /// <summary>
        /// Gets the screen height
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the screen width
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the screen height in pixels
        /// </summary>
        int HeightPx { get; }

        /// <summary>
        /// Gets the screen width in pixels
        /// </summary>
        int WidthPx { get; }

        /// <summary>
        /// Gets the screen density
        /// </summary>
        float Density { get; }

        /// <summary>
        /// Gets the screens X pixel density per inch
        /// </summary>
        double Xdpi { get; }

        /// <summary>
        /// Gets the screens Y pixel density per inch
        /// </summary>
        double Ydpi { get; }

        Orientation Orientation { get; }
    }

    public enum Orientation
    {
        Portrait = 1,
        Landscape = 2
    }
}
