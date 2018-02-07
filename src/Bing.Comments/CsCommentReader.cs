using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Comments
{
    /// <summary>
    /// 注释读取器，用于读取C#程序中的注释
    /// </summary>
    public static class CsCommentReader
    {
        /// <summary> 
        /// 读取任意成员的注释信息
        /// </summary>
        /// <param name="member">需要读取注释的成员</param>
        public static CsComments Create(MemberInfo member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("member");
            }
            switch (member.MemberType)
            {
                case MemberTypes.Constructor:
                    return Create((ConstructorInfo)member);
                case MemberTypes.Event:
                    return Create((EventInfo)member);
                case MemberTypes.Field:
                    return Create((FieldInfo)member);
                case MemberTypes.Method:
                    return Create((MethodInfo)member);
                case MemberTypes.Property:
                    return Create((PropertyInfo)member);
                case MemberTypes.TypeInfo:
                    return Create((Type)member);
                case MemberTypes.NestedType:
                    if (member is Type)
                    {
                        return Create((Type)member);
                    }
                    if (member is Type)
                    {
                        return Create((Type)member);
                    }

                    break;
                case MemberTypes.Custom:
                case MemberTypes.All:
                default:
                    break;
            }
            throw new NotSupportedException("不支持当前类型的对象");
        }
        /// <summary> 
        /// 读取任类注释信息
        /// </summary>
        /// <param name="type">需要读取注释的类</param>
        public static CsComments Create(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            var xpath = string.Format("T:{0}", type.FullName.Replace("+", "."));
            return CreateComment(type, xpath);
        }

        /// <summary> 
        /// 读取构造函数的注释信息
        /// </summary>
        /// <param name="ctor">需要读取注释的构造函数</param>
        public static CsComments Create(ConstructorInfo ctor)
        {
            if (ctor == null)
            {
                throw new ArgumentNullException("ctor");
            }
            var xpath = GetXPath("M", ctor.DeclaringType, ctor.IsStatic ? "#cctor" : "#ctor");
            xpath += GetXPathByParameters(ctor.GetParameters());
            return CreateComment(ctor, xpath);
        }

        /// <summary> 
        /// 读取事件的注释信息
        /// </summary>
        /// <param name="event">需要读取注释的事件</param>
        public static CsComments Create(EventInfo @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException("field");
            }
            var xpath = GetXPath("E", @event.DeclaringType, @event.Name);
            return CreateComment(@event, xpath);
        }

        /// <summary> 
        /// 读取方法的注释信息
        /// </summary>
        /// <param name="method">需要读取注释的方法</param>
        public static CsComments Create(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            var xpath = GetXPath("M", method.DeclaringType, method.Name);
            if (method.IsGenericMethod)
            {
                xpath += "``" + method.GetGenericArguments().Length;
                if (method.IsGenericMethodDefinition == false)
                {
                    method = method.GetGenericMethodDefinition();
                }
            }
            xpath += GetXPathByParameters(method.GetParameters());
            return CreateComment(method, xpath);
        }

        /// <summary> 
        /// 读取属性的注释信息
        /// </summary>
        /// <param name="prop">需要读取注释的属性</param>
        public static CsComments Create(PropertyInfo prop)
        {
            if (prop == null)
            {
                throw new ArgumentNullException("prop");
            }
            var xpath = GetXPath("P", prop.DeclaringType, prop.Name);
            xpath += GetXPathByParameters(prop.GetIndexParameters());
            return CreateComment(prop, xpath);
        }

        /// <summary> 
        /// 读取字段的注释信息
        /// </summary>
        /// <param name="field">需要读取注释的字段</param>
        public static CsComments Create(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            var xpath = GetXPath("F", field.DeclaringType, field.Name);
            return CreateComment(field, xpath);
        }

        /// <summary> 
        /// 读取枚举的值和注释
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        public static Dictionary<Enum, string> CreateEnumComment(Type enumType)
        {
            return CreateEnumComment<Enum>(enumType);
        }

        /// <summary>
        /// 读取枚举的值和注释
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <returns></returns>
        public static Dictionary<T, string> CreateEnumComment<T>()
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return CreateEnumComment<T>(typeof(T));
        }

        /// <summary> 
        /// 读取枚举的值和注释
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <param name="enumType">枚举的类型</param>
        private static Dictionary<T, string> CreateEnumComment<T>(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }
            else if (enumType.IsEnum == false)
            {
                throw new ArgumentOutOfRangeException(nameof(enumType), "enumType 不是 System.Enum");
            }
            var maps = new Dictionary<T, string>();
            var array = Enum.GetValues(enumType);
            foreach (T item in array)
            {
                var field = enumType.GetField(item.ToString());
                var comment = Create(field);
                if (comment != null && comment.Summary != null)
                {
                    maps.Add(item, comment.Summary);
                }
                else
                {
                    maps.Add(item, GetDescription(field));
                }
            }
            return maps;
        }

        /// <summary>
        /// 获取成员的DescriptionAttribute中的描述，如果没有这个特性返回Name
        /// </summary>
        /// <param name="member">需要获取描述的成员</param>
        /// <returns></returns>
        private static string GetDescription(MemberInfo member)
        {
            var attr = (DescriptionAttribute) Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute));
            if (attr == null)
            {
                return member.Name;
            }

            return attr.Description ?? member.Name;
        }

        /// <summary>
        /// 成员信息列表
        /// </summary>
        [ThreadStatic]
        private static List<MemberInfo> _member;

        /// <summary>
        /// 创建注释对象，读取任意成员的注释信息，如果xml不存在或者注释不存在返回null
        /// </summary>
        /// <param name="member">成员对象</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private static CsComments CreateComment(MemberInfo member, string path)
        {
            if (_member == null)
            {
                _member=new List<MemberInfo>();
            }
            else if (_member.Contains(member))
            {
                return null;
            }
            _member.Add(member);

            var xml = CsCommentsXmlCache.Get(member);
            if (xml == null)
            {
                return null;
            }

            var node = xml.SelectNodes("/doc/members/member[@name='" + path.Replace('+', '.') + "']");
            if (node.Count == 0)
            {
                return null;
            }

            _member.RemoveAt(_member.Count-1);
            return new CsComments(node[0]);
        }

        /// <summary>
        /// 如果存在参数，则返回与参数相关的Xml路径字符串
        /// </summary>
        /// <param name="args">参数数组</param>
        /// <returns></returns>
        private static string GetXPathByParameters(ParameterInfo[] args)
        {
            if (args == null || args.Length == 0)
            {
                return null;
            }
            var list=new List<string>(args.Length);
            foreach (var item in args)
            {
                var type = item.ParameterType;
                list.Add(GetParameterName(type));
            }

            return "(" + string.Join(",", list.ToArray()) + ")";
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="types">类型数组</param>
        /// <returns></returns>
        private static string GetParameterName(Type[] types)
        {
            var list=new List<string>(types.Length);
            foreach (var type in types)
            {
                list.Add(GetParameterName(type));
            }

            return "{" + string.Join(",", list.ToArray()) + "}";
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static string GetParameterName(Type type)
        {
            if (type.IsGenericType)
            {
                if (type.IsGenericParameter)
                {
                    return "``" + type.GenericParameterPosition;
                }
                else if(type.IsGenericTypeDefinition)
                {
                    
                }
                else
                {
                    return type.FullName.Substring(0, type.FullName.IndexOf('`')) +
                           GetParameterName(type.GetGenericArguments());
                }
            }

            return type.FullName;
        }

        /// <summary>
        /// 返回与类型相关的Xml路径字符串
        /// </summary>
        /// <param name="prefix">固定前缀</param>
        /// <param name="type">类型</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        private static string GetXPath(string prefix, Type type, string name)
        {
            if (type.IsGenericType && type.IsGenericTypeDefinition == false)
            {
                type = type.GetGenericTypeDefinition();
            }

            return string.Format("{0}:{1}.{2}", prefix, type.FullName.Replace("+", "."), name);
        }
    }
}
