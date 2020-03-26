namespace DoCover.Models
{
    public class DoOptions
    {

        /// <summary>
        /// 是否安装
        /// </summary>
        public bool Installed { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public int DbType { get; set; }

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string Conn { get; set; }

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix { get; set; }

    }
}
