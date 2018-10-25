using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR2_SPP;
using System.Collections.Generic;

namespace FakerTest
{
     [TestClass]
     public class FakerTests
     {
          private Faker faker;
          private TestClass result; 

          [TestInitialize]
          public void SetUp()
          {
               faker = new Faker();
               faker.dtoAdd(typeof(Foo));
               faker.dtoAdd(typeof(Bar));
               result = faker.Create<TestClass>();
          }

          [TestMethod]
          public void TwoSameTypesObject()
          {
               TestClass1 test1 = faker.Create<TestClass1>();
               Assert.AreNotEqual(test1.foo1, test1.foo2); 
          }

          [TestMethod]
          public void ShortGeneratorTest()
          {
               Assert.AreNotEqual(result.fieldShort, 0);
          }

          [TestMethod]
          public void IntGeneratorTest()
          {
               Assert.AreNotEqual(result.fieldInt, 0);
          }

          [TestMethod]
          public void LongGeneratorTest()
          {
               Assert.AreNotEqual(result.fieldLong, 0);
          }

          [TestMethod]
          public void FloatGeneratorTest()
          {
               Assert.AreNotEqual(result.fieldFloat, 0);
          }
          
          [TestMethod]
          public void ByteGeneratorTest()
          {
               Assert.AreNotEqual(result.fieldByte, 0);
          }
         
          [TestMethod]
          public void BoolGeneratorTest()
          {
               Assert.AreNotEqual(result.fieldBool, null);
          }

          [TestMethod]
          public void CharGeneratorTest()
          {
               Assert.AreNotEqual(result.propertyChar, 0);
          }

          [TestMethod]
          public void StringGeneratorTest()
          {
               Assert.IsTrue(result.fieldString != null && result.fieldString.Length != 0);
          }

          [TestMethod]
          public void DateTimeGeneratorTest()
          {
               Assert.IsTrue(result.fieldDate != null);
          }

          [TestMethod]
          public void ListGeneratorTest()
          {
               Assert.IsTrue(result.list != null && result.list is List<int>);
          }


          [TestMethod]
          public void NestingTest()
          {
               Assert.IsTrue(result.foo != null);
               Assert.IsTrue(result.foo.fieldBar != null);
               Assert.IsTrue(result.bar != null);
               Assert.IsTrue(result.bar.fieldFoo != null);
          }

          [TestMethod]
          public void CycleDependencyTest()
          {
               Assert.IsTrue(result.foo.fieldTest == null);
               Assert.IsTrue(result.foo.fieldBar.fieldFoo == null);
               Assert.IsTrue(result.bar.fieldFoo.fieldTest == null);
               Assert.IsTrue(result.bar.fieldFoo.fieldBar == null);
          }

     }
}
