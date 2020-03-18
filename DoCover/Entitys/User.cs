using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoCover.Models;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace DoCover.Entitys
{
    public class User
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(IsNullable = false,Length = 30)]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 32)]
        public string Pwd { get; set; }

        /// <summary>
        /// 类型：0-管理员，1-美工，2-前台
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Type { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 30)]
        public string NickName { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public string Qq { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public string Phone { get; set; }

        /// <summary>
        /// 状态：0-封禁，1-正常
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Status { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string Portrait { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 注册ip
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public string RegIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录ip
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 最后修改ip
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 16)]
        public string LastUpdateIp { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int LoginNum { get; set; }

        /// <summary>
        /// 金钱数量
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 18,DecimalDigits = 2)]
        public decimal Money { get; set; }

        /// <summary>
        /// 等级经验/积分
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int RankScores { get; set; }

        /// <summary>
        /// 唯一uuid
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 32)]
        public string Random { get; set; }

        /// <summary>
        /// 封禁备注
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 拓展
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Extend { get; set; }
    }
}