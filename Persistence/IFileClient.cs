using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IFileClient
    {
        void Append(Note item, string filename, string query);
        IEnumerable<T> ReadAll<T>(string filename, string tableName);

        void WriteAll(string filename, int items, string query);
        void DeleteFileContents(string filename);
    }
}
