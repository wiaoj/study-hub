namespace DesingPatterns.Structural.Proxy.RealWorld;
public class Employee {
    public String Username { get; set; }
    public String Password { get; set; }
    public String Role { get; set; }

    public Employee(String username, String password, String role) {
        this.Username = username;
        this.Password = password;
        this.Role = role;
    }
}