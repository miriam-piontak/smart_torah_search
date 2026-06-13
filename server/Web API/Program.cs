var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// שתי השורות שחסרו לך כדי שה-Swagger יעבוד:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BLL.ToraManager>();
// הוספת ה-CORS כדי שהריאקט יוכל לדבר עם השרת בהמשך
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // יוצר את ה-JSON של התיעוד
    app.UseSwaggerUI(); // יוצר את הממשק הוויזואלי (הדף הכחול)}
}
    app.UseHttpsRedirection();
// הפעלת ה-CORS
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
