using WebApplication1.Model;
using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка middleware для статических файлов
app.UseStaticFiles(); // Обслуживание файлов из wwwroot

// Настройка Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API V1");
        c.RoutePrefix = "swagger"; // Swagger UI будет доступен по /swagger
    });
}

app.UseAuthorization();

app.MapControllers();

// Инициализация данных студентов
var studentsList = new List<Student>()
{
    new Student { Id = 1, FirstName = "Иван", LastName = "Прудников", Email = "ivan@example.com" },
    new Student { Id = 2, FirstName = "Кирилл", LastName = "Федорков", Email = "lsmatrelka1@gmail.com" },
    new Student { Id = 3, FirstName = "Ярослав", LastName = "Маркидонов", Email = "marki_don@mail.ru" },
    new Student { Id = 4, FirstName = "Михаил", LastName = "Звёздочкин", Email = "mz2004@example.com" },
    new Student { Id = 5, FirstName = "Семён", LastName = "Газизов", Email = "gazizov_semen@mail.ru" }
};
StudentController.InitializeData(studentsList);

// Минимальные API для работы со студентами
app.MapGet("/students", () =>
{
    return Results.Ok(studentsList);
});

app.MapGet("/students/{id}", (int id) =>
{
    var student = studentsList.FirstOrDefault(s => s.Id == id);
    return student != null ? Results.Ok(student) : Results.NotFound();
});

app.MapPost("/students", (Student student) =>
{
    student.Id = studentsList.Max(s => s.Id) + 1;
    studentsList.Add(student);
    return Results.Created($"/students/{student.Id}", student);
});

// Маршрут для начальной страницы
app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html"); // Перенаправление на index.html
    return Task.CompletedTask;
});

app.Run();