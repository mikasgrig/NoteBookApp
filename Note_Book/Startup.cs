using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Persistence;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_Book
{
    class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            AddSql(services);
            services.AddSingleton<INotesRepository, NotesRepository>();
            services.AddSingleton<INotesService, NotesService>();
            services.AddSingleton<NoteApp>();
            return services.BuildServiceProvider();
        }
        public IServiceCollection AddSql(IServiceCollection service)
        {

            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "Localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.UserID = "testas";
            connectionStringBuilder.Password = "Testas2020;";
            connectionStringBuilder.Database = "notes";
            var connectionString = connectionStringBuilder.GetConnectionString(true);

            service.AddTransient<ISqlClient>(_ => new SqlClient(connectionString));
            return service;
        }
    }
}
