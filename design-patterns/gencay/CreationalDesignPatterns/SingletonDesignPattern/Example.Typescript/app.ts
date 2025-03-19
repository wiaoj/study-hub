class Database {
    private static database: Database;

    private constructor() {
        console.log('Database instance created');
    }


    public static get Instance(): Database {
        if (!this.database) {
            this.database = new Database();
        }
        return this.database;
    }

    private count: number = 0;
    public connection(): void {
        this.count++;
        console.log(`Database connection count: ${this.count}`);
     }
}

let db1 = Database.Instance;
db1.connection();
let db2 = Database.Instance;
db2.connection();
let db3 = Database.Instance;
db3.connection();
let db4 = Database.Instance;
db4.connection();

/*
Database instance created
Database connection count: 1
Database connection count: 2
Database connection count: 3
Database connection count: 4
*/