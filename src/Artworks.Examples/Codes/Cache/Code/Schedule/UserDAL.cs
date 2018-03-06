using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Cache.Code.Schedule
{
    public class UserDAL
    {
        public static DataTable GetList()
        {
            string sql = @"select top 100  id as Id,name as Name,crdate as CreateDate from  sysobjects  with(nolock) order by id";
            return Artworks.Examples.Codes.Database.Code.DatabaseOperator.Tester.ExecuteDataSet(sql, CommandType.Text).Tables[0];
        }

    }
}
