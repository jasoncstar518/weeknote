using System;
using ZR.Model;
using ZR.Model.Dto;
using ZR.Model.Models;
using System.Collections.Generic;

namespace ZR.Service.Business.IBusinessService
{
    /// <summary>
    /// 医院环境版本表service接口
    ///
    /// @author admin
    /// @date 2026-03-24
    /// </summary>
    public interface IHospitalEnvService : IBaseService<HospitalEnv>
    {
        PagedInfo<HospitalEnv> GetList(HospitalEnvQueryDto parm);

        int AddHospitalEnv(HospitalEnv parm);
    }
}
