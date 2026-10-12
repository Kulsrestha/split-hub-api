using SplitHub.Api.ErrorHandling;

const string ApiTitle = "Split Hub API";
const string ApiVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = context.HttpContext.Request.Path;
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddOpenApi(ApiVersion, options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = ApiTitle;
        document.Info.Version = ApiVersion;
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Registered first so it handles failures from every later middleware.
app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
