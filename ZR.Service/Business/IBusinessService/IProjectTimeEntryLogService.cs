using System;
using ZR.Model;
using ZR.Model.Dto;
using ZR.Model.Models;
using System.Collections.Generic;

namespace ZR.Service.Business.IBusinessService
{
    /// <summary>
    /// 钉钉日志记录service接口
    ///
    /// @author admin
    /// @date 2026-07-29
    /// </summary>
    public interface IProjectTimeEntryLogService : IBaseService<ProjectTimeEntryLog>
    {
        PagedInfo<ProjectTimeEntryLog> GetList(ProjectTimeEntryLogQueryDto parm);

        int AddProjectTimeEntryLog(ProjectTimeEntryLog parm);
    }
}
