using DesignPatterns.Creational.Prototype;

/*
 Problem
 */
Account normalAccount = new("1", 1000, 1000, true, new Customer("Ali"), true, true, true);
Account negativeAccount = new("2", -500, 1000, false, new Customer("Zeynep"), true, true, false);
Account frozenAccount = new("3", -1000, 1000, false, new Customer("Metin"), false, false, false);
