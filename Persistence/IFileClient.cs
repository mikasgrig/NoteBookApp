﻿using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IFileClient
    {
        void Append(Note item, string filename);
        IEnumerable<T> ReadAll<T>(string filename);

        void WriteAll<T>(string filename, IEnumerable<T> items);
        void DeleteFileContents(string filename);
    }
}