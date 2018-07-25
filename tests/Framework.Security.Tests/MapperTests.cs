using Framework.Model;
using Framework.Security.Tests.Models;
using System;
using Xunit;

namespace Framework.Security.Tests
{
    public class MapperTests
    {
        [Fact]
        public void FWSecureLongTest()
        {
            var model = new TestModel2() { Id = 10, Test = "Hello World!" };

            var map1 = model.Map<TestModel>();

            Assert.Equal(10, map1.Id.Value);
            Assert.Equal("Hello World!", map1.Test);

            var map2 = map1.Map<TestModel2>();

            Assert.Equal(10, map2.Id);
            Assert.Equal("Hello World!", map2.Test);
        }
    }
}
