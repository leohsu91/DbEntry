﻿using System;
using System.Linq.Expressions;
using Lephone.Data.Common;
using Lephone.Data.QuerySyntax;
using Lephone.Data.Linq;

namespace Lephone.Data.Definition
{
    [Serializable]
    public class DbObjectModelBase<T, TKey> : DbObjectSmartUpdate where T : DbObjectModelBase<T, TKey>
    {
        protected static CK Col
        {
            get { return CK.Column; }
        }

        protected static FieldNameGetter<T> Field
        {
            get { return CK<T>.Field; }
        }

        public static T FindById(TKey id)
        {
            return DbEntry.GetObject<T>(id);
        }

        public static DbObjectList<T> FindBySql(string sqlStr)
        {
            return DbEntry.Context.ExecuteList<T>(sqlStr);
        }

        public static DbObjectList<T> FindBySql(SqlEntry.SqlStatement sql)
        {
            return DbEntry.Context.ExecuteList<T>(sql);
        }

        public static DbObjectList<T> FindAll()
        {
            return DbEntry.From<T>().Where(WhereCondition.EmptyCondition).Select();
        }

        public static DbObjectList<T> FindAll(OrderBy ob)
        {
            return DbEntry.From<T>().Where(WhereCondition.EmptyCondition).OrderBy(ob).Select();
        }

        public static DbObjectList<T> FindAll(string orderBy)
        {
            return DbEntry.From<T>().Where(WhereCondition.EmptyCondition).OrderBy(orderBy).Select();
        }

        public static DbObjectList<T> Find(WhereCondition con)
        {
            return DbEntry.From<T>().Where(con).Select();
        }

        public static DbObjectList<T> Find(WhereCondition con, OrderBy ob)
        {
            return DbEntry.From<T>().Where(con).OrderBy(ob).Select();
        }

        public static DbObjectList<T> Find(WhereCondition con, string orderBy)
        {
            return DbEntry.From<T>().Where(con).OrderBy(orderBy).Select();
        }

        public static T FindOne(WhereCondition con)
        {
            return DbEntry.GetObject<T>(con);
        }

        public static T FindOne(WhereCondition con, OrderBy ob)
        {
            return DbEntry.GetObject<T>(con, ob);
        }

        public static T FindOne(WhereCondition con, string orderBy)
        {
            return DbEntry.GetObject<T>(con, Data.OrderBy.Parse(orderBy));
        }

        public static IAfterWhere<T> Where(WhereCondition con)
        {
            return new QueryContent<T>(DbEntry.Context).Where(con);
        }

        public static DbObjectList<T> FindRecent(int count)
        {
            string Id = ObjectInfo.GetKeyField(typeof(T)).Name;
            return DbEntry.From<T>().Where(WhereCondition.EmptyCondition).OrderBy((DESC)Id).Range(1, count).Select();
        }

        public static long GetCount(WhereCondition con)
        {
            return DbEntry.From<T>().Where(con).GetCount();
        }

        public static decimal? GetMax(WhereCondition con, string columnName)
        {
            return DbEntry.From<T>().Where(con).GetMax(columnName);
        }

        public static DateTime? GetMaxDate(WhereCondition con, string columnName)
        {
            return DbEntry.From<T>().Where(con).GetMaxDate(columnName);
        }

        public static decimal? GetMin(WhereCondition con, string columnName)
        {
            return DbEntry.From<T>().Where(con).GetMin(columnName);
        }

        public static DateTime? GetMinDate(WhereCondition con, string columnName)
        {
            return DbEntry.From<T>().Where(con).GetMinDate(columnName);
        }

        public static decimal? GetSum(WhereCondition con, string columnName)
        {
            return DbEntry.From<T>().Where(con).GetSum(columnName);
        }

        public static T New
        {
            get
            {
                return (T)ObjectInfo.GetInstance(typeof(T)).Handler.CreateInstance();
            }
        }

        public static void DeleteAll()
        {
            DeleteAll(WhereCondition.EmptyCondition);
        }

        public static void DeleteAll(WhereCondition con)
        {
            DbEntry.Delete<T>(con);
        }

        #region Linq methods

        public static LinqQueryProvider<T, TKey> Table
        {
            get { return new LinqQueryProvider<T, TKey>(null); }
        }

        public static DbObjectList<T> Find(Expression<Func<T, bool>> condition)
        {
            return DbEntry.From<T>().Where(condition).Select();
        }

        public static DbObjectList<T> Find(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby)
        {
            return DbEntry.From<T>().Where(condition).OrderBy(orderby).Select();
        }

        public static DbObjectList<T> Find(Expression<Func<T, bool>> condition, string orderby)
        {
            return DbEntry.From<T>().Where(condition).OrderBy(orderby).Select();
        }

        public static DbObjectList<T> FindAll(Expression<Func<T, object>> orderby)
        {
            return DbEntry.From<T>().Where(WhereCondition.EmptyCondition).OrderBy(orderby).Select();
        }

        public static T FindOne(Expression<Func<T, bool>> condition)
        {
            return DbEntry.Context.GetObject(condition);
        }

        public static LinqOrderSyntax<T> OrderBy(Expression<Func<T, object>> orderby)
        {
            return new LinqOrderSyntax<T>(orderby, true);
        }

        public static LinqOrderSyntax<T> OrderByDescending(Expression<Func<T, object>> orderby)
        {
            return new LinqOrderSyntax<T>(orderby, false);
        }

        public static long GetCount(Expression<Func<T, bool>> condition)
        {
            return DbEntry.From<T>().Where(condition).GetCount();
        }

        public static decimal? GetMax(Expression<Func<T, bool>> condition, Expression<Func<T, object>> column)
        {
            return DbEntry.From<T>().Where(condition).GetMax(column);
        }

        public static DateTime? GetMaxDate(Expression<Func<T, bool>> condition, Expression<Func<T, object>> column)
        {
            return DbEntry.From<T>().Where(condition).GetMaxDate(column);
        }

        public static decimal? GetMin(Expression<Func<T, bool>> condition, Expression<Func<T, object>> column)
        {
            return DbEntry.From<T>().Where(condition).GetMin(column);
        }

        public static DateTime? GetMinDate(Expression<Func<T, bool>> condition, Expression<Func<T, object>> column)
        {
            return DbEntry.From<T>().Where(condition).GetMinDate(column);
        }

        public static decimal? GetSum(Expression<Func<T, bool>> condition, Expression<Func<T, object>> column)
        {
            return DbEntry.From<T>().Where(condition).GetSum(column);
        }

        public static int DeleteAll(Expression<Func<T, bool>> condition)
        {
            return DbEntry.Delete<T>(ExpressionParser<T>.Parse(condition));
        }

	    #endregion
    }
}
