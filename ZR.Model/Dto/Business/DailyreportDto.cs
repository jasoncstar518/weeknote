using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZR.Model.Dto;
using ZR.Model.Models;

namespace ZR.Model.Dto
{
    /// <summary>
    /// 日报记录表输入对象
    /// </summary>
    public class DailyreportDto
    {
        [Required(ErrorMessage = "不能为空")]
        public long Id { get; set; }
        public long? Userid { get; set; }
        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? Createtime { get; set; }
        public byte Isdeleted { get; set; }
        public DateTime? Reportdate { get; set; }
        public string Projectid { get; set; }
        public string Hospitalname { get; set; }
        public string Projectname { get; set; }
        public string Worklocation { get; set; }
        public decimal Intransithours { get; set; }
        public decimal Actualhours { get; set; }
        public string WorkSummary { get; set; }
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 日报记录表查询对象
    /// </summary>
    public class DailyreportQueryDto : PagerInfo 
    {
        public long? Userid { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
