namespace DoCover.Common
{
    public class Global
    {
        /// <summary>
        /// 获取mysql链接字符串
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetMysqlConn(string ip,string port,string database,string userId,string pwd)
        {
            return $"Data Source={ip};port={port};database={database};User Id={userId};Password={pwd};Character Set=utf8;sslmode=none";
        }

        /// <summary>
        /// 获取SqlServer链接字符串
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetSqlServerConn(string ip, string port, string database, string userId, string pwd)
        {
            return $"server={ip},{port};database={database};uid={userId};pwd={pwd};pooling=true;min pool size=1;max pool size=2;";
        }

        /// <summary>
        /// 获取PgSql链接字符串
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetPgSqlConn(string ip, string port, string database, string userId, string pwd)
        {
            return $"PORT={port};DATABASE={database};HOST={ip};PASSWORD={pwd};USER ID={userId}";
        }

        /// <summary>
        /// 获取Oracle链接字符串
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <param name="servername"></param>
        /// <returns></returns>
        public static string GetOracleConn(string ip, string port, string database, string userId, string pwd,string servername)
        {
            return $"Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {ip})(PORT = {port}))(CONNECT_DATA = (SERVICE_NAME = {servername}))); User Id = {userId}; Password = {pwd};";
        }
    }
}
