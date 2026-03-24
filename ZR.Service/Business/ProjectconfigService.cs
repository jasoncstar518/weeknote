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
    /// @date 2025-12-16
    /// </summary>
    [AppService(ServiceType = typeof(IProjectconfigService), ServiceLifetime = LifeTime.Transient)]
    public class ProjectconfigService : BaseService<Projectconfig>, IProjectconfigService
    {
        private readonly ProjectconfigRepository _ProjectconfigRepository;
        public ProjectconfigService(ProjectconfigRepository repository)
        {
            _ProjectconfigRepository = repository;
        }

        #region 业务逻辑代码

        /// <summary>
        /// 查询项目配置表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<Projectconfig> GetList(ProjectconfigQueryDto parm)
        {
            //开始拼装查询条件
            var predicate = Expressionable.Create<Projectconfig>();

            //搜索条件查询语法参考Sqlsugar
            var response = _ProjectconfigRepository
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
        public int AddProjectconfig(Projectconfig parm)
        {
            var response = _ProjectconfigRepository.Insert(parm, it => new
            {
                it.Code,
                it.Name,
                it.HospitalName,
                it.Department,
                it.SigningDate,
                it.ProjectType,
                it.ProjectStatus,
                it.ProjectLeader,
                it.BusinessLeader,
                it.PresalesLeader,
                it.TotalControlFee,
                it.DevelopmentControlFee,
                it.ProjectControlFee,
                it.MaintenanceControlFee,
                it.TotalWorkHours,
                it.DevelopmentWorkHours,
                it.ProjectWorkHours,
                it.MaintenanceWorkHours,
                it.MilestoneCompletion,
                it.Remarks,
                it.RiskAnalysis,
                it.ContractStatus,
                it.DocumentStatus,
                it.AcceptanceReportStatus,
                it.InspectionQ1,
                it.InspectionQ2,
                it.InspectionQ3,
                it.InspectionQ4,
                it.MaintenanceStartDate,
                it.MaintenanceEndDate,
                it.MaintenanceTerminationDate,
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