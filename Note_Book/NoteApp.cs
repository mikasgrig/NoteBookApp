using System;
using Persistence.Models;
using Domain.Services;


namespace Note_Book
{
    class NoteApp
    {
        private readonly INotesService _notesService;
        public NoteApp(INotesService notesService)
        {
            _notesService = notesService;
        }
        public void Start()
        {
            var title = "";
            var text = "";
            Console.WriteLine("Note Book");
            Console.WriteLine("________________");
            while (true)
            {
                Console.WriteLine("Command:");
                Console.WriteLine("1 - Show Notes List ");
                Console.WriteLine("2 - Save note");
                Console.WriteLine("3 - Edit note");
                Console.WriteLine("4 - Delete note");
                Console.WriteLine("5 - Delete all notes");
                Console.WriteLine("6 - Exit");
                var choose = (Console.ReadLine());
                switch (choose)
                {
                    case "1":
                        var allNotes = _notesService.GetAll();
                        foreach (var item in allNotes)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case "2":
                        Console.Write("Enter note Title: ");
                        title = (Console.ReadLine());
                        Console.Write("Enter note Text: ");
                        text = (Console.ReadLine());
                        _notesService.Create(new Note
                        {
                            DateCreated = DateTime.Now,
                            Title = title,
                            Text = text
                        });
                        break;
                    case "3":
                        allNotes = _notesService.GetAll();
                        foreach (var item in allNotes)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        Console.Write("Enter note Id: ");
                        var idChoose = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter new note Title: ");
                        title = (Console.ReadLine());
                        Console.Write("Enter new note Text: ");
                        text = (Console.ReadLine());
                        _notesService.Edit(idChoose, title, text);
                        break;
                    case "4":
                        allNotes = _notesService.GetAll();
                        foreach (var item in allNotes)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        Console.Write("Enter note Index: ");
                        idChoose = Convert.ToInt32(Console.ReadLine());
                        _notesService.DeleteById(idChoose);
                        break;
                    case "5":
                        _notesService.ClearAll();
                        break;
                    case "6":
                        return;
                }
            }
        }
    }
}
