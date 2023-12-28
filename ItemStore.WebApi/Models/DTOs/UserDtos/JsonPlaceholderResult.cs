namespace ItemStore.WebApi.Models.DTOs.UserDtos;

public class JsonPlaceholderResult<T> where T : class
{
    public T? DataItem { get; set; }

    public List<T>? DataItems { get; set; }

    public bool IsSuccessful { get; set; }

    public string? ErrorMessage { get; set; }
}
