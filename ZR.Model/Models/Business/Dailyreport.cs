using System;
using System.Collections.Generic;
using SqlSugar;
using OfficeOpenXml.Attributes;

namespace ZR.Model.Models
{
    /// <summary>
    /// 日报记录表，数据实体对象
    ///
    /// @author admin
    /// @date 2025-11-10
    /// </summary>
    [SugarTable("dailyreport")]
    public class Dailyreport
    {
        /// <summary>
        /// 描述 : 
        /// 空值 : false  
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 描述 :用户ID 
        /// 空值 : true  
        /// </summary>
        public long? Userid { get; set; }

        /// <summary>
        /// 描述 :创建时间 
        /// 空值 : false  
        /// </summary>
        public DateTime? Createtime { get; set; }

        /// <summary>
        /// 描述 :是否删除(0否1是) 
        /// 空值 : true  
        /// </summary>
        public byte Isdeleted { get; set; }

        /// <summary>
        /// 描述 :日期 
        /// 空值 : true  
        /// </summary>
        public DateTime? Reportdate { get; set; }

        /// <summary>
        /// 描述 :项目编号 
        /// 空值 : true  
        /// </summary>
        public string Projectid { get; set; }

        /// <summary>
        /// 描述 :医院名称 
        /// 空值 : true  
        /// </summary>
        public string Hospitalname { get; set; }

        /// <summary>
        /// 描述 :项目名称 
        /// 空值 : true  
        /// </summary>
        public string Projectname { get; set; }

        /// <summary>
        /// 描述 :工作地点 
        /// 空值 : true  
        /// </summary>
        public string Worklocation { get; set; }

        /// <summary>
        /// 描述 :在途工时(h) 
        /// 空值 : true  
        /// </summary>
        public decimal Intransithours { get; set; }

        /// <summary>
        /// 描述 :实际工时(h) 
        /// 空值 : true  
        /// </summary>
        public decimal Actualhours { get; set; }

        /// <summary>
        /// 描述 :工作内容 
        /// 空值 : true  
        /// </summary>
        [SugarColumn(ColumnName = "work_summary")]
        public string WorkSummary { get; set; }

        /// <summary>
        /// 描述 :备注 
        /// 空值 : true  
        /// </summary>
        public string Remarks { get; set; }



    }
}