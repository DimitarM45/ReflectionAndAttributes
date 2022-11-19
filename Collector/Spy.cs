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

        public string AnalyzeAccessModifiers(string className)
        {
            Type investigatedType = Type.GetType(className);

            Object obj = Activator.CreateInstance(investigatedType);

            FieldInfo[] fields = investigatedType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            MethodInfo[] publicMethods = investigatedType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[] nonPublicMethods = investigatedType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (FieldInfo field in fields)
                stringBuilder.AppendLine($"{field.Name} must be private!");

            foreach (MethodInfo method in nonPublicMethods.Where(m => m.Name.StartsWith("get")))
                stringBuilder.AppendLine($"{method.Name} have to be public!");

            foreach (MethodInfo method in publicMethods.Where(m => m.Name.StartsWith("set")))
                stringBuilder.AppendLine($"{method.Name} have to be private!");

            return stringBuilder.ToString().Trim();
        }

        public string RevealPrivateMethods(string className)
        {
            Type investigatedClassType = Type.GetType(className);

            MethodInfo[] privateMethods = investigatedClassType.GetMethods(BindingFlags.Instance 
                | BindingFlags.Static 
                | BindingFlags.NonPublic);

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"All Private Methods of Class: {className}");
            stringBuilder.AppendLine($"Base Class: {investigatedClassType.BaseType.Name}");

            foreach (MethodInfo method in privateMethods)
                stringBuilder.AppendLine(method.Name);

            return stringBuilder.ToString().Trim();
        }

        public string CollectGettersAndSetters(string className)
        {
            Type investigatedClassType = Type.GetType(className);

            MethodInfo[] methods = investigatedClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (MethodInfo method in methods.Where(m => m.Name.StartsWith("get")))
                stringBuilder.AppendLine($"{method.Name} will return {method.ReturnType}");

            foreach (MethodInfo method in methods.Where(m => m.Name.StartsWith("set")))
                stringBuilder.AppendLine($"{method.Name} will set {method.GetParameters().First().ParameterType}");

            return stringBuilder.ToString().Trim();
        }
    }
}
