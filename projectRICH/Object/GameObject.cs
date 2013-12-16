using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object
{
    class GameObject : ISerializable
    {
        private Module.ModuleCollection attributes = new Module.ModuleCollection();
        static private Dictionary<string, Tuple<Type, System.Reflection.MethodInfo>> commands = new Dictionary<string, Tuple<Type, System.Reflection.MethodInfo>>();
        static GameObject()
        {
            var assem = System.Reflection.Assembly.GetExecutingAssembly();
            foreach (var t in assem.GetTypes())
            {
                if (t.GetInterfaces().Any((c) => { return c.Equals(typeof(Module.IModule)); }))
                {
                    foreach (var m in t.GetMethods())
                    {
                        foreach(var a in m.GetCustomAttributes(typeof(Module.ModuleCommand), false))
                        {
                            var cname = (a as Module.ModuleCommand).Name;
                            
                            if (commands.ContainsKey(cname))
                            {
                                throw new ArgumentException("커맨드 이름이 중복되었습니다. : " + cname);
                            }
                            commands[cname] = Tuple.Create(t, m);
                        }
                    }
                }
            }
        }

        public int CurrentContext()
        {
            return 0;
        }

        private T Query<T>() where T : class, Module.IModule
        {
            T attr = attributes.Find<T>();
            if (attr == null)
            {
                throw new ArgumentException("이 GameObject는 해당 속성을 가지고 있지 않습니다.");
            }

            return attr;
        }

        private void InvokeCommand(string command, object[] param)
        {
            if (commands.ContainsKey(command))
            {
                var attr = attributes.Find(commands[command].Item1);
                commands[command].Item2.Invoke(attr, param);
            }
        }

        public void Execute(string command)
        {
            InvokeCommand(command, null);
        }

        public void Execute<T1>(string command, T1 t1)
        {
            InvokeCommand(command, new object[] {t1});
        }

        public void Execute<T1, T2>(string command, T1 t1, T2 t2)
        {
            InvokeCommand(command, new object[] { t1, t2 });
        }

        public void Execute<T1, T2, T3>(string command, T1 t1, T2 t2, T3 t3)
        {
            InvokeCommand(command, new object[] { t1, t2, t3 });
        }

        public void Execute<T1, T2, T3, T4>(string command, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            InvokeCommand(command, new object[] { t1, t2, t3, t4 });
        }

        public void ReadFrom(System.IO.Stream strm)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(System.IO.Stream strm)
        {
            throw new NotImplementedException();
        }
    }
}
