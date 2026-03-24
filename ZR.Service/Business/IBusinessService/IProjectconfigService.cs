using System;
using ZR.Model;
using ZR.Model.Dto;
using ZR.Model.Models;
using System.Collections.Generic;

namespace ZR.Service.Business.IBusinessService
{
    /// <summary>
    /// 项目配置表service接口
    ///
    /// @author admin
    /// @date 2025-12-16
    /// </summary>
    public interface IProjectconfigService : IBaseService<Projectconfig>
    {
        PagedInfo<Projectconfig> GetList(ProjectconfigQueryDto parm);

        int AddProjectconfig(Projectconfig parm);
    }
}
