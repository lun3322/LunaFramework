using System.Data;

namespace Luna.Dapper
{
    public interface ILunaDbContextManager
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbConnection GetConnection(string name = "Default");

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetConnectionString(string name);
    }
}