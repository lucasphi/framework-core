using Framework.Model.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Security.Tests.Models
{
    class TestModel : IFWMap
    {
        public FWSecureLong Id { get; set; }

        public string Test { get; set; }
    }

    class TestModel2 : IFWMap
    {
        public long Id { get; set; }

        public string Test { get; set; }
    }
}
