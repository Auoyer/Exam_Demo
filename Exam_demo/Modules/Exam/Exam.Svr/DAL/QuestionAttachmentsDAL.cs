using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;using Exam.API;

namespace Exam.Svr
{
	/// <summary>
	/// 数据访问类:QuestionAttachments
	/// </summary>
	public partial class QuestionAttachmentsDAL
	{
		
		#region  是否存在
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from QuestionAttachments where Id=@Id ");
			
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
		public int Add(QuestionAttachments model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QuestionAttachments(");
			strSql.Append("QuestionId,FileUrl,Name)");

			strSql.Append(" values (");
			strSql.Append("@QuestionId,@FileUrl,@Name)");
			strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
			
			int result = 0;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@QuestionId", model.QuestionId, dbType: DbType.Int32);
				param.Add("@FileUrl", model.FileUrl, dbType: DbType.String);
				param.Add("@Name", model.Name, dbType: DbType.String);
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
		public bool Update(QuestionAttachments model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QuestionAttachments set ");
			strSql.Append("QuestionId=@QuestionId,");
			strSql.Append("FileUrl=@FileUrl,");
			strSql.Append("Name=@Name");
			strSql.Append(" where Id=@Id ");
			
			int result = 0;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@Id", model.Id, dbType: DbType.Int32);
				param.Add("@QuestionId", model.QuestionId, dbType: DbType.Int32);
				param.Add("@FileUrl", model.FileUrl, dbType: DbType.String);
				param.Add("@Name", model.Name, dbType: DbType.String);
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
			strSql.Append("delete from QuestionAttachments ");
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
		public QuestionAttachments GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,QuestionId,FileUrl,Name from QuestionAttachments ");
			strSql.Append(" where Id=@Id ");
			
			QuestionAttachments model=null;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@Id", Id, dbType: DbType.Int32);
				model = conn.Query<QuestionAttachments>(strSql.ToString(), param).FirstOrDefault();
			}
			return model;
		}

		#endregion
		
		#region  根据查询条件获取列表
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<QuestionAttachments> GetList(CustomFilter filter)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,QuestionId,FileUrl,Name ");
			strSql.Append(" FROM QuestionAttachments ");
			strSql.Append(" where 1=1 ");
				strSql.Append(GetStrWhere(filter));
			
			List<QuestionAttachments> list = new List<QuestionAttachments>();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				list = conn.Query<QuestionAttachments>(strSql.ToString()).ToList();
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
		public PageModel GetQuestionAttachmentsPageParams(CustomFilter filter)
		{
			PageModel model = new PageModel();
			model.Tables = "QuestionAttachments";
			model.PKey = "Id";
			model.Sort = "Id";
			model.Fields = "Id,QuestionId,FileUrl,Name";
			model.Filter = GetStrWhere(filter);
			return model;
		}

		#endregion
		



	}
}

