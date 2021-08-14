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
       
        private readonly ISqlClient _sqlClient;

        public NotesRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        public IEnumerable<Note> GetAll()
        {
            var sql = "SELECT * FROM notes";
            return _sqlClient.Query<Note>(sql);
        }

        public void Save(Note note)
        {
            var sql = "INSERT INTO notes (DateCreated, Title, Text)  VALUES  (@DateCreated, @Title, @Text)";
            _sqlClient.Execute(sql, note);
        }

        public void Edit(int id, string title, string text)
        {
            var sql = "UPDATE notes SET Title = @title, Text = @text WHERE id = @id";
            var param = new {title, text, id  };
            _sqlClient.Execute(sql, param);

        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM notes WHERE id = @id";
            _sqlClient.Execute(sql, new { id });
        }

        public void DeleteAll()
        {
            var sql = "DELETE FROM notes";
            _sqlClient.Execute(sql);
            sql = "ALTER TABLE `notes`.`notes` AUTO_INCREMENT = 1 ";
            _sqlClient.Execute(sql);
        }
    }
}
