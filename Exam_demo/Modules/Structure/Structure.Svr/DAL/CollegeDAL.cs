using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Structure.API;
using Utils;

namespace Structure.Svr
{
    /// <summary>
    /// ���ݷ�����:College
    /// </summary>
    public partial class CollegeDAL
    {

        #region  �Ƿ����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from College where Id=@Id ");

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
        public int Add(College model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into College(");
            strSql.Append("CollegeName,DomainName,CreateTime,CollegeCode)");

            strSql.Append(" values (");
            strSql.Append("@CollegeName,@DomainName,@CreateTime,@CollegeCode)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeName", model.CollegeName, dbType: DbType.String);
                param.Add("@DomainName", model.DomainName, dbType: DbType.String);
                param.Add("@CollegeCode", model.CollegeCode, dbType: DbType.String);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
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
        public bool Update(College model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update College set ");
            strSql.Append("CollegeName=@CollegeName,");
            strSql.Append("DomainName=@DomainName,");
            strSql.Append("CollegeCode=@CollegeCode");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@CollegeName", model.CollegeName, dbType: DbType.String);
                param.Add("@DomainName", model.DomainName, dbType: DbType.String);
                param.Add("@CollegeCode", model.CollegeCode, dbType: DbType.String);
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
            strSql.Append("delete from College ");
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
        public College GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CollegeName,DomainName,CreateTime,CollegeCode from College ");
            strSql.Append(" where Id=@Id ");

            College model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<College>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  ���ݲ�ѯ������ȡ�б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<College> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CollegeName,DomainName,CreateTime,CollegeCode ");
            strSql.Append(" FROM College ");
            strSql.Append(" where 1=1 ");
            if (filter != null)
            {
                strSql.Append(GetStrWhere(filter));
            }

            List<College> list = new List<College>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<College>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<College> GetList2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CollegeName,DomainName,CreateTime,CollegeCode ");
            strSql.Append(" FROM College ");
            strSql.Append(" where 1=1 ");
            if (filter != null)
            {
                strSql.Append(GetStrWhere2(filter));
            }

            List<College> list = new List<College>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<College>(strSql.ToString()).ToList();
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
            if (!string.IsNullOrWhiteSpace(filter.CollegeKey))
            {
                strSql.Append(" and (CollegeName like '%" + filter.CollegeKey.Trim() + "%' or DomainName like '%" + filter.CollegeKey.Trim() + "%' or CollegeCode like '%" + filter.CollegeKey.Trim() + "%')");
            }
            return strSql.ToString();
        }

        /// <summary>
        /// ����CustomFilter��ȡwhere���
        /// </summary>
        private string GetStrWhere2(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
          
            if (!string.IsNullOrWhiteSpace(filter.CollegeKey))
            {
                strSql.Append(" and CollegeName='" + filter.CollegeKey + "'");
            }
            return strSql.ToString();
        }

        #endregion

        #region  ��ȡ��ҳ����
        /// <summary>
        /// ��ȡ��ҳ����
        /// </summary>
        public PageModel GetCollegePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "College";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,CollegeName,DomainName,CreateTime,CollegeCode";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        /*=================�Զ���ֽ���==================*/

        /// <summary>
        /// �Ƿ���ڸü�¼(�޸�ʱ��������id������
        /// </summary>
        public bool Exists(int Id, string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from College where CollegeName=@CollegeName ");
            if (Id > 0)
            {
                strSql.Append(" and Id <> @Id ");
            }

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                if (Id > 0)
                {
                    param.Add("@Id", Id, dbType: DbType.Int32);
                }
                param.Add("@CollegeName", name, dbType: DbType.String);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #region ��ȡѧУ�����б� List<string> GetCollegeNameList()
        /// <summary>
        /// ��ȡѧУ�����б�
        /// </summary>
        /// <returns></returns>
        public List<string> GetCollegeNameList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CollegeName from College ");

            List<string> list = new List<string>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<string>(strSql.ToString()).ToList();
            }
            return list;
        }
        #endregion

        /// <summary>
        /// ����domainName��ȡѧУ
        /// </summary>     
        public College getCollegeByDomainName(string domainName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CollegeName,DomainName,CreateTime,CollegeCode from College ");
            strSql.Append(" where DomainName like '%" + domainName + "%' ");

            College model = null;
            //var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                //param.Add("@DomainName", domainName, dbType: DbType.Int32);
                model = conn.Query<College>(strSql.ToString()).FirstOrDefault();
            }
            return model;
        }

    }
}

