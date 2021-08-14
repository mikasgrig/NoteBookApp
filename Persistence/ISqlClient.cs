using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface ISqlClient
    {
        int Execute(string sql, object param = null);
        IEnumerable<T> Query<T>(string sql, object param = null);
    }
}
