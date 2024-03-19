namespace Pipeline;
public sealed class ConvertToCharArrayHandler : IHandler<String, Char[]> {
    public Char[] Process(String input) {
        Char[] characters = input.ToCharArray();
        String outputString = String.Join(",", characters);

        Logger.Information(
            "Current handler: {0}, input is {1} of type {2}, output is {3} of type {4}",
            GetType(), input, typeof(String), outputString, typeof(Char[])
        );

        return characters;
    }
}