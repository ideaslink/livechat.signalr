using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using livechat.signalr.hubs;
using Microsoft.AspNetCore.ResponseCompression;
using livechat.signalr.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddResponseCompression(opts => {
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream"}
    );
});

var app = builder.Build();

// set up db (sqlite)
using (var scope = app.Services.CreateScope()){
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

}

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.MapBlazorHub();
app.MapHub<ChatHub>("/ChatHub");
app.MapFallbackToPage("/_Host");

app.Run();
