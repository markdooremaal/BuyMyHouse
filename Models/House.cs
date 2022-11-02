namespace Models;

public class House
{
    public int HouseId { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string? Picture { get; set; }
}