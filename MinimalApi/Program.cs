using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapPost("/user", () => new { Name = "Gustavo Gonçalves", Age = 23 });

app.MapGet("/AddHeader", (HttpResponse response) =>
{
    response.Headers.Add("Teste", "Gustavo Goncalves");
    return new { Name = "Gustavo Gonçalves", Age = 23 };
});

app.MapPost("/saveProduct", (Product product) =>
{
    return product.Code + " - " + product.Name;
});

//api.app.com/users?datastart={date}&dateend={date}
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;

});

//api.app.com/user/{code}
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    return code;

});

app.MapGet("/getproductbyheader", (HttpRequest request) =>
{
    return request.Headers["product-code"].ToString();
});



app.Run();


public static class ProductRepository
{
    public static List<Product> Products { get; set;  }

    public static void Add(Product product)
    {
        if(Products == null)
        {
            Products = new List<Product>();
        }

        Products.Add(product);
    }

    public static Product GetBy(string code)
    {
        return Products.First(p => p.Code == code);
    }
}
public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}
