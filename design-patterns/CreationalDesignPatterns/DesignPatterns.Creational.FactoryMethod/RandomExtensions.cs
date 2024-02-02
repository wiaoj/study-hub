namespace DesignPatterns.FactoryMethod;
public static class RandomExtensions {

    private static readonly String[] firstNames = [
        "Ali",
        "Ayse",
        "Bahar",
        "Bekir",
        "Bulent",
        "Can",
        "Cem",
        "Demet",
        "Elif",
        "Eylem",
        "Faruk",
        "Fatma",
        "Ganime",
        "Gulsum",
        "Haydar",
        "Halil",
        "Ismail",
        "Jale",
        "Kemal",
        "Leman",
        "Mehmet",
        "Mihrimah",
        "Murat",
        "Mustafa",
        "Nedim",
        "Nilufer",
        "Selim",
        "Selman",
        "Sevda",
        "Suleyman",
        "Tarik",
        "Teoman",
        "Turgut",
        "Yeliz",
        "Zuhal"];

    private static readonly String[] departments = ["Production", "Sales", "Marketing", "Engineering"];

    public static Int32 CreateEmployeeId(this Random random) {
        return random.Next(1, 100_000);
    }
    public static String CreateEmployeeName(this Random random) { 
        return firstNames[random.Next(35)];
    }

    public static String CreateEmployeeDepartment(this Random random) { 
        return departments[random.Next(4)];
    }

    public static Int32 CreateEmployeeYear(this Random random) {
        return random.Next(1, 20);
    }
}