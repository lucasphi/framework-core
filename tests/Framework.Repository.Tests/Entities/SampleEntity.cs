using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repository.Tests.Entities
{
    class SampleEntity : FWEntity
    {
        public string Str { get; set; }

        public List<InnerEntity> List { get; set; }
    }

    class InnerEntity : FWEntity
    {
        public string InnerStr { get; set; }
    }
}
