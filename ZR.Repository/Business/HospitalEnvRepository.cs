using System;
using Infrastructure.Attribute;
using ZR.Repository.System;
using ZR.Model.Models;

namespace ZR.Repository
{
    /// <summary>
    /// 医院环境版本表仓储
    ///
    /// @author admin
    /// @date 2026-03-24
    /// </summary>
    [AppService(ServiceLifetime = LifeTime.Transient)]
    public class HospitalEnvRepository : BaseRepository<HospitalEnv>
    {
        #region 业务逻辑代码
        #endregion
    }
}