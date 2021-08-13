using Persistence;
using Persistence.Repositories;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Note_Book
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            var startapProvaider = startup.ConfigureServices();
            var noteApp = startapProvaider.GetService<NoteApp>();
            noteApp.Start();
        }
    }
}
