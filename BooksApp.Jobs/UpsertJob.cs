using BooksApp.Application.Books.Commands;
using BooksApp.Domain.DTOs;
using BooksApp.Jobs.Abstractions;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BooksApp.Jobs
{
    public sealed class UpsertJob(IMediator mediator, IHttpClientFactory httpClientFactory, IConfiguration configuration) : IUpsertJob
    {
        private readonly IMediator _mediator = mediator;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IConfiguration _configuration = configuration;

        private readonly JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration.GetSection("BookApi:UpsertJobUrl").Value, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var itemDtos = await DeserializeBookItems(response, cancellationToken);

                if (itemDtos is null)
                {
                    return;
                }

                var commands = itemDtos
                    .Select(item => new UpsertBookCommand(
                        item.Id, item.VolumeInfo.Title, item.VolumeInfo.SubTitle,
                        item.VolumeInfo.PageCount, item.VolumeInfo.MainCategory, item.VolumeInfo.Categories,
                        item.VolumeInfo.Dimensions?.Height, item.VolumeInfo.Dimensions?.Width, item.VolumeInfo.Dimensions?.Thickness,
                        item.VolumeInfo.AverageRating, item.VolumeInfo.RatingsCount, true))
                    .ToList();

                foreach (var command in commands)
                {
                    await _mediator.Send(command, cancellationToken);
                }
            }
        }

        private async Task<List<GoogleBookItemDto>?> DeserializeBookItems(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var serializedJson = await response.Content.ReadAsStringAsync(cancellationToken);

            using var json = JsonDocument.Parse(serializedJson);

            var items = json.RootElement.GetProperty("items");

            var itemDtos = JsonSerializer.Deserialize<List<GoogleBookItemDto>>(items.GetRawText(), jsonSerializerOptions);
            
            return itemDtos;
        }
    }
}
