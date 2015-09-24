namespace JsGoogleCompile
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Static class for helping with guards
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Assert argument not null.
        /// </summary>
        /// <param name="variableNameExpression">
        /// An lambda expression containing the variable name
        /// </param>
        /// <param name="value">
        /// The variable to check.
        /// </param>
        /// <typeparam name="T">The type of variable we are checking </typeparam>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of parameter being null</exception>
        public static void ArgumentNotNull<T>(Expression<Func<T>> variableNameExpression, T value) where T : class 
        {
            if (value == null)
            {
                var name = ((MemberExpression)variableNameExpression.Body).Member.Name;
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Assert string not null or empty.
        /// </summary>
        /// <param name="variableNameExpression">
        /// An lambda expression containing the variable name
        /// </param>
        /// <param name="value">
        /// The variable to check.
        /// </param>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of string parameter being null or empty</exception>
        public static void ArgumentNotNullOrEmpty(Expression<Func<string>> variableNameExpression, string value) 
        {
            if (string.IsNullOrEmpty(value))
            {
                var name = ((MemberExpression)variableNameExpression.Body).Member.Name;
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Assert value not null.
        /// </summary>
        /// <param name="variableNameExpression">
        /// An lambda expression containing the variable name
        /// </param>
        /// <param name="value">
        /// The variable to check.
        /// </param>
        /// <typeparam name="T">The type of variable we are checking </typeparam>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of parameter being null</exception>
        public static void ValueNotNull<T>(Expression<Func<T>> variableNameExpression, T value) where T : class
        {
            if (value == null)
            {
                var name = ((MemberExpression)variableNameExpression.Body).Member.Name;
                throw new NullReferenceException(name);
            }
        }

        /// <summary>
        /// Assert string value not null or empty. Use this for local variables or object members
        /// </summary>
        /// <param name="variableNameExpression">
        /// An lambda expression containing the variable name
        /// </param>
        /// <param name="value">
        /// The variable to check.
        /// </param>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of string parameter being null or empty</exception>
        public static void ValueNotNullOrEmpty(Expression<Func<string>> variableNameExpression, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                var name = ((MemberExpression)variableNameExpression.Body).Member.Name;
                throw new NullReferenceException(name);
            }
        }
    }
}
