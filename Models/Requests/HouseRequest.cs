namespace Models.Requests;

public class HouseRequest
{
    public string Title { get; set; }
    public double Price { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}