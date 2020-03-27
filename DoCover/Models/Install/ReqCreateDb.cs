namespace DoCover.Models.Install
{
    public class ReqCreateDb
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public int DbType { get; set; }
        
        /// <summary>
        /// ip
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix { get; set; }
    }
}