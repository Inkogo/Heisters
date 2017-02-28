using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Heisters
{
    class InjectionContainer
    {
        public Dictionary<Type, object> objs;

        static InjectionContainer _instance;
        public static InjectionContainer Instance
        {
            get
            {
                if (_instance == null) _instance = new InjectionContainer();
                return _instance;
            }
        }

        public InjectionContainer()
        {
            objs = new Dictionary<Type, object>();
        }

        public void RegisterObject<T>(T t)
        {
            if (!objs.ContainsKey(typeof(T)))
                objs.Add(typeof(T), t);
        }

        public void InjectDependencies<T>(T t)
        {
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo i in fields.Where(x => x.IsDefined(typeof(InjectAttribute), false)))
            {
                if (objs.ContainsKey(i.FieldType))
                    i.SetValue(t, objs[i.FieldType]);
            }
        }
    }
}
