using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Artworks.Database.CommonModel
{
    /*
    /// <summary>
    /// 表示该类为Datatable帮助类
    /// </summary>
    public static class DataTableHelper
    {
        public static IList<T> ToList<T>(this DataTable dt) where T : class ,new()
        {
            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            var t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach(t.GetProperties(), p =>
            {
                if (dt.Columns.IndexOf(p.Name) != -1)
                    prlist.Add(p);
            });
            //创建返回的集合
            var oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                var ob = new T();
                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }

        public static T Fill<T>(this DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }
            T model = (T)Activator.CreateInstance(typeof(T));
            //T model = new T();
            for (var i = 0; i < dr.Table.Columns.Count; i++)
            {
                var column = dr.Table.Columns[i];
                PropertyInfo propertyInfo = model.GetType().GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
            }
            return model;
        }

        public static T[] FillToArray<T>(this DataTable dt)
        {
            T[] result = null;
            List<T> modelList = Fill<T>(dt);
            if (modelList != null && modelList.Count > 0)
            {
                result = new T[modelList.Count];
                modelList.CopyTo(result);
            }
            return result;
        }

        public static List<T> Fill<T>(this DataTable dt)
        {
            List<T> modelList = null;

            if (dt == null || dt.Rows.Count == 0)
            {
                modelList = new List<T>();
                return modelList;
            }

            try
            {
                if (dt.Rows.Count < 50)
                    modelList = new List<T>();
                else
                    modelList = new List<T>(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    T model = Fill<T>(dr);
                    if (model != null)
                        modelList.Add(model);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally { }
            return modelList;
        }

    }
    */
}
