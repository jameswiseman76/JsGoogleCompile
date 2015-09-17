namespace JsGoogleCompile
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Static class for helping with guards
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Assert not null.
        /// </summary>
        /// <param name="variable">
        /// The selector.
        /// </param>
        /// <typeparam name="T">The type of variable we are checking </typeparam>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of parameter being null</exception>
        public static void NotNull<T>(Expression<Func<T>> variable) where T : class 
        {
            var value = variable.Compile().Invoke();
            if (value == null)
            {
                string name = ((MemberExpression)variable.Body).Member.Name;
                throw new ArgumentNullException(name);
            }
        }
    }
}
