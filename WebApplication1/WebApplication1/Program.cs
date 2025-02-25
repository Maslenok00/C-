using WebApplication1.Model;
using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);

// ���������� ��������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ��������� middleware ��� ����������� ������
app.UseStaticFiles(); // ������������ ������ �� wwwroot

// ��������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API V1");
        c.RoutePrefix = "swagger"; // Swagger UI ����� �������� �� /swagger
    });
}

app.UseAuthorization();

app.MapControllers();

// ������������� ������ ���������
var studentsList = new List<Student>()
{
    new Student { Id = 1, FirstName = "����", LastName = "���������", Email = "ivan@example.com" },
    new Student { Id = 2, FirstName = "������", LastName = "��������", Email = "lsmatrelka1@gmail.com" },
    new Student { Id = 3, FirstName = "�������", LastName = "����������", Email = "marki_don@mail.ru" },
    new Student { Id = 4, FirstName = "������", LastName = "���������", Email = "mz2004@example.com" },
    new Student { Id = 5, FirstName = "����", LastName = "�������", Email = "gazizov_semen@mail.ru" }
};
StudentController.InitializeData(studentsList);

// ����������� API ��� ������ �� ����������
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

// ������� ��� ��������� ��������
app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html"); // ��������������� �� index.html
    return Task.CompletedTask;
});

app.Run();