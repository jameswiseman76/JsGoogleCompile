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
        /// <param name="variable">
        /// The variable to check.
        /// </param>
        /// <typeparam name="T">The type of variable we are checking </typeparam>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of parameter being null</exception>
        public static void ArgumentNotNull<T>(Expression<Func<T>> variable) where T : class 
        {
            var memberSelector = (MemberExpression)variable.Body;
            var constantSelector = (ConstantExpression)memberSelector.Expression;
            var value = ((FieldInfo)memberSelector.Member).GetValue(constantSelector.Value);
 
            if (value == null)
            {
                var name = ((MemberExpression)variable.Body).Member.Name;
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Assert string not null or empty.
        /// </summary>
        /// <param name="variable">
        /// The variable to test.
        /// </param>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of string parameter being null or empty</exception>
        public static void ArgumentNotNullOrEmpty(Expression<Func<string>> variable) 
        {
            var memberSelector = (MemberExpression)variable.Body;
            var constantSelector = (ConstantExpression)memberSelector.Expression;
            var value = ((FieldInfo)memberSelector.Member).GetValue(constantSelector.Value) as string;

            if (string.IsNullOrEmpty(value))
            {
                var name = ((MemberExpression)variable.Body).Member.Name;
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Assert value not null.
        /// </summary>
        /// <param name="variable">
        /// The variable to check.
        /// </param>
        /// <typeparam name="T">The type of variable we are checking </typeparam>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of parameter being null</exception>
        public static void ValueNotNull<T>(Expression<Func<T>> variable) where T : class
        {
            var memberSelector = (MemberExpression)variable.Body;
            var constantSelector = (ConstantExpression)memberSelector.Expression;
            var value = ((FieldInfo)memberSelector.Member).GetValue(constantSelector.Value);

            if (value == null)
            {
                var name = ((MemberExpression)variable.Body).Member.Name;
                throw new NullReferenceException(name);
            }
        }

        /// <summary>
        /// Assert string value not null or empty. Use this for local variables or object members
        /// </summary>
        /// <param name="variable">
        /// The variable to test.
        /// </param>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of string parameter being null or empty</exception>
        public static void ValueNotNullOrEmpty(Expression<Func<string>> variable)
        {
            var memberSelector = (MemberExpression)variable.Body;
            var constantSelector = (ConstantExpression)memberSelector.Expression;
            var value = ((FieldInfo)memberSelector.Member).GetValue(constantSelector.Value) as string;

            if (string.IsNullOrEmpty(value))
            {
                var name = ((MemberExpression)variable.Body).Member.Name;
                throw new NullReferenceException(name);
            }
        }
    }
}
