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
    public class SelprojectDto
    {
        [Required(ErrorMessage = "自增主键不能为空")]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string HospitalName { get; set; }
        public string Remarks { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreationTime { get; set; }
        [Required(ErrorMessage = "修改时间不能为空")]
        public DateTime? UpdateTime { get; set; }
        public byte IsDeleted { get; set; }
    }

    /// <summary>
    /// 项目配置表查询对象
    /// </summary>
    public class SelprojectQueryDto : PagerInfo 
    {
    }
}
