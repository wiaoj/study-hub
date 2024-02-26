namespace DesingPatterns.Structural.Proxy.RealWorld;
public class SharedFolderProxy : ISharedFolder {
    private ISharedFolder folder;
    private readonly Employee employee;

    public SharedFolderProxy(Employee emp) {
        this.employee = emp;
    }

    public void PerformRWOperations() {
        if(this.employee.Role.ToUpper() is "CEO" or "MANAGER") {
            this.folder = new SharedFolder();
            Console.WriteLine("Shared Folder Proxy makes call to the RealFolder 'PerformRWOperations method'");
            this.folder.PerformRWOperations();
        }
        else {
            Console.WriteLine("Shared Folder proxy says 'You don't have permission to access this folder'");
        }
    }
}