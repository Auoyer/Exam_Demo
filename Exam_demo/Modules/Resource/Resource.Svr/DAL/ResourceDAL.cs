using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Resource.API;

namespace Resource.Svr
{
    /// <summary>
    /// 数据访问类:ResourceDAL
    /// </summary>
    public partial class ResourceDAL
    {
        public ResourceDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int chapterId,int userId,string resourceName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Resource where ChapterId=@ChapterId ");
            strSql.Append(" and ResourceName=@ResourceName ");
            strSql.AppendFormat(" and UserId in (0,{0})",userId);

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ChapterId", chapterId, dbType: DbType.Int32);
                param.Add("@ResourceName", resourceName, dbType: DbType.String);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        /// <summary>
        /// 获取章节下资源的数目
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public int GetResourceNum(int chapterId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.Append("select count(1) from Resource where ChapterId=@ChapterId");
            param.Add("@ChapterId",chapterId,dbType:DbType.Int32);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;

        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Resources model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Resource(");
            strSql.Append("ResourceName,FileName,FilePath,ImagePath,GuideRole,ConvertStatus,UserId,CreateDate)");

            strSql.Append(" values (");
            strSql.Append("@ResourceName,@FileName,@FilePath,@ImagePath,@GuideRole,@ConvertStatus,@UserId,@CreateDate)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ResourceName", model.ResourceName, dbType: DbType.String);
                param.Add("@FileName", model.FileName, dbType: DbType.String);
                param.Add("@FilePath", model.FilePath, dbType: DbType.String);
                param.Add("@ImagePath", model.ImagePath, dbType: DbType.String);
                param.Add("@GuideRole", model.GuideRole, dbType: DbType.Int32);
                param.Add("@ConvertStatus", model.ConvertStatus, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Resources model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Resource set ");
            strSql.Append("ResourceName=@ResourceName,");
            strSql.Append("FileName=@FileName,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("ImagePath=@ImagePath,");
            strSql.Append("GuideRole=@GuideRole,");
            strSql.Append("ConvertStatus=@ConvertStatus,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ResourceName", model.ResourceName, dbType: DbType.String);
                param.Add("@FileName", model.FileName, dbType: DbType.String);
                param.Add("@FilePath", model.FilePath, dbType: DbType.String);
                param.Add("@ImagePath", model.ImagePath, dbType: DbType.String);
                param.Add("@GuideRole", model.GuideRole, dbType: DbType.Int32);
                param.Add("@ConvertStatus", model.ConvertStatus, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateDate", model.CreateDate, dbType: DbType.DateTime);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Resource ");
            strSql.Append(" where Id=@Id ");
       

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }


        public bool DeleteResourceByChapter(int chapterId, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Resource ");
            strSql.Append(" where ChapterId=@ChapterId ");
            strSql.Append(" and UserId=@UserId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ChapterId", chapterId, dbType: DbType.Int32);
                param.Add("@UserId",userId,dbType:DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
 
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Resources GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Resource ");
            strSql.Append(" where Id=@Id ");

            Resources model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Resources>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Resources GetModelByRole(int RoleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Resource ");
            strSql.Append(" where GuideRole=@RoleId ");

            Resources model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@RoleId", RoleId, dbType: DbType.Int32);
                model = conn.Query<Resources>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Resources> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ResourceName,FileName,FilePath,ImagePath,ChapterId,ConvertStatus,UserId,CreateDate ");
            strSql.Append(" FROM Resource ");
            strSql.Append(GetStrWhere(filter));

            List<Resources> list = new List<Resources>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Resources>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" where 1=1 ");
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.ChapterId.HasValue)
            {
                strSql.Append(" and ChapterId="+filter.ChapterId);
            }
            if (filter.UserId.HasValue)
            {
                strSql.AppendFormat(" and UserId in (0,{0})",filter.UserId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetResourcePageParams()
        {
            PageModel model = new PageModel();
            model.Tables = "Resource";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ResourceName,FileName,FilePath,ImagePath,ChapterId,ConvertStatus,UserId,CreateDate";
            model.Filter = "";
            return model;
        }

        #endregion


        /// <summary>
        /// 更新转换状态
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateConvertStatus(string fileName,int status)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.Append("update Resource set ");
            strSql.Append("ConvertStatus=@ConvertStatus ");
            strSql.Append(" where FileName=@FileName");
            param.Add("@ConvertStatus",status,dbType:DbType.Int32);
            param.Add("@FileName",fileName,dbType:DbType.String);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(strSql.ToString(),param);
 
            }
            return result > 0;
 
        }



    }
}
