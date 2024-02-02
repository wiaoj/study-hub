using System.Text;

namespace DesignPatterns.BuilderPattern.Method1;
public class EndpointBuilder {
    private readonly StringBuilder stringBuilder = new();
    private readonly StringBuilder stringBuilderParams = new();
    private const Char DEFAULT_DELIMITER = '/';
    public String BaseUrl { get; set; }

    public EndpointBuilder(String baseUrl) {
        this.BaseUrl = baseUrl;
    }

    public EndpointBuilder Append(String value) {
        // localhost/api/v1/user
        this.stringBuilder.Append(value);
        this.stringBuilder.Append(DEFAULT_DELIMITER);
        return this;
    }

    public EndpointBuilder AppendParam(String name, String value) {
        // localhost/api/v1/user/[id=5]
        this.stringBuilderParams.AppendFormat("{0}={1}&", name, value);
        return this;
    }

    public String Build() {
        // ?? => bunlar istenmez
        if(this.BaseUrl.EndsWith(DEFAULT_DELIMITER))
            this.stringBuilder.Insert(0, this.BaseUrl);
        else {
            this.stringBuilder.Insert(0, this.BaseUrl + DEFAULT_DELIMITER);
        }

        String url = this.stringBuilder.ToString().TrimEnd('&');

        if(this.stringBuilderParams.Length > default(Int32)) {
            String queryParams = this.stringBuilderParams.ToString().TrimEnd('&');
            url = this.stringBuilder.ToString().TrimEnd(DEFAULT_DELIMITER).TrimEnd('?');

            // localhost/api/v1/user?[id=5]
            url = $"{url}?{queryParams}";
        }

        return url.TrimEnd(DEFAULT_DELIMITER);
    }
}