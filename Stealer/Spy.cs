using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string investigatedClass, params string[] fieldNames)
        {
            Type investigatedClassType = Type.GetType(investigatedClass);

            FieldInfo[] fields = investigatedClassType
                .GetFields((BindingFlags)60)
                .Where(f => fieldNames
                .Contains(f.Name))
                .ToArray();

            StringBuilder stringBuilder = new StringBuilder();

            Object obj = Activator.CreateInstance(investigatedClassType);

            Console.WriteLine($"Class under investigation: {investigatedClass}");

            foreach (FieldInfo field in fields)
                stringBuilder.AppendLine($"{field.Name} = {field.GetValue(obj)}");

            return stringBuilder.ToString().Trim();
        }
    }
}
