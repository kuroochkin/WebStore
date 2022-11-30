var builder = WebApplication.CreateBuilder(args); // ������� builder
var app = builder.Build(); // �������� ����������

var servises = builder.Services; // ��������� �������
servises.AddControllersWithViews(); // ��������� MVC


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // �������� ���� ������
}

app.UseRouting(); // ��������� �������������

app.MapGet("/", () => app.Configuration["HelloProject"]);

app.Run(); // ������ ���������