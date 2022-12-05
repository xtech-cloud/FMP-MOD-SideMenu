
using System;
using LibMVCS = XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.SideMenu.LIB.Unity
{
    /// <summary>
    /// 虚拟数据
    /// </summary>
    public class DummyModel : DummyModelBase
    {

        public class DummyStatus : DummyStatusBase
        {
        }

        public DummyModel(string _uid) : base(_uid)
        {
        }
    }
}

