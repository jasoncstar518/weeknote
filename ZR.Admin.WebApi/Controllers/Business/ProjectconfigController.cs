using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Infrastructure.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using ZR.Admin.WebApi.Extensions;
using ZR.Admin.WebApi.Filters;
using ZR.Common;
using ZR.Model.Dto;
using ZR.Model.Models;
using ZR.Service.Business.IBusinessService;

namespace ZR.Admin.WebApi.Controllers
{
    /// <summary>
    /// 项目配置表Controller
    /// 
    /// @tableName projectconfig
    /// @author admin
    /// @date 2025-12-16
    /// </summary>
    [Verify]
    [Route("business/Projectconfig")]
    public class ProjectconfigController : BaseController
    {
        /// <summary>
        /// 项目配置表接口
        /// </summary>
        private readonly IProjectconfigService _ProjectconfigService;

        public ProjectconfigController(IProjectconfigService ProjectconfigService)
        {
            _ProjectconfigService = ProjectconfigService;
        }

        /// <summary>
        /// 查询项目配置表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "business:projectconfig:list")]
        public IActionResult QueryProjectconfig([FromQuery] ProjectconfigQueryDto parm)
        {
            var response = _ProjectconfigService.GetList(parm);
            return SUCCESS(response);
        }


        /// <summary>
        /// 查询项目配置表详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "business:projectconfig:query")]
        public IActionResult GetProjectconfig(long Id)
        {
            var response = _ProjectconfigService.GetFirst(x => x.Id == Id);
            
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加项目配置表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "business:projectconfig:add")]
        [Log(Title = "项目配置表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddProjectconfig([FromBody] ProjectconfigDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求参数错误");
            }
            long userId = HttpContext.GetUId();
            parm.CreatedBy = userId;
            parm.CreationTime = DateTimeHelper.GetBeginTime(DateTime.Now);
            parm.IsDeleted =0;
            parm.UpdatedBy = userId;
            parm.UpdateTime = DateTimeHelper.GetBeginTime(DateTime.Now);
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Projectconfig>().ToCreate(HttpContext);

            var response = _ProjectconfigService.AddProjectconfig(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 更新项目配置表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "business:projectconfig:edit")]
        [Log(Title = "项目配置表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateProjectconfig([FromBody] ProjectconfigDto parm)
        {
            if (parm == null)
            {
                throw new CustomException("请求实体不能为空");
            }
            long userId = HttpContext.GetUId();
            parm.UpdatedBy = userId;
            parm.UpdateTime = DateTimeHelper.GetBeginTime(DateTime.Now);
            parm.IsDeleted = 0;
            //从 Dto 映射到 实体
            var modal = parm.Adapt<Projectconfig>().ToUpdate(HttpContext);

            var response = _ProjectconfigService.Update(w => w.Id == modal.Id, it => new Projectconfig()
            {
                //Update 字段映射
                Code = modal.Code,
                Name = modal.Name,
                HospitalName = modal.HospitalName,
                Department = modal.Department,
                SigningDate = modal.SigningDate,
                ProjectType = modal.ProjectType,
                ProjectStatus = modal.ProjectStatus,
                ProjectLeader = modal.ProjectLeader,
                BusinessLeader = modal.BusinessLeader,
                PresalesLeader = modal.PresalesLeader,
                TotalControlFee = modal.TotalControlFee,
                DevelopmentControlFee = modal.DevelopmentControlFee,
                ProjectControlFee = modal.ProjectControlFee,
                MaintenanceControlFee = modal.MaintenanceControlFee,
                TotalWorkHours = modal.TotalWorkHours,
                DevelopmentWorkHours = modal.DevelopmentWorkHours,
                ProjectWorkHours = modal.ProjectWorkHours,
                MaintenanceWorkHours = modal.MaintenanceWorkHours,
                MilestoneCompletion = modal.MilestoneCompletion,
                Remarks = modal.Remarks,
                RiskAnalysis = modal.RiskAnalysis,
                ContractStatus = modal.ContractStatus,
                DocumentStatus = modal.DocumentStatus,
                AcceptanceReportStatus = modal.AcceptanceReportStatus,
                InspectionQ1 = modal.InspectionQ1,
                InspectionQ2 = modal.InspectionQ2,
                InspectionQ3 = modal.InspectionQ3,
                InspectionQ4 = modal.InspectionQ4,
                MaintenanceStartDate = modal.MaintenanceStartDate,
                MaintenanceEndDate = modal.MaintenanceEndDate,
                MaintenanceTerminationDate = modal.MaintenanceTerminationDate,
                CreatedBy = modal.CreatedBy,
                UpdatedBy = modal.UpdatedBy,
                CreationTime = modal.CreationTime,
                UpdateTime = modal.UpdateTime,
                IsDeleted = modal.IsDeleted,
            });

            return ToResponse(response);
        }

        /// <summary>
        /// 删除项目配置表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{ids}")]
        [ActionPermissionFilter(Permission = "business:projectconfig:delete")]
        [Log(Title = "项目配置表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteProjectconfig(string ids)
        {
            int[] idsArr = Tools.SpitIntArrary(ids);
            if (idsArr.Length <= 0) { return ToResponse(ApiResult.Error($"删除失败Id 不能为空")); }

            var response = _ProjectconfigService.Delete(idsArr);

            return ToResponse(response);
        }


    }
}