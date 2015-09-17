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
        /// Assert not null.
        /// </summary>
        /// <param name="variable">
        /// The selector.
        /// </param>
        /// <typeparam name="T">The type of variable we are checking </typeparam>
        /// <exception cref="ArgumentNullException">Exception thrown in the event of parameter being null</exception>
        public static void NotNull<T>(Expression<Func<T>> variable) where T : class 
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
    }
}
