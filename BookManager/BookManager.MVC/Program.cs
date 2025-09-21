namespace BookManager.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient("BookApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7280/api/");
            });

            builder.Services.AddHttpClient<BookManager.MVC.Services.BookService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Books/Error");
              
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Books}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
