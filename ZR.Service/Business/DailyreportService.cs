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
using Infrastructure.Extensions;

namespace ZR.Service.Business
{
    /// <summary>
    /// 日报记录表Service业务层处理
    ///
    /// @author admin
    /// @date 2025-11-10
    /// </summary>
    [AppService(ServiceType = typeof(IDailyreportService), ServiceLifetime = LifeTime.Transient)]
    public class DailyreportService : BaseService<Dailyreport>, IDailyreportService
    {
        private readonly DailyreportRepository _DailyreportRepository;
        public DailyreportService(DailyreportRepository repository)
        {
            _DailyreportRepository = repository;
        }

        #region 业务逻辑代码

        /// <summary>
        /// 查询日报记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<Dailyreport> GetList(DailyreportQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Dailyreport>();
            predicate.AndIF(parm.Userid>0, f => f.Userid == parm.Userid);
            predicate.AndIF(parm.StartTime.IsNotEmpty(), f => f.Reportdate >= parm.StartTime);
            predicate.AndIF(parm.EndTime.IsNotEmpty(), f => f.Reportdate < parm.EndTime);
            //搜索条件查询语法参考Sqlsugar
            var response = _DailyreportRepository
                .Queryable()
                .Where(predicate.ToExpression())
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 添加日报记录表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int AddDailyreport(Dailyreport parm)
        {
            var response = _DailyreportRepository.Insert(parm, it => new
            {
                it.Userid,
                it.Createtime,
                it.Isdeleted,
                it.Reportdate,
                it.Projectid,
                it.Hospitalname,
                it.Projectname,
                it.Worklocation,
                it.Intransithours,
                it.Actualhours,
                it.WorkSummary,
                it.Remarks,
            });
            return response;
        }
        #endregion
    }
}