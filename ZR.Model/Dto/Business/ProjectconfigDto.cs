using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZR.Model.Dto;
using ZR.Model.Models;

namespace ZR.Model.Dto
{
    /// <summary>
    /// 项目配置表输入对象
    /// </summary>
    public class ProjectconfigDto
    {
        [Required(ErrorMessage = "自增主键不能为空")]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string HospitalName { get; set; }
        public string Department { get; set; }
        public DateTime? SigningDate { get; set; }
        public string ProjectType { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectLeader { get; set; }
        public string BusinessLeader { get; set; }
        public string PresalesLeader { get; set; }
        public decimal TotalControlFee { get; set; }
        public decimal DevelopmentControlFee { get; set; }
        public decimal ProjectControlFee { get; set; }
        public decimal MaintenanceControlFee { get; set; }
        public decimal TotalWorkHours { get; set; }
        public decimal DevelopmentWorkHours { get; set; }
        public decimal ProjectWorkHours { get; set; }
        public decimal MaintenanceWorkHours { get; set; }
        public string MilestoneCompletion { get; set; }
        public string Remarks { get; set; }
        public string RiskAnalysis { get; set; }
        public string ContractStatus { get; set; }
        public string DocumentStatus { get; set; }
        public string AcceptanceReportStatus { get; set; }
        public string InspectionQ1 { get; set; }
        public string InspectionQ2 { get; set; }
        public string InspectionQ3 { get; set; }
        public string InspectionQ4 { get; set; }
        public DateTime? MaintenanceStartDate { get; set; }
        public DateTime? MaintenanceEndDate { get; set; }
        public DateTime? MaintenanceTerminationDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public byte IsDeleted { get; set; }
    }

    /// <summary>
    /// 项目配置表查询对象
    /// </summary>
    public class ProjectconfigQueryDto : PagerInfo 
    {
    }
}
