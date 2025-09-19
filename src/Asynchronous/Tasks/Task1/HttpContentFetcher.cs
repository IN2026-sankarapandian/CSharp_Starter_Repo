using Asynchronous.Common;

namespace Asynchronous.Tasks.Task1;

/// <summary>
/// Provide methods to download content from internet.
/// </summary>
public class HttpContentFetcher
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpContentFetcher"/> class.
    /// </summary>
    public HttpContentFetcher()
    {
        this._httpClient = new HttpClient();
    }

    /// <summary>
    /// Downloads the content frm the specified URL.
    /// </summary>
    /// <param name="url">URL to fetch the content./</param>
    /// <returns>Content downloaded from the URL.</returns>
    public async Task<Result<string>> DownloadContentAsync(string url)
    {
        try
        {
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return Result<string>.Success(content);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
}
