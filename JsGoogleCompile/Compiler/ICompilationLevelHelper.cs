namespace JsGoogleCompile
{
    /// <summary>
    /// The CompilationLevelHelper interface.
    /// </summary>
    public interface ICompilationLevelHelper
    {
        /// <summary>
        /// The from.
        /// </summary>
        /// <param name="shortCode">
        /// The short code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string From(string shortCode);

        /// <summary>
        /// Denotes if the given short code option is valid.
        /// </summary>
        /// <param name="shortCode">
        /// The short code representing the given option
        /// </param>
        /// <returns>
        /// true or false indicating validity
        /// </returns>
        bool IsValid(string shortCode);
    }
}
