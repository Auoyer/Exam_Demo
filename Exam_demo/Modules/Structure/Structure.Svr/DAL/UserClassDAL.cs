using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Structure.API;

namespace Structure.Svr
{
    /// <summary>
    /// ���ݷ�����:UserClass
    /// </summary>
    public partial class UserClassDAL
    {

        #region  �Ƿ����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserClass where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  ����

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(UserClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserClass(");
            strSql.Append("UserId,ClassId,RoleId)");

            strSql.Append(" values (");
            strSql.Append("@UserId,@ClassId,@RoleId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@ClassId", model.ClassId, dbType: DbType.Int32);
                param.Add("@RoleId", model.RoleId, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

        #region  ����
        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(UserClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserClass set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("RoleId=@RoleId");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@ClassId", model.ClassId, dbType: DbType.Int32);
                param.Add("@RoleId", model.RoleId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  ɾ��
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserClass ");
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

        #endregion

        #region  ��ȡʵ��
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public UserClass GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,ClassId,RoleId from UserClass ");
            strSql.Append(" where Id=@Id ");

            UserClass model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<UserClass>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  ���ݲ�ѯ������ȡ�б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<UserClass> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,ClassId,RoleId ");
            strSql.Append(" FROM UserClass ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<UserClass> list = new List<UserClass>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UserClass>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// ����CustomFilter��ȡwhere���
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.Id2.HasValue)
            {
                strSql.Append(" and UserId=" + filter.Id2);
            }
            if (filter.IdList != null && filter.IdList.Count > 0)
            {
                strSql.AppendFormat(" and UserId in ('{0}')", string.Join("','", filter.IdList));
            }
            return strSql.ToString();
        }

        #endregion

        #region  ��ȡ��ҳ����
        /// <summary>
        /// ��ȡ��ҳ����
        /// </summary>
        public PageModel GetUserClassPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "UserClass";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,UserId,ClassId,RoleId";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion


        /*========================�Զ���ֽ���==========================*/
        /// <summary>
        /// ͳ������
        /// </summary>
        public int Count(int? classId, int? roleId)
        {
            int result = 0;
            var param = new DynamicParameters();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserClass where 1=1 ");
            if (classId.HasValue)
            {
                strSql.Append(" and ClassId=@ClassId ");
                param.Add("@ClassId", classId.Value, dbType: DbType.Int32);
            }
            if (roleId.HasValue)
            {
                strSql.Append(" and RoleId=@RoleId ");
                param.Add("@RoleId", roleId.Value, dbType: DbType.Int32);
            }
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }

    }
}

