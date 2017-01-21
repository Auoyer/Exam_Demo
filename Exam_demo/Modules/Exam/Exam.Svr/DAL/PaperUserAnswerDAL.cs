using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;using Exam.API;
using Utils;

namespace Exam.Svr
{
	/// <summary>
	/// 数据访问类:PaperUserAnswer
	/// </summary>
	public partial class PaperUserAnswerDAL
	{
		
		#region  是否存在
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PaperUserAnswer where Id=@Id ");
			
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
		
		#region  新增

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(PaperUserAnswer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PaperUserAnswer(");
			strSql.Append("ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer)");

			strSql.Append(" values (");
			strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuesionTypeId,@Answer)");
			strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
			
			int result = 0;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
				param.Add("@UserId", model.UserId, dbType: DbType.Int32);
				param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
				param.Add("@QuesionTypeId", model.QuesionTypeId, dbType: DbType.Int32);
				param.Add("@Answer", model.Answer, dbType: DbType.Int32);
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
		public bool Update(PaperUserAnswer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PaperUserAnswer set ");
			strSql.Append("ExamPaperId=@ExamPaperId,");
			strSql.Append("UserId=@UserId,");
			strSql.Append("QuesionId=@QuesionId,");
			strSql.Append("QuesionTypeId=@QuesionTypeId,");
			strSql.Append("Answer=@Answer");
			strSql.Append(" where Id=@Id ");
			
			int result = 0;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@Id", model.Id, dbType: DbType.Int32);
				param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
				param.Add("@UserId", model.UserId, dbType: DbType.Int32);
				param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
				param.Add("@QuesionTypeId", model.QuesionTypeId, dbType: DbType.Int32);
				param.Add("@Answer", model.Answer, dbType: DbType.Int32);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PaperUserAnswer ");
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
		
		#region  获取实体
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public PaperUserAnswer GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer from PaperUserAnswer ");
			strSql.Append(" where Id=@Id ");
			
			PaperUserAnswer model=null;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@Id", Id, dbType: DbType.Int32);
				model = conn.Query<PaperUserAnswer>(strSql.ToString(), param).FirstOrDefault();
			}
			return model;
		}

		#endregion
		
		#region  根据查询条件获取列表
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<PaperUserAnswer> GetList(CustomFilter filter)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer ");
			strSql.Append(" FROM PaperUserAnswer ");
			strSql.Append(" where 1=1 ");
				strSql.Append(GetStrWhere(filter));
			
			List<PaperUserAnswer> list = new List<PaperUserAnswer>();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				list = conn.Query<PaperUserAnswer>(strSql.ToString()).ToList();
			}
			return list;
		}
		
		/// <summary>
		/// 根据CustomFilter获取where语句
		/// </summary>
		private string GetStrWhere(CustomFilter filter)
		{
			StringBuilder strSql=new StringBuilder();
			if(filter.Id.HasValue)
			{
				strSql.Append(" and Id=" + filter.Id);
			}
			return strSql.ToString();
		}

		#endregion
		
		#region  获取分页参数
		/// <summary>
		/// 获取分页参数
		/// </summary>
		public PageModel GetPaperUserAnswerPageParams(CustomFilter filter)
		{
			PageModel model = new PageModel();
			model.Tables = "PaperUserAnswer";
			model.PKey = "Id";
			model.Sort = "Id";
			model.Fields = "Id,ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer";
			model.Filter = GetStrWhere(filter);
			return model;
		}

		#endregion


        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        internal List<PaperUserAnswer> BatchAddPaperUserAnswer(List<PaperUserAnswer> PaperUserAnswer)
        {
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var model in PaperUserAnswer)
                    {
                        #region
                        strSql.Clear();
                        strSql.Append("insert into PaperUserAnswer(");
                        strSql.Append("ExamPaperId,UserId,QuesionId,QuesionTypeId,Answer)");

                        strSql.Append(" values (");
                        strSql.Append("@ExamPaperId,@UserId,@QuesionId,@QuesionTypeId,@Answer)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ExamPaperId", model.ExamPaperId, dbType: DbType.Int32);
                        param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                        param.Add("@QuesionId", model.QuesionId, dbType: DbType.Int32);
                        param.Add("@QuesionTypeId", model.QuesionTypeId, dbType: DbType.Int32);
                        param.Add("@Answer", model.Answer, dbType: DbType.Int32);
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        #endregion

                        conn.Execute(strSql.ToString(), param, tran);
                        model.Id = param.Get<int>("@returnid");

                    }

                    tran.Commit();
                    return PaperUserAnswer;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("BatchAddPaperUserAnswer", ex);
                    tran.Rollback();
                    return null;
                }
            }
        }

	}
}

