using backend.Src.Models;

namespace backend.Src.Data
{
    public class Seed
    {
        /// <summary>
        /// Metodo que llena la base de datos .
        /// </summary>
        /// <param name="context">
        /// - context: Contexto de la base de datos.
        /// </param>
        public async static void SeedData(DataContext context)
        {
            await SeedUsers(context);
        }

        /// <summary>
        /// Metodo que se encarga de poblar la base de datos con usuarios.	
        /// </summary>
        /// <param name="context">
        /// - context: Contexto de la base de datos.
        /// </param>
        /// <returns>
        /// Añade usuarios a la base de datos .
        /// </returns>
        private async static Task SeedUsers(DataContext context)
        {
            if (context.Users.Any()) return;

            var users = new List<User>(){
                new(){
                    Fullname= "David Nahum Araya Cádiz",
                    Email = "david.araya@alumnos.ucn.cl",
                    Password = BCrypt.Net.BCrypt.HashPassword("20767691"),
                    Rut =  "20.767.691-8",
                    Birthday = 2001,
                },
            };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }

}
