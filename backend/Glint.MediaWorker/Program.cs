using Glint.MediaWorker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<MediaWorker>();

var host = builder.Build();
await host.RunAsync();
