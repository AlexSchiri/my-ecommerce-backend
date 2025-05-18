namespace ArticoliWebService.Dtos
{
    // DTO (Data Transfer Object)
public class CartDto
{
    public int Id { get; set; }
    public string codart { get; set; }
    public string UserId { get; set; }
    public bool acquistato { get; set; }
    public int qty { get; set; }
    public string ImageUrl { get; set; }
}
}