using System.Collections;

namespace EventPlus.Core.Models;

public class Response
{
    public ICollection<string> Errors { get; set; } = [];
}

public class Response<T>(T data) : Response
{
    public T Data { get; set; } = data;
}

public class CollectionResponse<T>(T data) : Response<T>(data) where T : ICollection
{
    public int Count { get; set; } = data.Count;
}