using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            Type startUpType = typeof(StartUp);

            MethodInfo[] methods = startUpType.GetMethods(BindingFlags.Instance 
                | BindingFlags.Public 
                | BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                if (method.CustomAttributes.Any(a => a.AttributeType == typeof(AuthorAttribute)))
                {
                    Object[] attributes = method.GetCustomAttributes(false);

                    foreach (AuthorAttribute attribute in attributes)
                        Console.WriteLine($"{method.Name} is written by {attribute.Name}");
                }
            }
        }
    }
}
