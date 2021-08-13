using System;
using System.Collections.Generic;
using System.Linq;
using Persistence.Models;
using Persistence.Repositories;

namespace Domain.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly List<String> _swearWord;
        public NotesService(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
            _swearWord = new List<string>
            {
                "Car",
                "Tree",
                "Walk",
                "Dog"
            };
        }
        public void ClearAll()
        {
            _notesRepository.DeleteAll();
        }

        public void Create(Note note)
        {
            foreach (var item in _swearWord)
            {
                if (_swearWord.Any(item => note.Text == item))
                {
                    throw new Exception("Note id invalid!");
                }
            }
            _notesRepository.Save(note);
        }

        public void DeleteById(int id)
        {
            _notesRepository.Delete(id);
        }

        public void Edit(int id, string title, string text)
        {
            _notesRepository.Edit(id, title, text);
        }

        public IEnumerable<Note> GetAll()
        {
            var allNotes = _notesRepository.GetAll();
            return allNotes;
        }
    }
}
