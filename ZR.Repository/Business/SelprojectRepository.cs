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
    /// @date 2026-04-30
    /// </summary>
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class SelprojectRepository : BaseRepository<Selproject>
    {
        #region 业务逻辑代码
        #endregion
    }
}