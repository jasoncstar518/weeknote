using System;
using Infrastructure.Attribute;
using ZR.Repository.System;
using ZR.Model.Models;

namespace ZR.Repository
{
    /// <summary>
    /// 日报记录表仓储
    ///
    /// @author admin
    /// @date 2025-11-10
    /// </summary>
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class DailyreportRepository : BaseRepository<Dailyreport>
    {
        #region 业务逻辑代码
        #endregion
    }
}