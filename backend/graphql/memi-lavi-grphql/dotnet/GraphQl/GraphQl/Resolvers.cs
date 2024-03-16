using GraphQl.Models;

namespace GraphQl.GraphQl;
public class Resolvers {
    public String GetFormattedDate([Parent] Book book) {
        return book.Published.ToShortDateString();
    }

    public String GetTitle([Parent] Book book) {
        return $"[{book.Title.ToUpper()}]";
    }
}