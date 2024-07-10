using System.Text.Json.Serialization;

namespace Sidio.MailBluster.Models;

/// <summary>
/// The page metadata. It contains additional information about the response like number of total items, current
/// page no, items per page, total pages etc.
/// </summary>
public sealed record PageMetaData
{
    /// <summary>
    /// Gets the total records.
    /// </summary>
    [JsonPropertyName("total")]
    public required int Total { get; init; }

    /// <summary>
    /// Gets the previous page number.
    /// </summary>
    [JsonPropertyName("prevPageNo")]
    public int? PrevPageNo { get; init; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    [JsonPropertyName("pageNo")]
    public required int PageNo { get; init; }

    /// <summary>
    /// Gets the next page number.
    /// </summary>
    [JsonPropertyName("nextPageNo")]
    public int? NextPageNo { get; init; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    [JsonPropertyName("perPage")]
    public required int PerPage { get; init; }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    [JsonPropertyName("totalPage")]
    public required int TotalPage { get; init; }
}