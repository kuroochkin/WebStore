var builder = WebApplication.CreateBuilder(args); // ������� builder

var servises = builder.Services; // ��������� �������
servises.AddControllersWithViews();// ��������� MVC

var app = builder.Build(); // �������� ����������


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // �������� ���� ������
}

app.UseRouting(); // ��������� �������������



app.MapDefaultControllerRoute();

app.Run(); // ������ ���������