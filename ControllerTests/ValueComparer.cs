using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ControllerTests
{
    public class ValueComparer<T> : IEqualityComparer<T>
    {
        public string[] ExcludeProperties { get; set; }

        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (object.Equals(x, default(T)) || object.Equals(y, default(T)) || (x.GetType() != y.GetType()))
            {
                return false;
            }

            var properties = x.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            if (!properties.Any())
            {
                throw new InvalidOperationException("Objects don't have properties to compare");
            }

            if (ExcludeProperties?.Any() == true)
            {
                properties = properties.Where(p => !ExcludeProperties.Contains(p.Name)).ToList();
            }

            foreach (var property in properties)
            {
                var valueX = property.GetValue(x);
                var valueY = property.GetValue(y);

                if (!Equals(valueX, valueY))
                {
                    //TestContext.Progress.Write($"Check for {property.Name} is failed: {valueX} is not equal to {valueY}");
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            var properties = obj.GetType().GetProperties();
            unchecked
            {
                return properties.Aggregate(17, (current, property) => current * property.GetValue(obj).GetHashCode() * 31);
            }
        }
    }
}
