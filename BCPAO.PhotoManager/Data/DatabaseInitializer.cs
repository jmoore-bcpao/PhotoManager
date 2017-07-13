using System.Linq;

namespace BCPAO.PhotoManager.Data
{
   public class DatabaseInitializer
   {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Photos.Any())
            {
                return; // DB has been seeded
            }

            var users = new User[]
            {
                new User { Email="james.moore@bcpao.us", LockoutEnabled=false, NormalizedEmail="james.moore@bcpao.us", UserName="james.moore" }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
