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
          public void ShortGeneratorTest()
          {
               Assert.IsTrue(result.fieldShort >= short.MinValue && result.fieldShort <= short.MaxValue);
          }

          [TestMethod]
          public void IntGeneratorTest()
          {
               Assert.IsTrue(result.fieldInt >= int.MinValue && result.fieldInt <= int.MaxValue);
          }

          [TestMethod]
          public void LongGeneratorTest()
          {
               Assert.IsTrue(result.fieldLong >= long.MinValue && result.fieldLong <= long.MaxValue);
          }

          [TestMethod]
          public void FloatGeneratorTest()
          {
               Assert.IsTrue(result.fieldFloat >= float.MinValue && result.fieldFloat <= float.MaxValue);
          }
          
          [TestMethod]
          public void ByteGeneratorTest()
          {
               Assert.IsTrue(result.fieldByte >= byte.MinValue && result.fieldByte <= byte.MaxValue);
          }
         
          [TestMethod]
          public void BoolGeneratorTest()
          {
               Assert.IsTrue(result.fieldBool == true || result.fieldBool == false);
          }

          [TestMethod]
          public void CharGeneratorTest()
          {
               Assert.IsTrue((result.propertyChar >= char.MinValue) && (result.propertyChar <= char.MaxValue));
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
