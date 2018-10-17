using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LR2_SPP
{
     public class ConsoleWriter : IWriter
     {
          private List<Type> dtoList;

          public ConsoleWriter()
          {
               dtoList = new List<Type>();
          }

          public void dtoListAdd(Type t)
          {
               dtoList.Add(t);
          }

          public void dtoListRemove(Type t)
          {
               dtoList.Remove(t);
          }

          private void PrintArray(object obj, string indent)
          {
               Array array = (Array)obj;
               object value;

               Console.WriteLine(string.Format("Length = {0}", array.Length));
               for (int i = 0; i < array.Length; i++)
               {
                    value = array.GetValue(i);
                    Console.Write(string.Format("{0}[{1}] ", indent, i));
                    if (value.GetType().IsArray || value.GetType().IsGenericType)
                    {
                         PrintValue(value, indent + "    ");
                    }
                    else
                         Console.WriteLine(value);
               }
          }

          private void PrintList(object obj, string indent)
          {
               object value;
               IList list = (IList)obj;
               Console.WriteLine(string.Format("Length = {0}", list.Count));

               for (int i = 0; i < list.Count; i++)
               {
                    value = list[i];
                    Console.Write(string.Format("{0}[1]>", indent, i));
                    if (value.GetType().IsArray || value.GetType().IsGenericType)
                    {
                         PrintValue(value, indent + "    ");
                    }
                    else
                         Console.WriteLine(value);
               }
          }

          private void PrintValue(object obj, string indent)
          {
               if (obj != null)
               {
                    if (obj.GetType().IsArray)
                         PrintArray(obj, indent + "  ");
                    else if (obj.GetType().IsGenericType)
                         PrintList(obj, indent);
                    else if (dtoList.Contains(obj.GetType()))
                    {
                         Console.WriteLine();
                         Print(obj, indent + "  ");
                    }
                    else
                         Console.WriteLine(obj);
               }
               else
                    Console.WriteLine("null");
          }

          public void Print(object obj, String indent)
          {
               Type t;

               if (obj != null)
               {
                    t = obj.GetType();
                    Console.WriteLine(indent + "Fields:");
                    FieldInfo[] fields = t.GetFields();
                    foreach (FieldInfo field in fields)
                    {
                         Console.Write(string.Format("{0}: {1} , {2}  ", indent, field.FieldType.ToString(), field.Name));
                         PrintValue(field.GetValue(obj), indent);
                    }

                    Console.WriteLine(indent + "Propeties:");
                    PropertyInfo[] properties = t.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                         if (property.CanWrite && property.SetMethod.IsPublic)
                         {
                              Console.Write(string.Format("{0}: {1},  {2}  ", indent, property.PropertyType.ToString(), property.Name));
                              PrintValue(property.GetValue(obj), indent);
                         }
                    }
                    Console.WriteLine();
               }
               else
                    Console.WriteLine("null");
          }
     }
}
