using System;
using ZR.Model;
using ZR.Model.Dto;
using ZR.Model.Models;
using System.Collections.Generic;

namespace ZR.Service.Business.IBusinessService
{
    /// <summary>
    /// 日报记录表service接口
    ///
    /// @author admin
    /// @date 2025-11-10
    /// </summary>
    public interface IDailyreportService : IBaseService<Dailyreport>
    {
        PagedInfo<Dailyreport> GetList(DailyreportQueryDto parm);

        int AddDailyreport(Dailyreport parm);
    }
}
