using System;
using System.Collections.Generic;
using SqlSugar;
using OfficeOpenXml.Attributes;

namespace ZR.Model.Models
{
    /// <summary>
    /// 钉钉日志记录，数据实体对象
    ///
    /// @author admin
    /// @date 2026-07-29
    /// </summary>
    [SugarTable("project_time_entry_log")]
    public class ProjectTimeEntryLog
    {
        /// <summary>
        /// 描述 :主键ID 
        /// 空值 : false  
        /// </summary>
        [EpplusTableColumn(Header = "主键ID")]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 描述 :创建人姓名 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "创建人姓名")]
        public string Creator { get; set; }

        /// <summary>
        /// 描述 :创建人所属部门 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "创建人所属部门")]
        [SugarColumn(ColumnName = "creator_department")]
        public string CreatorDepartment { get; set; }

        /// <summary>
        /// 描述 :填报日期 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "填报日期", NumberFormat = "yyyy-MM-dd HH:mm:ss")]
        [SugarColumn(ColumnName = "entry_date")]
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// 描述 :项目唯一编号 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "项目唯一编号")]
        [SugarColumn(ColumnName = "project_code")]
        public string ProjectCode { get; set; }

        /// <summary>
        /// 描述 :医院完整名称 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "医院完整名称")]
        [SugarColumn(ColumnName = "hospital_full_name")]
        public string HospitalFullName { get; set; }

        /// <summary>
        /// 描述 :项目名称 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "项目名称")]
        [SugarColumn(ColumnName = "project_name")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 描述 :工作地点 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "工作地点")]
        [SugarColumn(ColumnName = "work_location")]
        public string WorkLocation { get; set; }

        /// <summary>
        /// 描述 :在途工时（小时） 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "在途工时（小时）")]
        [SugarColumn(ColumnName = "transit_hours")]
        public decimal TransitHours { get; set; }

        /// <summary>
        /// 描述 :实际工时（小时） 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "实际工时（小时）")]
        [SugarColumn(ColumnName = "actual_hours")]
        public decimal ActualHours { get; set; }

        /// <summary>
        /// 描述 :工作内容简要描述 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "工作内容简要描述")]
        [SugarColumn(ColumnName = "work_summary")]
        public string WorkSummary { get; set; }

        /// <summary>
        /// 描述 :备注信息 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "备注信息")]
        public string Remarks { get; set; }

        /// <summary>
        /// 描述 :日志类型（如：正常/加班/调休等） 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "日志类型（如：正常/加班/调休等）")]
        [SugarColumn(ColumnName = "log_type")]
        public string LogType { get; set; }

        /// <summary>
        /// 描述 :数据导入时间 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "数据导入时间", NumberFormat = "yyyy-MM-dd HH:mm:ss")]
        [SugarColumn(ColumnName = "import_time")]
        public DateTime? ImportTime { get; set; }

        /// <summary>
        /// 描述 :创建者 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "创建者")]
        [SugarColumn(ColumnName = "create_by")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 描述 :创建时间 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "创建时间", NumberFormat = "yyyy-MM-dd HH:mm:ss")]
        [SugarColumn(ColumnName = "create_time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 描述 :更新者 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "更新者")]
        [SugarColumn(ColumnName = "update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 描述 :更新时间 
        /// 空值 : true  
        /// </summary>
        [EpplusTableColumn(Header = "更新时间", NumberFormat = "yyyy-MM-dd HH:mm:ss")]
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }



    }
}