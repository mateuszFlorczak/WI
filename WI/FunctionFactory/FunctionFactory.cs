using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunctionFactory.Controls;
using FunctionFactory.CoreFunctions;

namespace FunctionFactory
{
    public static class Factory
    {
        private static IList<IFunction> _Functions;

        public static ReadOnlyCollection<IFunction> Functions
        {
            get
            {
                if (_Functions == null)
                {
                    _Functions = new List<IFunction>();
                    //Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                    Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToArray();
                    foreach(Type type in types)
                        if (type.IsClass && (typeof(IFunction)).IsAssignableFrom(type))
                            _Functions.Add((IFunction)type.GetConstructor(Type.EmptyTypes).Invoke(null));
                }
                return new ReadOnlyCollection<IFunction>(_Functions); 
            }
        }
    }
}
