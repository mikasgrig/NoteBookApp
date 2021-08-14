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
            var sql = "SELECT * FROM note";
            return _sqlClient.Query<Note>(sql);
        }

        public void Save(Note note)
        {
            var sql = "INSERT INTO note (DateCreated, Title, Text)  VALUES  (@DateCreated, @Title, @Text)";
            _sqlClient.Execute(sql, note);
        }

        public void Edit(int id, string title, string text)
        {
            var sql = "UPDATE note SET Title = @titleNew, Text = @textNew WHERE id = @idUser";
            var param = new {titleNew = title, TextNew= text, idUser = id  };
            _sqlClient.Execute(sql, param);

        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM note WHERE id = @idUser";
            var param = new
            {
                idUser = id
            };
            _sqlClient.Execute(sql, param);
        }

        public void DeleteAll()
        {
            var sql = "DELETE FROM note";
            _sqlClient.Execute(sql);
            sql = "ALTER TABLE `notes`.`note` AUTO_INCREMENT = 1 ";
            _sqlClient.Execute(sql);
        }
    }
}
