﻿using System.Text.Json.Serialization;

namespace ImGuiSharp.Generator.Data;

internal class BaseDefinition
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}