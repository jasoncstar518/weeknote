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
    /// 钉钉日志记录Service业务层处理
    ///
    /// @author admin
    /// @date 2026-07-29
    /// </summary>
    [AppService(ServiceType = typeof(IProjectTimeEntryLogService), ServiceLifetime = LifeTime.Transient)]
    public class ProjectTimeEntryLogService : BaseService<ProjectTimeEntryLog>, IProjectTimeEntryLogService
    {
        private readonly ProjectTimeEntryLogRepository _ProjectTimeEntryLogRepository;
        public ProjectTimeEntryLogService(ProjectTimeEntryLogRepository repository)
        {
            _ProjectTimeEntryLogRepository = repository;
        }

        #region 业务逻辑代码

        /// <summary>
        /// 查询钉钉日志记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ProjectTimeEntryLog> GetList(ProjectTimeEntryLogQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<ProjectTimeEntryLog>();

            //搜索条件查询语法参考Sqlsugar
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Creator), it => it.Creator == parm.Creator);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.CreatorDepartment), it => it.CreatorDepartment == parm.CreatorDepartment);
            predicate = predicate.AndIF(parm.BeginEntryDate == null, it => it.EntryDate >= DateTime.Now.AddDays(-1));
            predicate = predicate.AndIF(parm.BeginEntryDate != null, it => it.EntryDate >= parm.BeginEntryDate && it.EntryDate <= parm.EndEntryDate);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProjectCode), it => it.ProjectCode == parm.ProjectCode);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.HospitalFullName), it => it.HospitalFullName.Contains(parm.HospitalFullName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProjectName), it => it.ProjectName.Contains(parm.ProjectName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.WorkSummary), it => it.WorkSummary.Contains(parm.WorkSummary));
            var response = _ProjectTimeEntryLogRepository
                .Queryable()
                .OrderBy("entry_date desc")
                .Where(predicate.ToExpression())
                .ToPage(parm);

            // 查询在途工时和实际工时合计
            var queryable = _ProjectTimeEntryLogRepository.Queryable().Where(predicate.ToExpression());
            var transitHoursSum = queryable.Sum(it => it.TransitHours);
            var actualHoursSum = queryable.Sum(it => it.ActualHours);
            response.Extra["transitHoursSum"] = transitHoursSum;
            response.Extra["actualHoursSum"] = actualHoursSum;

            return response;
        }

        /// <summary>
        /// 添加钉钉日志记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int AddProjectTimeEntryLog(ProjectTimeEntryLog parm)
        {
            var response = _ProjectTimeEntryLogRepository.Insert(parm, it => new
            {
                it.Creator,
                it.CreatorDepartment,
                it.EntryDate,
                it.ProjectCode,
                it.HospitalFullName,
                it.ProjectName,
                it.WorkLocation,
                it.TransitHours,
                it.ActualHours,
                it.WorkSummary,
                it.Remarks,
                it.LogType,
                it.ImportTime,
                it.CreateBy,
                it.CreateTime,
                it.UpdateBy,
                it.UpdateTime,
            });
            return response;
        }
        #endregion
    }
}