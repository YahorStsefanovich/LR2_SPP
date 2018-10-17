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

          public Generator()
          {
               typeDictionary = new Dictionary<Type, Func<object>>();
               pluginName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Plugins.dll");
               if (!File.Exists(pluginName))
               {
                    throw new InvalidPluginPathException("Wrong plugin's path");
               }

               assembly = Assembly.LoadFrom(pluginName);
               typeDictionary = fillDictionary(typeDictionary);
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
     }
}
