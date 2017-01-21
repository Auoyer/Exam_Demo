using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;using Exam.API;

namespace Exam.Svr
{
	/// <summary>
	/// ���ݷ�����:QuestionAttachments
	/// </summary>
	public partial class QuestionAttachmentsDAL
	{
		
		#region  �Ƿ����
		/// <summary>
		/// �Ƿ���ڸü�¼
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
		
		#region  ����

		/// <summary>
		/// ����һ������
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
		
		#region  ����
		/// <summary>
		/// ����һ������
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
		
		#region  ɾ��
		/// <summary>
		/// ɾ��һ������
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
		
		#region  ��ȡʵ��
		/// <summary>
		/// �õ�һ������ʵ��
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
		
		#region  ���ݲ�ѯ������ȡ�б�
		/// <summary>
		/// ��������б�
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
		/// ����CustomFilter��ȡwhere���
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
		
		#region  ��ȡ��ҳ����
		/// <summary>
		/// ��ȡ��ҳ����
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

