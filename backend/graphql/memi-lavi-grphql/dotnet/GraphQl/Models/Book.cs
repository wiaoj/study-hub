namespace GraphQl.Models;
public class Book {
    public Int32 Id { get; set; }
    public String Title { get; set; }
    public String Author { get; set; }
    public DateTime Published { get; set; }
    public Int32 Pages { get; set; }
}