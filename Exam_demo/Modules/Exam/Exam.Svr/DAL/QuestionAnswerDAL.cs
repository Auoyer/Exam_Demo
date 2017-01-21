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
	/// ���ݷ�����:QuestionAnswer
	/// </summary>
	public partial class QuestionAnswerDAL
	{
		
		#region  �Ƿ����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from QuestionAnswer where Id=@Id ");
			
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
		public int Add(QuestionAnswer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QuestionAnswer(");
			strSql.Append("QuestionId,Answer,Sort)");

			strSql.Append(" values (");
			strSql.Append("@QuestionId,@Answer,@Sort)");
			strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");
			
			int result = 0;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@QuestionId", model.QuestionId, dbType: DbType.Int32);
				param.Add("@Answer", model.Answer, dbType: DbType.Int32);
				param.Add("@Sort", model.Sort, dbType: DbType.Int32);
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
		public bool Update(QuestionAnswer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QuestionAnswer set ");
			strSql.Append("QuestionId=@QuestionId,");
			strSql.Append("Answer=@Answer,");
			strSql.Append("Sort=@Sort");
			strSql.Append(" where Id=@Id ");
			
			int result = 0;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@Id", model.Id, dbType: DbType.Int32);
				param.Add("@QuestionId", model.QuestionId, dbType: DbType.Int32);
				param.Add("@Answer", model.Answer, dbType: DbType.Int32);
				param.Add("@Sort", model.Sort, dbType: DbType.Int32);
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
			strSql.Append("delete from QuestionAnswer ");
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
		public QuestionAnswer GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,QuestionId,Answer,Sort from QuestionAnswer ");
			strSql.Append(" where Id=@Id ");
			
			QuestionAnswer model=null;
			var param = new DynamicParameters();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				param.Add("@Id", Id, dbType: DbType.Int32);
				model = conn.Query<QuestionAnswer>(strSql.ToString(), param).FirstOrDefault();
			}
			return model;
		}

		#endregion
		
		#region  ���ݲ�ѯ������ȡ�б�
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<QuestionAnswer> GetList(CustomFilter filter)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,QuestionId,Answer,Sort ");
			strSql.Append(" FROM QuestionAnswer ");
			strSql.Append(" where 1=1 ");
				strSql.Append(GetStrWhere(filter));
			
			List<QuestionAnswer> list = new List<QuestionAnswer>();
			using (var conn = DBHelper.CreateConnection())
			{
				conn.Open();
				list = conn.Query<QuestionAnswer>(strSql.ToString()).ToList();
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
		public PageModel GetQuestionAnswerPageParams(CustomFilter filter)
		{
			PageModel model = new PageModel();
			model.Tables = "QuestionAnswer";
			model.PKey = "Id";
			model.Sort = "Id";
			model.Fields = "Id,QuestionId,Answer,Sort";
			model.Filter = GetStrWhere(filter);
			return model;
		}

		#endregion


       

	}
}

