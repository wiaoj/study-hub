using System.Text;

namespace Pipeline;
public sealed class RemoveDigitsHandler : IHandler<String, String> {
    public String Process(String input) {
        StringBuilder output = new();
        Predicate<Char> isNotDigit = c => !Char.IsDigit(c);

        input.ToList().ForEach(c => {
            if(isNotDigit(c)) {
                output.Append(c);
            }
        });

        String outputString = output.ToString();

        Logger.Information(
            "Current handler: {0}, input is {1} of type {2}, output is {3} of type {4}",
            GetType(), input, typeof(String), outputString, typeof(String)
        );

        return outputString;
    }
}