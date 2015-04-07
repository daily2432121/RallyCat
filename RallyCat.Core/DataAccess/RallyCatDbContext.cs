using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace RallyCat.Core.DataAccess
{
    public class RallyCatDbContext
    {
        public static void SetConnectionString(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        private static string _connectionStringName = null;

        public static IDbContext QueryDb()
        {
            if (_connectionStringName == null)
            {
                throw new DataException("Please set ConnectionStringName first");
            }
            return new DbContext().ConnectionStringName(_connectionStringName, new SqlServerProvider());
        }
    }
}
