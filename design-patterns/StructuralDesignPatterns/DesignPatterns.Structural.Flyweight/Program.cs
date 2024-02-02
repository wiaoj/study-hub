using DesignPatterns.Structural.Flyweight;

IFactory bookFactory = new BookFactory();
// Page 1
Line line1 = bookFactory.CreateLine(10);
Character c1 = bookFactory.CreateCharacter('t', true);
line1.Add(c1);
Character c2 = bookFactory.CreateCharacter('h', false);
line1.Add(c2);
Character c3 = bookFactory.CreateCharacter('i', false);
line1.Add(c3);
Character c4 = bookFactory.CreateCharacter('s', false);
line1.Add(c4);
Character c5 = bookFactory.CreateCharacter(' ', false);
line1.Add(c5);
Character c6 = bookFactory.CreateCharacter('b', false);
line1.Add(c6);
Character c7 = bookFactory.CreateCharacter('o', false);
line1.Add(c7);
Character c8 = bookFactory.CreateCharacter('o', false);
line1.Add(c8);
Character c9 = bookFactory.CreateCharacter('k', false);
line1.Add(c9);
line1.AddEndOfLine();

Page page1 = bookFactory.CreatePage(1, 20);
page1.Add(line1);

Book book = bookFactory.CreateBook("Thinking Design Patterns", 349);
book.AddPage(page1);
Console.WriteLine(book);
