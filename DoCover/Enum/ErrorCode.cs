// ReSharper disable InconsistentNaming
using System.ComponentModel;

namespace DoCover.Enum
{
    /// <summary>
    /// 错误对照码
    /// </summary>
    public enum ErrorCode
    {
        #region E1错误
        /// <summary>
        /// 未知错误
        /// </summary>
        [Description("未知错误")]
        E1_15100 = 20100,
        #endregion

        #region E2错误
        /// <summary>
        /// 权限不足，无法访问。
        /// </summary>
        [Description("权限不足，无法访问。")]
        E2_20100 = 20100,

        /// <summary>
        /// 用户不存在。
        /// </summary>
        [Description("用户不存在。")]
        E2_21101 = 21101,

        /// <summary>
        /// 用户名重复。
        /// </summary>
        [Description("用户名重复。")]
        E2_21102 = 21102,

        /// <summary>
        /// 当前密码不正确。
        /// </summary>
        [Description("当前密码不正确")]
        E2_21103 = 21103,

        /// <summary>
        /// 数据库选择不正确。
        /// </summary>
        [Description("数据库选择不正确")]
        E2_21104 = 21104,

        /// <summary>
        /// 该用户有关联订单，推荐使用禁用功能。(禁止删除该用户)
        /// </summary>
        [Description("该用户有关联订单，推荐使用禁用功能。")]
        E2_22101 = 22101,

        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        E2_25100 = 25100,


        #endregion
    }
}