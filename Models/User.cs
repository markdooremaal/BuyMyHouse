namespace Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public double Income { get; set; }
    public virtual Mortgage? Mortgage { get; set; }
}