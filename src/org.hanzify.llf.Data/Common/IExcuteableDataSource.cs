
using System;
using System.Collections;
using System.Web.UI;

namespace org.hanzify.llf.Data.Common
{
    public interface IExcuteableDataSource
    {
        IEnumerable Select(DataSourceSelectArguments arguments);
        int Delete(IDictionary keys, IDictionary values);
        int Insert(IDictionary values);
        int Update(IDictionary keys, IDictionary values, IDictionary oldValues);
    }
}
