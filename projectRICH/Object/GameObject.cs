using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object
{
    public class GameObject<D> : IGameObject, ISerializable where D : IGameObject
    {
        private class CommandMethodChainTemplate
        {
            public System.Type ModuleType { get; set; }
            public System.Reflection.MethodInfo Method { get; set; }
            public int ModuleIndex { get; set; }
            public CommandMethodChainTemplate Next { get; set; }
        }

        private class CommandMethodChain
        {
            public Module.IModule Module { get; set; }
            public System.Reflection.MethodInfo Method { get; set; }
            public CommandMethodChain Next { get; set; }
        }

        static private Dictionary<string, CommandMethodChainTemplate> commands = new Dictionary<string, CommandMethodChainTemplate>();
        static private System.Type[] moduleTemplates;
        static GameObject()
        {
            var moduleAttrs = typeof(D).GetCustomAttributes(typeof(Module.ModuleAttribute), true);

            foreach (Module.ModuleAttribute a in moduleAttrs)
            {
                foreach (var m in a.Module.GetMethods())
                {
                    foreach (Module.ModuleCommand c in m.GetCustomAttributes(typeof(Module.ModuleCommand), false))
                    {
                        var commandParameters = m.GetParameters();
                        if (commandParameters.Length < 1 || !commandParameters[0].ParameterType.Equals(typeof(IGameObject)))
                        {
                            throw new ArgumentException("ModuleCommand는 반드시 인자가 1개 이상 있어야 하며, 첫번째 인자는 IGameObject이어야 합니다.");
                        }

                        // 커맨드는 반드시 인자가 하나 이상이어야 하고, 첫번째는 IGameObject타입이어야 함.
                        // 링크되는 커맨드들은 리턴은 void고 파라미터의 갯수와 타입이 동일해야 함.

                        var nextChain = new CommandMethodChainTemplate() { ModuleType = a.Module, Method = m, Next = null };

                        if (commands.ContainsKey(c.Name))
                        {
                            var chain = commands[c.Name];

                            if (!chain.Method.ReturnType.Equals(typeof(void)) ||
                                !nextChain.Method.ReturnType.Equals(typeof(void)))
                            {
                                throw new ArgumentException("링크되는 커맨드들의 리턴 타입은 void이어야 합니다. : " + c.Name);
                            }

                            var chainMethodParameters = chain.Method.GetParameters();
                            var nextMethodParameters = nextChain.Method.GetParameters();

                            if (chainMethodParameters.Length != nextMethodParameters.Length)
                            {
                                throw new ArgumentException("링크되는 커맨드들의 파라미터는 갯수와 타입이 동일해야 합니다. : " + c.Name);
                            }

                            for (int i = 0; i < chainMethodParameters.Length; ++i )
                            {
                                if (!chainMethodParameters[i].ParameterType.Equals(nextMethodParameters[i].ParameterType))
                                {
                                    throw new ArgumentException("링크되는 커맨드들의 파라미터는 갯수와 타입이 동일해야 합니다. : " + c.Name);
                                }
                            }

                            while (chain.Next != null)
                            {
                                chain = chain.Next;
                            }
                            chain.Next = nextChain;
                        }
                        else
                        {
                            commands[c.Name] = nextChain;
                        }
                    }
                }
            }
            var moduleToIndex = new Dictionary<Type, int>();
            var moduleList = new List<System.Type>();
            foreach (var v in commands.Values)
            {
                CommandMethodChainTemplate command = v;
                do
                {
                    var module = command.ModuleType;
                    if (!moduleToIndex.ContainsKey(module))
                    {
                        moduleToIndex[module] = moduleList.Count;
                        moduleList.Add(module);
                    }
                    command.ModuleIndex = moduleToIndex[module];
                    command = command.Next;
                } while (command != null);
            }

            moduleTemplates = moduleList.ToArray();
            
        }

        private Dictionary<string, CommandMethodChain> commandAccessor = new Dictionary<string, CommandMethodChain>();
        public GameObject()
        {
            Module.IModule[] moduleImpls = new Module.IModule[moduleTemplates.Length];
            for (int i = 0; i < moduleTemplates.Length; ++i)
            {
                moduleImpls[i] = moduleTemplates[i].GetConstructor(System.Type.EmptyTypes).Invoke(null) as Module.IModule;
            }

            foreach (var p in commands)
            {
                var currentCommand = p.Value;

                CommandMethodChain commandCursorImpl = null;
                var commandCursor = currentCommand;

                do
                {
                    var newCommand = new CommandMethodChain()
                    {
                        Method = currentCommand.Method,
                        Module = moduleImpls[currentCommand.ModuleIndex],
                        Next = null
                    };

                    if (commandCursorImpl == null)
                    {
                        commandAccessor[p.Key] = newCommand;
                        commandCursorImpl = newCommand;
                    }
                    else
                    {
                        commandCursorImpl.Next = newCommand;
                        commandCursorImpl = newCommand;
                    }

                    currentCommand = currentCommand.Next;
                } while (currentCommand != null);
            }
        }

        public int CurrentContext()
        {
            return 0;
        }

        private void InvokeCommand(string command, object[] param)
        {
            if (commandAccessor.ContainsKey(command))
            {
                var currentCommand = commandAccessor[command];
                do
                {
                    currentCommand.Method.Invoke(currentCommand.Module, param);
                    currentCommand = currentCommand.Next;
                } while (currentCommand != null);
            }
            else
            {
                throw new ArgumentException("커맨드가 없습니다. : " + command);
            }
        }

        private R InvokeCommand<R>(string command, object[] param)
        {
            if (commandAccessor.ContainsKey(command))
            {
                var currentCommand = commandAccessor[command];
                return (R)currentCommand.Method.Invoke(currentCommand.Module, param);
            }
            else
            {
                throw new ArgumentException("커맨드가 없습니다. : " + command);
            }
        }

        public void Execute(string command)
        {
            InvokeCommand(command, new object[] { this });
        }

        public void Execute<T1>(string command, T1 t1)
        {
            InvokeCommand(command, new object[] {this, t1});
        }

        public void Execute<T1, T2>(string command, T1 t1, T2 t2)
        {
            InvokeCommand(command, new object[] { this, t1, t2 });
        }

        public void Execute<T1, T2, T3>(string command, T1 t1, T2 t2, T3 t3)
        {
            InvokeCommand(command, new object[] { this, t1, t2, t3 });
        }

        public void Execute<T1, T2, T3, T4>(string command, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            InvokeCommand(command, new object[] { this, t1, t2, t3, t4 });
        }

        public void ReadFrom(System.IO.Stream strm)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(System.IO.Stream strm)
        {
            throw new NotImplementedException();
        }

        public R Execute<R>(string command)
        {
            return InvokeCommand<R>(command, new object[] { this });
        }

        public R Execute<T1, R>(string command, T1 t1)
        {
            return InvokeCommand<R>(command, new object[] { this, t1 });
        }

        public R Execute<T1, T2, R>(string command, T1 t1, T2 t2)
        {
            return InvokeCommand<R>(command, new object[] { this, t1, t2 });
        }

        public R Execute<T1, T2, T3, R>(string command, T1 t1, T2 t2, T3 t3)
        {
            return InvokeCommand<R>(command, new object[] { this, t1, t2, t3 });
        }
        public R Execute<T1, T2, T3, T4, R>(string command, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            return InvokeCommand<R>(command, new object[] { this, t1, t2, t3, t4 });
        }
    }
}
