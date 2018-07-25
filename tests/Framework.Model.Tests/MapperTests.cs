using Framework.Collections;
using Framework.Model.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Framework.Model.Tests
{
    public class MapperTests
    {
        [Fact]
        public void MapEmptyList()
        {
            List<SimpleClass1> emptyList = new List<SimpleClass1>();
            var mappedList = emptyList.Map<SimpleClass2>();

            Assert.Equal(emptyList.Count, mappedList.Count());
        }

        [Fact]
        public void MapList()
        {
            List<SimpleClass1> list = new List<SimpleClass1>
            {
                new SimpleClass1() { Var1 = "Hello", Var2 = 1, List = new List<InnerClass>() {
                    new InnerClass() { InnerString = "Inner"}
                } }
            };
            var mappedList = list.Map<SimpleClass2>();

            Assert.Collection(mappedList, (item) =>
            {
                Assert.Equal("Hello", item.Var1);
                Assert.Equal(1, item.Var2Test);
                Assert.Collection(item.ListTest, (innerItem) =>
                {
                    Assert.Equal("Inner", innerItem.TestName);
                });
            });
        }

        [Fact]
        public void MapPrivateFields()
        {
            List<SimpleClass1> list = new List<SimpleClass1>
            {
                new SimpleClass1(10,
                                 new Dictionary<int, string>() { { 10, "a" } },
                                 new Dictionary<string, InnerClass>() { { "hello", new InnerClass() { InnerString = "Test" } } })
            };
            var mappedList = list.Map<SimpleClass2>();

            Assert.Collection(mappedList, (item) =>
            {
                Assert.Equal(10, item.Val1);
                Assert.Collection(item.Dic, val =>
                {
                    Assert.Equal("a", val.Value);
                });
                Assert.Collection(item.ComplexDic, val =>
                {
                    Assert.Equal("hello", val.Key);
                    Assert.Equal("Test", val.Value.InnerString);
                });
            });
        }

        [Fact]
        public void MapFWList()
        {
            FWList<InnerClass> list = new FWList<InnerClass>
            {
                new InnerClass() { InnerString = "Val1" },
                new InnerClass() { InnerString = "Val2" },
                new InnerClass() { InnerString = "Val3" }
            };

            list.RemoveItems(new List<int>() { 1 });

            var mappedList = list.Map<InnerClass2>() as FWList<InnerClass2>;

            Assert.Collection(mappedList.RemovedItems as IEnumerable<InnerClass2>, val =>
            {
                Assert.Equal("Val2", val.TestName);
            });
        }

        [Fact]
        public void MapObject()
        {
            SimpleClass1 sc = new SimpleClass1()
            {
                Var1 = "Hello",
                Var2 = 1,
                Datetime = new DateTime(2000, 01, 01),
                ClassEnum = EnumTest.Var2
            };
            var sc2 = sc.Map<SimpleClass2>();

            Assert.Equal("Hello", sc2.Var1);
            Assert.Equal(1, sc2.Var2Test);
            Assert.Equal(new DateTime(2000, 01, 01), sc2.Datetime);
            Assert.Equal(EnumTest.Var2, sc2.ClassEnum);
        }

        [Fact]
        public void MapObjectToExistingObject()
        {
            SimpleClass1 sc = new SimpleClass1()
            {
                Var1 = "Test",
                Var2 = 1
            };

            var partial = new PartialClass()
            {
                Var1 = "Hello"
            };

            sc = partial.Map(sc);

            Assert.Equal("Hello", sc.Var1);
            Assert.Equal(1, sc.Var2);
        }

        [Fact]
        public void MapWithCustomMap()
        {
            var c1 = new CustomMap1() { InnerString = "Hello" };
            var c2 = c1.Map<CustomMap2>();

            Assert.Equal("Hello", c2.InnerString);
            Assert.Equal(2, c2.CustomMap);

            var c3 = c1.Map<CustomMap2, CustomMapper>();

            Assert.Null(c3.InnerString);

            FWMapperHelper.AddMap<CustomMap1, CustomMap2, CustomMapper>();
            var c4 = c1.Map<CustomMap2>();

            Assert.Null(c4.InnerString);
        }
    }
}
