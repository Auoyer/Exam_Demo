using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Exam.API;

namespace Exam.Svr
{
    /// <summary>
    /// ���ݷ�����:PaperScore
    /// </summary>
    public partial class PaperScoreDAL
    {

        #region  �Ƿ����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PaperScore where Id=@Id ");

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
        public int Add(PaperScore model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PaperScore(");
            strSql.Append("PaperID,CharpterID,Count,Score)");

            strSql.Append(" values (");
            strSql.Append("@PaperID,@CharpterID,@Count,@Score)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@PaperID", model.PaperID, dbType: DbType.Int32);
                param.Add("@CharpterID", model.CharpterID, dbType: DbType.String);
                param.Add("@Count", model.Count, dbType: DbType.Int32);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
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
        public bool Update(PaperScore model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PaperScore set ");
            strSql.Append("PaperID=@PaperID,");
            strSql.Append("CharpterID=@CharpterID,");
            strSql.Append("Count=@Count,");
            strSql.Append("Score=@Score");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@PaperID", model.PaperID, dbType: DbType.Int32);
                param.Add("@CharpterID", model.CharpterID, dbType: DbType.String);
                param.Add("@Count", model.Count, dbType: DbType.Int32);
                param.Add("@Score", model.Score, dbType: DbType.Decimal);
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
            strSql.Append("delete from PaperScore ");
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
        public PaperScore GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PaperID,CharpterID,Count,Score from PaperScore ");
            strSql.Append(" where Id=@Id ");

            PaperScore model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<PaperScore>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  ���ݲ�ѯ������ȡ�б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<PaperScore> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PaperID,CharpterID,Count,Score ");
            strSql.Append(" FROM PaperScore ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<PaperScore> list = new List<PaperScore>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<PaperScore>(strSql.ToString()).ToList();
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
            return strSql.ToString();
        }

        #endregion

        #region  ��ȡ��ҳ����
        /// <summary>
        /// ��ȡ��ҳ����
        /// </summary>
        public PageModel GetPaperScorePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "PaperScore";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,PaperID,CharpterID,Count,Score";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

