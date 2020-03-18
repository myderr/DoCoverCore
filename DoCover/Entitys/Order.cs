using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace DoCover.Entitys
{
    public class Order
    {
        /// <summary>
        /// 订单id-年月日时分秒随机6位
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, Length = 32)]
        public string OrderId { get; set; }

        /// <summary>
        /// 状态，0-订单关闭，1-订单完成，2-订单未处理
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Status { get; set; }

        /// <summary>
        /// 创建者id-前台
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int CreateUserId { get; set; }

        /// <summary>
        /// 接单者id-美工
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int FinishUserId { get; set; }

        /// <summary>
        /// 顾客名字
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 30)]
        public string CustName { get; set; }

        /// <summary>
        /// 订单价格
        /// </summary>

        [SugarColumn(IsNullable = true, Length = 18, DecimalDigits = 2)]
        public decimal Price { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 30)]
        public string Type { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 30)]
        public string Title { get; set; }

        /// <summary>
        /// 订单主体内容
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string Content { get; set; }

        /// <summary>
        /// 规定时间 -秒
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int SpeciFiedTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string CustEmail { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 完成后的结果
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string Results { get; set; }
    }
}
