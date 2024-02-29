using DesignPatterns.Structural.Bridge.RealWorld;

Customers customers = new() {
    Data = new CustomersData("Chicago")
};

customers.Show();
customers.Next();
customers.Show();
customers.Next();
customers.Show();
customers.Add("Henry Velasquez");
customers.ShowAll();