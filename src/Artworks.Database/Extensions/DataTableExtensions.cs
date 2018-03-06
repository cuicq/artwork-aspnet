using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Artworks.Database.Extensions
{
    /// <summary>
    /// Datatable 扩展。
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 将DataTable的数据转换成List泛型集合（建议使用）
        /// </summary>
        public static IList<T> Fill<T>(this DataTable that, Func<DataRow, T> func)
        {
            if (that == null || that.Rows.Count == 0)
            {
                return new List<T>();
            }
            IList<T> list = new List<T>(that.Rows.Count);
            foreach (DataRow dr in that.Rows)
            {
                list.Add(func(dr));
            }
            return list;
        }

        /// <summary>
        /// 将DataRow转换成T泛型对象（建议使用）
        /// </summary>
        public static T Fill<T>(this DataRow dr, Func<DataRow, T> func)
        {
            return func(dr);
        }


        [Obsolete("该方法使用反射影响性能，建议使用public static IList<T> Fill<T>(this DataTable that, Func<DataRow, T> func)")]
        public static List<T> Fill<T>(this DataTable that)
        {
            if (that == null || that.Rows.Count == 0)
            {
                return new List<T>();
            }
            List<T> list = new List<T>(that.Rows.Count);
            try
            {
                foreach (DataRow dr in that.Rows)
                {
                    T model = Fill<T>(dr);
                    if (model != null)
                        list.Add(model);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally { }
            return list;
        }


        [Obsolete("该方法使用反射影响性能，建议使用public static IList<T> Fill<T>(this DataTable that, Func<DataRow, T> func)")]
        public static IList<T> ToList<T>(this DataTable dt) where T : class ,new()
        {

            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            var t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
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

        [Obsolete("该方法使用反射影响性能，建议使用public static IList<T> Fill<T>(this DataTable that, Func<DataRow, T> func)")]
        public static T Fill<T>(this DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }
            T model = (T)Activator.CreateInstance(typeof(T));
            for (var i = 0; i < dr.Table.Columns.Count; i++)
            {
                var column = dr.Table.Columns[i];
                PropertyInfo propertyInfo = model.GetType().GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
            }
            return model;
        }


    }
}
