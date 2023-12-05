﻿using System.Text.Json.Serialization;

namespace GreenHouse.HttpApiClient
{
    public class ValidationProblemDetails
    {
        [JsonPropertyName("title")] public string? Title { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("errors")] public IDictionary<string, string[]>? Errors { get; set; }
    }
}
