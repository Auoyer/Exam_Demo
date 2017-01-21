namespace Utils
{
    /// <summary>
    /// 数据库配置名称，用于多跨库的事务提交
    /// 崔伟伟  2016-04-15
    /// </summary>
    public enum EnumDataBase
    {
        /// <summary>
        /// 竞赛数据库
        /// </summary>
        Match,

        /// <summary>
        /// 用户数据库
        /// </summary>
        User
    }

    /// <summary>
    /// 根据枚举，返回数据库链接配置
    /// </summary>
    public class EnumDataBaseName
    {
        public static string GetDataBaseConfig(EnumDataBase dataBase)
        {
            switch (dataBase)
            {
                case EnumDataBase.Match:
                    return "Match.Svr.SQL";

                case EnumDataBase.User:
                    return "Structure.Svr.SQL";

                default:
                    break;
            }
            return "";
        }
    }
}