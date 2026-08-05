using System;
using Infrastructure.Attribute;
using ZR.Repository.System;
using ZR.Model.Models;

namespace ZR.Repository
{
    /// <summary>
    /// 钉钉日志记录仓储
    ///
    /// @author admin
    /// @date 2026-07-29
    /// </summary>
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class ProjectTimeEntryLogRepository : BaseRepository<ProjectTimeEntryLog>
    {
        #region 业务逻辑代码
        #endregion
    }
}