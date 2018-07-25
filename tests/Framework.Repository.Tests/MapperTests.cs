using Framework.Repository.Tests.Dtos;
using Framework.Repository.Tests.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Framework.Repository.Tests
{
    public class MapperTests
    {
        [Fact]
        public void EntityMapper()
        {
            var dto = new SampleDto()
            {
                Str = "Hello World!",
                List = new Collections.FWList<InnerDto>()
                {
                    new InnerDto() { InnerStr = "Hi!" }
                }
            };

            dto.List.RemoveItems(new List<int> { 0 });

            var entity = dto.MapEntity<SampleEntity>();

            Assert.Equal("Hello World!", entity.Str);
            Assert.Collection(entity.List, e =>
            {
                Assert.Equal("Hi!", e.InnerStr);
            });
        }
    }
}
