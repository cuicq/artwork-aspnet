using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Artworks.Database.Extensions
{
    internal class test_group1
    {
        public class DataItem
        {
            public int ID
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }
        }

        private static void main()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = i.ToString();
                dr["Name"] = "Name" + i.ToString();
                dt.Rows.Add(dr);
            }

            dt.Fill<DataItem>(DataRowToDataItem);

            DataRow dataRow = dt.Rows[0];
            dataRow.Fill<DataItem>(DataRowToDataItem);
        }

        private static DataItem DataRowToDataItem(DataRow row)
        {
            return new DataItem() { ID = 0, Name = "" };
        }


    }
}
