using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     class Generator
     {
          private String pluginName;
          private Assembly assembly;
          private Dictionary<Type, Func<object>> typeDictionary;
          private CollectionGenerator collectionGenerator;
          private SystemGenerator dateTimeGenerator;
          private List<Type> dtoTypeList;
          private List<Type> cycleList;
          private Faker faker;

          public Generator()
          {
               typeDictionary = new Dictionary<Type, Func<object>>();
               collectionGenerator = new CollectionGenerator();
               dateTimeGenerator = new SystemGenerator();

               dtoTypeList = new List<Type>();
               cycleList = new List<Type>();

               pluginName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins.dll");
               if (!File.Exists(pluginName))
               {
                    throw new InvalidPluginPathException("Wrong plugin's path");
               }

               assembly = Assembly.LoadFrom(pluginName);
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

          public void DTOAddType(Type t)
          {
               if (!dtoTypeList.Contains(t))
                    dtoTypeList.Add(t);
          }

          public void DTORemoveType(Type t)
          {
               dtoTypeList.Remove(t);
          }

          private Dictionary<Type, Func<object>> fillDictionary(Dictionary<Type, Func<object>> dictionary)
          {
               foreach (Type type in assembly.GetExportedTypes())
               {
                    if (type.IsClass && typeof(IGenerator).IsAssignableFrom(type))
                    {
                         var plugin = assembly.CreateInstance(type.FullName) as IGenerator;
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
