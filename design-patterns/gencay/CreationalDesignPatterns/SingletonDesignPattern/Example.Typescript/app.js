"use strict";
class Database {
    constructor() {
        this.count = 0;
        console.log('Database instance created');
    }
    static get Instance() {
        if (!this.database) {
            this.database = new Database();
        }
        return this.database;
    }
    connection() {
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
//# sourceMappingURL=app.js.map