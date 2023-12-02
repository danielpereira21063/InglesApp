using InglesApp.Data.Transaction;
using InglesApp.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.Urls.Add("https://localhost:7014");
    app.UseCors("Development");

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.Urls.Add("http://localhost:7015");
    app.UseCors("Production");
}

app.Use(async (context, next) =>
{
    await next.Invoke();
    var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
    await unitOfWork.CommitAsync();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//app.UseCors(options =>
//{
//    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//});




app.Run();