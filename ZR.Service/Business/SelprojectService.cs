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

            //模糊查询：项目编码、项目名称、所属医院
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Code), it => it.Code.Contains(parm.Code));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Name), it => it.Name.Contains(parm.Name));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.HospitalName), it => it.HospitalName.Contains(parm.HospitalName));

            //搜索条件查询语法参考Sqlsugar
            var query = _SelprojectRepository
                .Queryable()
                .Where(predicate.ToExpression());

            //未指定排序字段时，默认按创建时间（插入时间）倒序
            if (string.IsNullOrEmpty(parm.Sort))
            {
                query = query.OrderBy(it => it.CreationTime, OrderByType.Desc);
            }

            var response = query.ToPage(parm);

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