using System;
using Infrastructure.Attribute;
using ZR.Repository.System;
using ZR.Model.Models;

namespace ZR.Repository
{
    /// <summary>
    /// 项目配置表仓储
    ///
    /// @author admin
    /// @date 2025-12-16
    /// </summary>
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class ProjectconfigRepository : BaseRepository<Projectconfig>
    {
        #region 业务逻辑代码
        #endregion
    }
}