using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using System.Runtime.Serialization;

namespace OXM
{
    internal static class MapBuilderUtility
    {
        internal static void QualifyName(ref XName name, XNamespace ns)
        {
            if (name.Namespace == XNamespace.None)
            {
                name = ns + name.LocalName;
            }
        }

        internal static void GetTypeAndProperty(LambdaExpression exp, out string typeName, out string propertyName)
        {
            typeName = exp.Parameters[0].Type.ToString();
            propertyName = ((MemberExpression)exp.Body).Member.Name;
        }
    }
}
