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
    /// 项目配置表Service业务层处理
    ///
    /// @author admin
    /// @date 2026-04-30
    /// </summary>
    [AppService(ServiceType = typeof(ISelprojectService), ServiceLifetime = LifeTime.Transient)]
    public class SelprojectService : BaseService<Selproject>, ISelprojectService
    {
        private readonly SelprojectRepository _SelprojectRepository;
        public SelprojectService(SelprojectRepository repository)
        {
            _SelprojectRepository = repository;
        }

        #region 业务逻辑代码

        /// <summary>
        /// 查询项目配置表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<Selproject> GetList(SelprojectQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Selproject>();

            //搜索条件查询语法参考Sqlsugar
            var response = _SelprojectRepository
                .Queryable()
                .Where(predicate.ToExpression())
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 添加项目配置表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int AddSelproject(Selproject parm)
        {
            var response = _SelprojectRepository.Insert(parm, it => new
            {
                it.Code,
                it.Name,
                it.HospitalName,
                it.Remarks,
                it.CreatedBy,
                it.UpdatedBy,
                it.CreationTime,
                it.UpdateTime,
                it.IsDeleted,
            });
            return response;
        }
        #endregion
    }
}