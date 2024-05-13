using App.Desafio.Blog.Domain.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class BlogChannelClientService : BackgroundService
{
    const string urlDefault = "http://web:5000/blogchannel";
    private readonly ILogger<BlogChannelClientService> _logger;
    private readonly AppSettings _appSettings;
    private HubConnection _hubConnection;

    public BlogChannelClientService(ILogger<BlogChannelClientService> logger, IOptions<AppSettings> appSettings)
    {   
        _logger = logger;
        _appSettings = appSettings.Value;

        var url = _appSettings?.BlogChannelSettings?.Endpoint ?? urlDefault;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url) 
            .Build();

        _hubConnection.On<string>("ReceiveMessage", (message) =>
        {
            _logger.LogInformation($"Received message: {message}");
        });
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_hubConnection.State == HubConnectionState.Disconnected)
                {
                    await _hubConnection.StartAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error connecting to channel: {ex.Message}");
                
                await Task.Delay(3000, stoppingToken);
            }
            
            await Task.Delay(5000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _hubConnection.DisposeAsync();
        await base.StopAsync(cancellationToken);
    }
}
