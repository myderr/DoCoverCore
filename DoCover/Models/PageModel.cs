namespace DoCover.Models
{
    public class PageModel: BaseModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 分页条数
        /// </summary>
        public int Iimit { get; set; }
    }
}