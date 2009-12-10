﻿using System.Data;
using System.Data.Common;
using Lephone.Data;
using Lephone.Data.SqlEntry;
using NUnit.Framework;

namespace Lephone.UnitTest.Data
{
    [TestFixture]
    public class DatasetTest : DataTestBase
    {
        [Test]
        public void TestUpdateDatasetDirect1()
        {
            var dc = DbEntry.Context;
            DataSet ds = dc.ExecuteDataset(new SqlStatement("select [Name] from [People] where 1=0"));

            dc.UsingConnection(delegate
            {
                var da = (DbDataAdapter)dc.Driver.GetDbAdapter();
                var sql = new SqlStatement("insert into [People] ([Name]) VALUES (@name)");
                var c = (DbCommand)dc.GetDbCommand(sql);
                c.Parameters.Add(dc.Driver.GetDbParameter(new DataParameter("name", ""), true));
                da.InsertCommand = c;

                DataTable dt = ds.Tables[0];

                for (int i = 0; i < 10; i++)
                {
                    object[] row = { "jxf" };
                    dt.Rows.Add(row);
                }

                da.Update(ds);
                ds.AcceptChanges();
            });

            var list = dc.From<SinglePerson>().Where(WhereCondition.EmptyCondition).OrderBy("Id").Select();
            Assert.AreEqual(13, list.Count);
            Assert.AreEqual("Tom", list[0].Name);
            Assert.AreEqual("jxf", list[3].Name);
            Assert.AreEqual("jxf", list[12].Name);
        }

        [Test]
        public void TestUpdateDatasetDirect2()
        {
            var dc = DbEntry.Context;
            var sql = new SqlStatement("select [Id],[Name] from [People] where 1=0");
            DataSet ds = dc.ExecuteDataset(sql);

            dc.UsingConnection(delegate
            {
                var da = (DbDataAdapter)dc.Driver.GetDbAdapter(dc.GetDbCommand(sql));
                var cb = dc.Driver.GetCommandBuilder();
                cb.DataAdapter = da;

                DataTable dt = ds.Tables[0];

                for (int i = 0; i < 10; i++)
                {
                    object[] row = { 0, "jxf" };
                    dt.Rows.Add(row);
                }

                da.Update(ds);
                ds.AcceptChanges();
            });

            var list = dc.From<SinglePerson>().Where(WhereCondition.EmptyCondition).OrderBy("Id").Select();
            Assert.AreEqual(13, list.Count);
            Assert.AreEqual("Tom", list[0].Name);
            Assert.AreEqual("jxf", list[3].Name);
            Assert.AreEqual("jxf", list[12].Name);
        }

        [Test]
        public void TestUpdateDataset1()
        {
            var dc = DbEntry.Context;
            DataSet ds = dc.ExecuteDataset(new SqlStatement("select [Name] from [People] where 1=0"));

            var sql = new SqlStatement("insert into [People] ([Name]) VALUES (@name)");
            sql.Parameters.Add(new DataParameter("@name", ""));

            DataTable dt = ds.Tables[0];

            for (int i = 0; i < 10; i++)
            {
                object[] row = { "jxf" };
                dt.Rows.Add(row);
            }

            dc.UpdateDataset(sql, null, null, ds);

            var list = dc.From<SinglePerson>().Where(WhereCondition.EmptyCondition).OrderBy("Id").Select();
            Assert.AreEqual(13, list.Count);
            Assert.AreEqual("Tom", list[0].Name);
            Assert.AreEqual("jxf", list[3].Name);
            Assert.AreEqual("jxf", list[12].Name);
        }

        [Test]
        public void TestUpdateDataset2()
        {
            var dc = DbEntry.Context;
            var sql = new SqlStatement("select [Name] from [People] where 1=0");
            DataSet ds = dc.ExecuteDataset(sql);

            DataTable dt = ds.Tables[0];

            for (int i = 0; i < 10; i++)
            {
                object[] row = { "jxf" };
                dt.Rows.Add(row);
            }

            dc.UpdateDataset(sql, ds);

            var list = dc.From<SinglePerson>().Where(WhereCondition.EmptyCondition).OrderBy("Id").Select();
            Assert.AreEqual(13, list.Count);
            Assert.AreEqual("Tom", list[0].Name);
            Assert.AreEqual("jxf", list[3].Name);
            Assert.AreEqual("jxf", list[12].Name);
        }
    }
}
