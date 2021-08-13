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
            var allNotes = _fileClient.ReadAll<Note>(FileName).ToList();
            var deleteNotes = allNotes.Where(item => item.Id != id);
            /*_fileClient.WriteAll(FileName, deleteNotes);*/
        }

        public void DeleteAll()
        {
            _fileClient.DeleteFileContents(FileName);
        }

        public void Edit(int id, string title, string text)
        {
            var allNotes = _fileClient.ReadAll<Note>(FileName).ToList();
            var noteEdit = allNotes.First(item => item.Id == id);
            noteEdit.Title = title;
            noteEdit.Text = text;
           /* _fileClient.WriteAll(FileName, allNotes);*/
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
