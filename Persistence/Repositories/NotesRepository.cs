using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{

    public class NotesRepository : INotesRepository
    {
        private readonly IFileClient _fileClient;
        private const string FileName = "notes";
        public NotesRepository(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }
        public void Delete(int id)
        {
            var query = $"DELETE FROM note WHERE id = {id}";
            _fileClient.WriteAll(FileName, query);
        }

        public void DeleteAll()
        {
            _fileClient.DeleteFileContents(FileName);
        }

        public void Edit(int id, string title, string text)
        {

            var query = $"UPDATE note SET Title = '{title}', Text = '{text}' WHERE id = {id}";
            _fileClient.WriteAll(FileName, query);
        }

        public IEnumerable<Note> GetAll()
        {
           return _fileClient.ReadAll<Note>(FileName);
        }

        public void Save(Note note)
        {
            _fileClient.Append(note, FileName);
        }
    }
}
