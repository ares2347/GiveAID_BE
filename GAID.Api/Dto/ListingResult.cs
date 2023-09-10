namespace GAID.Api.Dto;

public class ListingResult<T> where T: class
{
    public IQueryable<T> Data { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public int Total { get; set; }
}