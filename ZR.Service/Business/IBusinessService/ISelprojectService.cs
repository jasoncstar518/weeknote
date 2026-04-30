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
    /// @date 2026-04-30
    /// </summary>
    public interface ISelprojectService : IBaseService<Selproject>
    {
        PagedInfo<Selproject> GetList(SelprojectQueryDto parm);

        int AddSelproject(Selproject parm);
    }
}
