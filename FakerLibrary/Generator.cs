using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LR2_SPP
{
     class Generator
     {
          private String pluginName;
          private Assembly assembly;
          private Dictionary<Type, Func<object>> typeDictionary;
          private CollectionGenerator collectionGenerator;
          private List<Type> dtoTypeList;
          private List<Type> cycleList;
          private Faker faker;

          public Generator()
          {
               typeDictionary = new Dictionary<Type, Func<object>>();
               collectionGenerator = new CollectionGenerator();

               dtoTypeList = new List<Type>();
               cycleList = new List<Type>();
               //string a = Assembly.GetEntryAssembly().Location;
               //string b = Path.GetDirectoryName(a);

               //pluginName = Path.Combine(b, "Plugins.dll");
               pluginName = "D:\\LR\\sem5(NOW)\\SPP\\LR2_SPP\\LR2_SPP\\LR2_SPP\\bin\\Debug\\Plugins.dll";
               if (!File.Exists(pluginName))
               {
                    throw new InvalidPluginPathException("Wrong plugin's path");
               }

               assembly = Assembly.LoadFile(pluginName);
               typeDictionary = fillDictionary(typeDictionary);
          }

          public void AddToCycle(Type t)
          {
               cycleList.Add(t);
          }

          public void RemoveFromCycle(Type t)
          {
               cycleList.Remove(t);
          }

          public void SetFaker(Faker faker)
          {
               this.faker = faker;
          }

          public void dtoAddType(Type t)
          {
               if (!dtoTypeList.Contains(t))
                    dtoTypeList.Add(t);
          }

          public void dtoRemoveType(Type t)
          {
               dtoTypeList.Remove(t);
          }

          private Dictionary<Type, Func<object>> fillDictionary(Dictionary<Type, Func<object>> dictionary)
          {
               foreach (var type in assembly.GetTypes())
               {
                    if (type.GetInterface(typeof(IGenerator).ToString()) != null)
                    {
                         var plugin = assembly.CreateInstance(type.FullName) as IGenerator;
                         if (!dictionary.ContainsKey(plugin.GetValueType()))
                              dictionary.Add(plugin.GetValueType(), plugin.generateValue);
                    }
               }
               return dictionary;              
          }
          
          public object GenerateValue(Type t)
          {
               object obj = null;
               Func<object> generatorDelegate = null;

               if (t.IsGenericType)
               {
                    obj = collectionGenerator.generateList(t.GenericTypeArguments[0], this);
               }
               else if (typeDictionary.TryGetValue(t, out generatorDelegate))
                    obj = generatorDelegate.Invoke();
               else if (dtoTypeList.Contains(t))
               {
                    if (!cycleList.Contains(t))
                         obj = faker.Create(t);
               }
               return obj;
          }        
     }
}
