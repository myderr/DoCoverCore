using System.ComponentModel;
using DoCover.Common;
using SqlSugar;

namespace DoCover.Entitys
{
    public class Setting: ModelContext
    {

        /// <summary>
        /// 设置表ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public int SetId { get; set; }

        /// <summary>
        /// 设置项
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string Key { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string Remark { get; set; }

        public Setting()
        {

        }

        public Setting(EnumSet set, string value)
        {
            SetId = (int)set;
            Key = set.ToString();
            Remark = set.GetDescription();
            Value = value;
        }

    }

    public enum EnumSet
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        [Description("网站名称")]
        WebsiteName = 1,
        /// <summary>
        /// 站长邮箱
        /// </summary>
        [Description("站长邮箱")]
        WebsiteAdminEmail = 2,

        /// <summary>
        /// 网站公钥
        /// </summary>
        [Description("网站公钥")]
        PublicKey = 3,

        /// <summary>
        /// 网站私钥
        /// </summary>
        [Description("网站私钥")]
        PrivateKey = 4,
    }
}
