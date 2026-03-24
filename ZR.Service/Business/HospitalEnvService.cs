using System;
using SqlSugar;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Attribute;
using ZR.Model;
using ZR.Model.Dto;
using ZR.Model.Models;
using ZR.Repository;
using ZR.Service.Business.IBusinessService;

namespace ZR.Service.Business
{
    /// <summary>
    /// 医院环境版本表Service业务层处理
    ///
    /// @author admin
    /// @date 2026-03-24
    /// </summary>
    [AppService(ServiceType = typeof(IHospitalEnvService), ServiceLifetime = LifeTime.Transient)]
    public class HospitalEnvService : BaseService<HospitalEnv>, IHospitalEnvService
    {
        private readonly HospitalEnvRepository _HospitalEnvRepository;
        public HospitalEnvService(HospitalEnvRepository repository)
        {
            _HospitalEnvRepository = repository;
        }

        #region 业务逻辑代码

        /// <summary>
        /// 查询医院环境版本表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<HospitalEnv> GetList(HospitalEnvQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<HospitalEnv>();

            //搜索条件查询语法参考Sqlsugar
            var response = _HospitalEnvRepository
                .Queryable()
                .Where(predicate.ToExpression())
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 添加医院环境版本表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int AddHospitalEnv(HospitalEnv parm)
        {
            var response = _HospitalEnvRepository.Insert(parm, it => new
            {
                it.HospitalName,
                it.HospitalCode,
                it.ProdVersion,
                it.TestVersion,
                it.Status,
                it.Remark,
                it.CreateBy,
                it.CreateTime,
                it.UpdateBy,
                it.UpdateTime,
                it.DelFlag,
            });
            return response;
        }
        #endregion
    }
}