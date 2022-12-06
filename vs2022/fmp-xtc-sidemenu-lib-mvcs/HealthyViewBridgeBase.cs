
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.65.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using System.Threading.Tasks;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.SideMenu.LIB.Bridge;

namespace XTC.FMP.MOD.SideMenu.LIB.MVCS
{
    /// <summary>
    /// Healthy的视图桥接层基类（协议部分）
    /// 处理UI的事件
    /// </summary>
    public class HealthyViewBridgeBase : IHealthyViewBridge
    {

        /// <summary>
        /// 直系服务层
        /// </summary>
        public HealthyService? service { get; set; }


        /// <summary>
        /// 处理Echo的提交
        /// </summary>
        /// <param name="_dto">HealthyEchoRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnEchoSubmit(IDTO _dto, object? _context)
        {
            HealthyEchoRequestDTO? dto = _dto as HealthyEchoRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallEcho(dto?.Value, _context);
        }


    }
}
