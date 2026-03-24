using System;
using System.Collections.Generic;
using SqlSugar;
using OfficeOpenXml.Attributes;

namespace ZR.Model.Models
{
    /// <summary>
    /// 项目配置表，数据实体对象
    ///
    /// @author admin
    /// @date 2025-12-16
    /// </summary>
    [SugarTable("projectconfig")]
    public class Projectconfig
    {
        /// <summary>
        /// 描述 :自增主键 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 描述 :项目编码 
        /// 空值 : true  
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述 :项目名称 
        /// 空值 : true  
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述 :所属医院 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "hospital_name")]
        public string HospitalName { get; set; }

        /// <summary>
        /// 描述 :建设科室 
        /// 空值 : true  
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 描述 :签订时间 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "signing_date")]
        public DateTime? SigningDate { get; set; }

        /// <summary>
        /// 描述 :项目类型（可选值：全新建设、运维） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "project_type")]
        public string ProjectType { get; set; }

        /// <summary>
        /// 描述 :项目状态（可选值：备案、在建、运维、已结束） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "project_status")]
        public string ProjectStatus { get; set; }

        /// <summary>
        /// 描述 :项目负责人 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "project_leader")]
        public string ProjectLeader { get; set; }

        /// <summary>
        /// 描述 :商务负责人 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "business_leader")]
        public string BusinessLeader { get; set; }

        /// <summary>
        /// 描述 :售前负责人 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "presales_leader")]
        public string PresalesLeader { get; set; }

        /// <summary>
        /// 描述 :控费总金额(万) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "total_control_fee")]
        public decimal TotalControlFee { get; set; }

        /// <summary>
        /// 描述 :控费开发金额(万) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "development_control_fee")]
        public decimal DevelopmentControlFee { get; set; }

        /// <summary>
        /// 描述 :控费项目金额(万) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "project_control_fee")]
        public decimal ProjectControlFee { get; set; }

        /// <summary>
        /// 描述 :控费运维金额(万) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_control_fee")]
        public decimal MaintenanceControlFee { get; set; }

        /// <summary>
        /// 描述 :控费总工时(人天) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "total_work_hours")]
        public decimal TotalWorkHours { get; set; }

        /// <summary>
        /// 描述 :控费开发工时(人天) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "development_work_hours")]
        public decimal DevelopmentWorkHours { get; set; }

        /// <summary>
        /// 描述 :控费项目工时(人天) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "project_work_hours")]
        public decimal ProjectWorkHours { get; set; }

        /// <summary>
        /// 描述 :控费运维工时(人天) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_work_hours")]
        public decimal MaintenanceWorkHours { get; set; }

        /// <summary>
        /// 描述 :里程碑完成节点情况 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "milestone_completion")]
        public string MilestoneCompletion { get; set; }

        /// <summary>
        /// 描述 :备注 
        /// 空值 : true  
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 描述 :控费风险分析说明 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "risk_analysis")]
        public string RiskAnalysis { get; set; }

        /// <summary>
        /// 描述 :合同状态（可选值：有、待签、已签、待招标、售前、合同中、已结束、中止运维） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "contract_status")]
        public string ContractStatus { get; set; }

        /// <summary>
        /// 描述 :资料状态（可选值：挂起、无、公司挂起） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "document_status")]
        public string DocumentStatus { get; set; }

        /// <summary>
        /// 描述 :验收报告状态（可选值：有、无） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "acceptance_report_status")]
        public string AcceptanceReportStatus { get; set; }

        /// <summary>
        /// 描述 :一季度巡检报告（可选值：有、无） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "inspection_q1")]
        public string InspectionQ1 { get; set; }

        /// <summary>
        /// 描述 :二季度巡检报告（可选值：有、无） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "inspection_q2")]
        public string InspectionQ2 { get; set; }

        /// <summary>
        /// 描述 :三季度巡检报告（可选值：有、无） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "inspection_q3")]
        public string InspectionQ3 { get; set; }

        /// <summary>
        /// 描述 :四季度巡检报告（可选值：有、无） 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "inspection_q4")]
        public string InspectionQ4 { get; set; }

        /// <summary>
        /// 描述 :维保开始时间 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_start_date")]
        public DateTime? MaintenanceStartDate { get; set; }

        /// <summary>
        /// 描述 :维保结束时间 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_end_date")]
        public DateTime? MaintenanceEndDate { get; set; }

        /// <summary>
        /// 描述 :中止运维时间 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_termination_date")]
        public DateTime? MaintenanceTerminationDate { get; set; }

        /// <summary>
        /// 描述 :创建用户ID 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "created_by")]
        public long? CreatedBy { get; set; }

        /// <summary>
        /// 描述 :修改人用户ID 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "updated_by")]
        public long? UpdatedBy { get; set; }

        /// <summary>
        /// 描述 :创建时间 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(ColumnName = "creation_time")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 描述 :修改时间 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 描述 :是否删除(0否1是) 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "is_deleted")]
        public byte IsDeleted { get; set; }



    }
}