using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZR.Model.Dto;
using ZR.Model.Models;

namespace ZR.Model.Dto
{
    /// <summary>
    /// 钉钉日志记录输入对象
    /// </summary>
    public class ProjectTimeEntryLogDto
    {
        [Required(ErrorMessage = "主键ID不能为空")]
        public int Id { get; set; }
        public string Creator { get; set; }
        public string CreatorDepartment { get; set; }
        public DateTime? EntryDate { get; set; }
        public string ProjectCode { get; set; }
        public string HospitalFullName { get; set; }
        public string ProjectName { get; set; }
        public string WorkLocation { get; set; }
        public decimal TransitHours { get; set; }
        public decimal ActualHours { get; set; }
        public string WorkSummary { get; set; }
        public string Remarks { get; set; }
        public string LogType { get; set; }
        public DateTime? ImportTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 钉钉日志记录查询对象
    /// </summary>
    public class ProjectTimeEntryLogQueryDto : PagerInfo 
    {
        public string Creator { get; set; }
        public string CreatorDepartment { get; set; }
        public DateTime? BeginEntryDate { get; set; }
        public DateTime? EndEntryDate { get; set; }
        public string ProjectCode { get; set; }
        public string HospitalFullName { get; set; }
        public string ProjectName { get; set; }
        public string WorkSummary { get; set; }
    }
}
