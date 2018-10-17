using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     class Faker
     {          
          private ConstructorInfo getConstructorWithMaxParameters(Type type)
          {
               ConstructorInfo[] constructors = type.GetConstructors();
               ConstructorInfo maxConstructor = constructors[0];
               if (constructors.Length > 0)
               {                 
                    int maxLength = maxConstructor.GetParameters().Length;
                    foreach (ConstructorInfo constructor in constructors)
                    {
                         if (constructor.GetParameters().Length > maxLength)
                         {
                              maxLength = constructor.GetParameters().Length;
                              maxConstructor = constructor;
                         }
                    }
                    return maxConstructor;
               }
               return null;
          }

          public T Create<T>() where T : class, new(){
               Type type = typeof(T).GetType();

               //find constructors with max amount of parameters
               ConstructorInfo constructor = getConstructorWithMaxParameters(type);
               if (constructor != null)
               {                   
                    object obj = constructor.Invoke(constructor, constructor.GetParameters());
                    return (T)obj;
               }
               else
               {
                    object obj = new T();
                    //fill in fields with piblic fields 
                    FieldInfo[] fieldNames = type.GetFields();
                    foreach (FieldInfo field in fieldNames)
                    {
                      
                    }
                    return (T)obj;
               }            
                                             
          }          
     }
}
