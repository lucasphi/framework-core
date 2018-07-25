using Framework.Model.Mapper;
using Framework.Model.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Model.Tests
{
    class BaseClass
    {
        public string Var1 { get; set; }
    }

    class SimpleClass1 : IFWMap
    {
        public SimpleClass1()
        { }

        public SimpleClass1(long val1, Dictionary<int, string> dic, Dictionary<string, InnerClass> complexDic)
        {
            _val1 = val1;
            _dic = dic;
            _complexDic = complexDic;
        }

        public string Var1 { get; set; }

        public int Var2 { get; set; }

        public DateTime? Datetime { get; set; }

        public EnumTest ClassEnum { get; set; }

        public List<InnerClass> List { get; set; }
        
        private long _val1;

        private Dictionary<int, string> _dic;

        private Dictionary<string, InnerClass> _complexDic;
    }

    class SimpleClass2 : BaseClass, IFWMap
    {
        [FWMapName("Var2")]
        public int Var2Test { get; set; }

        [FWMapName("List")]
        public List<InnerClass2> ListTest { get; set; }

        public DateTime? Datetime { get; set; }

        public EnumTest ClassEnum { get; set; }

#pragma warning disable 649
        [FWMapPrivate]
        private long _val1;

        [FWMapPrivate]
        private Dictionary<int, string> _dic;

        [FWMapPrivate]
        private Dictionary<string, InnerClass> _complexDic;
#pragma warning restore 649

        #region Helper Properties
        public long Val1
        {
            get { return _val1; }
        }

        public Dictionary<int, string> Dic
        {
            get { return _dic; }
        }

        public Dictionary<string, InnerClass> ComplexDic
        {
            get { return _complexDic; }
        }
        #endregion
    }

    public class InnerClass : IFWMap
    {
        public string InnerString { get; set; }
    }

    public class InnerClass2 : IFWMap
    {
        [FWMapName("InnerString")]
        public string TestName { get; set; }
    }

    public class PartialClass : IFWMap
    {
        public string Var1 { get; set; }
    }

    public class CustomMap1 : IFWMap
    {
        public string InnerString { get; set; }
    }

    public class CustomMap2 : IFWMap
    {
        public string InnerString { get; set; }

        [FWMapCustom(typeof(CustomProperty))]
        public int CustomMap { get; set; }
    }

    public enum EnumTest
    {
        Var1 = 1,
        Var2,
        Var3
    }
}
