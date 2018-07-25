using Framework.Collections;
using Framework.Model.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repository.Tests.Dtos
{
    class SampleDto : IFWMap
    {
        public string Str { get; set; }

        public FWList<InnerDto> List { get; set; }
    }

    class InnerDto
    {
        public string InnerStr { get; set; }
    }
}
