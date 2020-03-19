using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoCover.Common;
using DoCover.Models;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace DoCover.Entitys
{
    public class MysqlContext
    {
        private readonly IOptionsSnapshot<DoOptions> _options;

        public MysqlContext(IOptionsSnapshot<DoOptions> options)
        {
            _options = options;
            DbType dbType = DbType.MySql;
            switch (_options.Value.DbType)
            {
                case 1:
                    dbType = DbType.MySql;
                    break;
                case 2:
                    dbType = DbType.SqlServer;
                    break;
                case 3:
                    dbType = DbType.Oracle;
                    break;
                case 4:
                    dbType = DbType.PostgreSQL;
                    break;
            }
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _options.Value.Conn,
                DbType = dbType,
                InitKeyType = InitKeyType.Attribute, //从特性读取主键和自增列信息
                IsAutoCloseConnection = true, //开启自动释放模式和EF原理一样我就不多解释了
            });
            MappingTableList map = new MappingTableList
            {
                {typeof(User).Name, _options.Value.TablePrefix + typeof(User).Name + "s"},
                {typeof(Setting).Name, _options.Value.TablePrefix + typeof(Setting).Name + "s"},
                {typeof(Order).Name, _options.Value.TablePrefix + typeof(Order).Name + "s"},
                {typeof(Setting).Name, _options.Value.TablePrefix + typeof(Setting).Name + "s"}
            };
            Db.MappingTables = map;
            //用来打印Sql方便你调式    
            Db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n" +
                                      Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName,
                                          it => it.Value)));
                };
        }


        public async Task InitDatabase()
        {
            Db.DbMaintenance.CreateDatabase();
            Db.CodeFirst.InitTables(typeof(User), typeof(Order), typeof(Setting));
            if (!this.User.IsAny(m => true))
            {
                await User.AsInsertable(new User() { Name = "admin", Pwd = "admin", Type = 0 })
                    .ExecuteCommandAsync();
            }

            if (!this.Setting.IsAny(m => true))
            {
                List<Setting> settings = new List<Setting>();
                RsaForJava rsa = new RsaForJava();
                var reRsakey = rsa.GetKey();
                foreach (var itme in System.Enum.GetValues(typeof(EnumSet)))
                {
                    EnumSet set = (EnumSet)itme;
                    string value = "";
                    switch (set)
                    {
                        case EnumSet.WebsiteName:
                            value = "才几美工系统";
                            break;
                        case EnumSet.WebsiteAdminEmail:
                            value = "admin@49os.com";
                            break;
                        case EnumSet.PrivateKey:
                            value = reRsakey.PrivateKey;
                            break;
                        case EnumSet.PublicKey:
                            value = reRsakey.PublicKey;
                            break;
                    }
                    settings.Add(new Setting(set,value));
                }
                await this.Setting.AsInsertable(settings).ExecuteCommandAsync();
            }
        }

        public SqlSugarClient Db;

        //建立表与模型的映射关系
        public DbSet<User> User => new DbSet<User>(Db);

        public DbSet<Order> Order => new DbSet<Order>(Db);

        public DbSet<Setting> Setting => new DbSet<Setting>(Db);
    }

    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {

        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="keyId"></param>
        /// <returns></returns>
        public T GetValueByKeyId(object keyId)
        {
            return Context.Queryable<T>().InSingle(keyId);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public IUpdateable<T> Updateable()
        {
            return Context.Updateable<T>();
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ISugarQueryable<T> Sql(string sql)
        {
            return Context.SqlQueryable<T>(sql);
        }

    }
}
