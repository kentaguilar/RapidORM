using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RapidORM.Helpers
{
    public class PropertyHelper
    {
        /// <summary>
        /// Retrieves class property name
        /// </summary>
        /// <param name="propertyLambda"></param>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var body = propertyLambda.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return body.Member.Name;
        }

        public static void GetProperties<T>(Expression<Action<T>> expression)
        {
            var body = expression.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }
            else
            {
                foreach (var argument in body.Arguments)
                {
                    var constant = argument as ConstantExpression;
                    if (constant != null)
                    {
                        Console.WriteLine(constant.Value);
                    }
                }
            }
        }
    }
}
