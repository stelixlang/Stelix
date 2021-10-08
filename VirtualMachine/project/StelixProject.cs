using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VirtualMachine.natives;
using VirtualMachine.natives.attr;
using VirtualMachine.natives.web;
using VirtualMachine.project.model;
using VirtualMachine.project.section;

namespace VirtualMachine.project
{
    public class StelixProject
    {
        public Dictionary<string, Model> LoadedModels { get; }
        public string Name { get; }

        public static Dictionary<string, NativeModel> NativeModels { get; } = new Dictionary<string, NativeModel>();

        private static void SetupNatives()
        {
            _SetupNativeClass("system", "Shell", typeof(Shell));
            _SetupNativeClass("system/io", "File", typeof(File));
            _SetupNativeClass("system/web", "WebC", typeof(WebC));
        }

        static StelixProject()
        {
            SetupNatives();
        }

        private static void _SetupNativeClass(string location, string name, Type type)
        {
            NativeClass nativeClass = new NativeClass(location, name);
            foreach (MethodInfo methodInfo in type.GetMethods())
            {
                if (methodInfo.GetCustomAttribute<NATIVE_FUNC>() == null)
                {
                    continue;
                }
                nativeClass.Functions.Add(new NativeFunction(methodInfo.Name, methodInfo));
            }
            NativeModels.Add(location.Length != 0 ? location + "/" + name : name, nativeClass);
        }

        public StelixProject(string projectName)
        {
            Name = projectName;
            LoadedModels = new Dictionary<string, Model>();
        }

        public Model FindModel(string modelName)
        {
            foreach (KeyValuePair<string, Model> loadedModel in LoadedModels)
            {
                if (loadedModel.Key.Equals(modelName))
                {
                    return loadedModel.Value;
                }
            }

            foreach (KeyValuePair<string, NativeModel> nativeModel in NativeModels)
            {
                if (nativeModel.Key.Equals(modelName))
                {
                    return nativeModel.Value;
                }
            }

            return null;
        }

        public IFunction FindFunction(string className, string functionName)
        {
            foreach (KeyValuePair<string, Model> loadedModel in LoadedModels)
            {
                if (loadedModel.Key.Equals(className))
                {
                    if (loadedModel.Value.GetType() == typeof(ClassModel))
                    {
                        ClassModel classModel = (ClassModel) loadedModel.Value;
                        for (var i = 0; i < classModel.Functions.Count; i++)
                        {
                            IFunction function = classModel.Functions[i];
                            if (function.Name == functionName)
                            {
                                return function;
                            }
                        }
                    }
                    break;
                }
            }

            foreach (KeyValuePair<string, NativeModel> nativeModelPair in NativeModels)
            {
                if (nativeModelPair.Key.Equals(className))
                {
                    return ((NativeClass) nativeModelPair.Value).Functions.First(x => x.Name.Equals(functionName));
                }
            }

            return null;
        }
    }
}