namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();


            builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            {
                //googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                //googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

                googleOptions.ClientId = "asdf";
                googleOptions.ClientSecret = "asdf";
            });



            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}