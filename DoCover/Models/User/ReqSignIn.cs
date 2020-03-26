namespace DoCover.Models.User
{
    public class ReqSignIn
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}