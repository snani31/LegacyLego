using LegacyLego.Presentation.Orders;
using LegacyLego.Presentation.Payments;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseOrdersEndpoints();
app.UsePaymentEndpoints();

app.Run();