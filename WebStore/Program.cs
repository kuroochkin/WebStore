using WebStore.Infrastructure.Conventions;
using WebStore.Services;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args); // ������� builder

var servises = builder.Services; // ��������� �������
servises.AddSingleton<IEmployeesData, InMemoryEmployeesData>(); // Singleton - ������ ��� InMemory!

servises.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
}
);// ��������� MVC

var app = builder.Build(); // �������� ����������


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // �������� ���� ������
}

app.UseStaticFiles(); // ���������� ����������� ����� (wwwroot)

app.UseMiddleware<TestConvention>(); // ��������� ���� ������������� ��

app.UseRouting(); // ��������� �������������


// app.MapDefaultControllerRoute(); // ��������� default-�������

app.MapControllerRoute( // ����������� �������
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // ������ ���������