namespace JsGoogleCompile
{
    using System.Collections.Generic;

    /// <summary>
    /// The compilation level.
    /// </summary>
    public static class CompilationLevel
    {
        /// <summary>
        /// The simple full.
        /// </summary>
        public const string SimpleFull = "SIMPLE_OPTIMIZATIONS";

        /// <summary>
        /// The white space.
        /// </summary>
        public const string WhiteSpace = "WHITESPACE_ONLY";

        /// <summary>
        /// The advanced.
        /// </summary>
        public const string Advanced = "ADVANCED_OPTIMIZATIONS";

        /// <summary>
        /// The compilation level mapping.
        /// </summary>
        private static readonly Dictionary<string, string> Mapping = new Dictionary<string, string>
        {
            { "S", SimpleFull }, 
            { "W", WhiteSpace }, 
            { "A", Advanced }, 
        };

        /// <summary>
        /// The from.
        /// </summary>
        /// <param name="shortCode">
        /// The short code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string From(string shortCode)
        {
            return Mapping.ContainsKey(shortCode) ? Mapping[shortCode.ToUpper()] : Advanced;
        }
    }
}
