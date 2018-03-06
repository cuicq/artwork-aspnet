using Artworks.Database;
using Artworks.Database.CommonModel;
using Artworks.Database.Core;
using Artworks.Examples.Codes.Application.Code.CommonModel;
using Artworks.Examples.Codes.Application.Code.CommonModel.Contracts;
using Artworks.Infrastructure.Application.Persistence;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using Artworks.Database.Extensions;
using Artworks.Utility.Common;
using System.Collections.Generic;
using Artworks.Infrastructure.Application.Query;

namespace Artworks.Examples.Codes.Application.Code.Repositories
{
    public class UserRepository : Repository<Model>, IUserRepository
    {
        public UserRepository(IRepositoryContext context)
            : base(context)
        {

        }


        protected override void DoSave(Model model)
        {
            string sql = @"insert into t_user(t_id,t_name,t_createdate)values(@t_id,@t_name,@t_createdate)";

            DbParameter[] parameters ={
                  DataParameterProvider.Instance.MakeInParameter("@t_id", (DbType)MySqlDbType.Int32 ,4,model.ID),
                  DataParameterProvider.Instance.MakeInParameter("@t_name", (DbType)MySqlDbType.VarChar ,50,model.Name),
                  DataParameterProvider.Instance.MakeInParameter("@t_createdate", (DbType)MySqlDbType.DateTime ,8,model.CreateDate),
            };
            if (!DatabaseOperator.Master.ExecuteNonQuery(sql, CommandType.Text, parameters))
            {
                throw new OperationException(" Table dbo.[t_user] insert data failed.");
            }
        }

        protected override void DoRemove(Model model)
        {
            string sql = @"delete from t_user where t_id=@t_id";
            DbParameter[] parameters ={
                  DataParameterProvider.Instance.MakeInParameter("@t_id", (DbType)MySqlDbType.Int32 ,4,model.ID),
            };
            if (!DatabaseOperator.Master.ExecuteNonQuery(sql, CommandType.Text, parameters))
            {
                throw new OperationException(" Table dbo.[t_user] remove data failed.");
            }
        }

        protected override void DoChange(Model model)
        {
            string sql = @"update t_user set t_name=@t_name where t_id=@t_id";
            DbParameter[] parameters ={
                  DataParameterProvider.Instance.MakeInParameter("@t_id", (DbType)MySqlDbType.Int32 ,4,model.ID),
                  DataParameterProvider.Instance.MakeInParameter("@t_name", (DbType)MySqlDbType.VarChar ,50,model.Name)
            };
            if (!DatabaseOperator.Master.ExecuteNonQuery(sql, CommandType.Text, parameters))
            {
                throw new OperationException(" Table dbo.[t_user] update data failed.");
            }
        }

        public override Model FindBy(object id)
        {
            string sql = @"select t_id,t_name,t_createdate from t_user where t_id=@t_id";
            DbParameter parameter = DataParameterProvider.Instance.MakeInParameter("@t_id", (DbType)MySqlDbType.Int32, 4, id);
            var dt = DatabaseOperator.Master.ExecuteDataSet(sql, CommandType.Text, parameter).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0].Fill<Model>(TransformDataItem);
            }
            return null;
        }

        public override IEnumerable<Model> FindAll(QueryObject queryObject)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            string clause = queryObject.GetWhereClause(parameters);
            string sql = string.Format(" select t_id,t_name,t_createdate from t_user {0}", clause);
            var dt = DatabaseOperator.Master.ExecuteDataSet(sql, CommandType.Text, parameters.ToArray()).Tables[0];
            return dt.Fill<Model>(TransformDataItem);
        }

        private Model TransformDataItem(DataRow dr)
        {
            return new Model
            {
                ID = TypeUtil.GetInt(dr["t_id"], 0),
                Name = dr["t_name"].ToString(),
                CreateDate = TypeUtil.GetDateTime(dr["t_createdate"].ToString())
            };
        }
    }
}
