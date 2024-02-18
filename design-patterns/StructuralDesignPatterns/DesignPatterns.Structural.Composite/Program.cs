using DesignPatterns.Structural.Composite.BasicComposite;
using DesignPatterns.Structural.Composite.RealTimeExample;
using Directory = DesignPatterns.Structural.Composite.RealTimeExample.Directory;

//Test.CreateTest();

FileSystemItem myBook = new FileItem("MyBook.txt", 1209876);
FileSystemItem myVideo = new FileItem("MyVideo.mp4", 1243543245);
FileSystemItem myMusic = new FileItem("MyMusic.mp3", 3456789);
FileSystemItem myResume = new FileItem("MyResume.pdf", 2345);
FileSystemItem mySoftware = new FileItem("MySoftware.exe", 1123456789);
FileSystemItem myDocument = new FileItem("MyDocument.doc", 12435656);

//Create the Root Directory i.e. Composite Object 
Directory rootDirectory = new("RootDirectory");

//Add 2 More Folders i.e. two more composite objects  
Directory documents = new("Documents");
Directory audios = new("Audios");
//Add the above two folders under Root Directory
rootDirectory.AddComponent(documents);
rootDirectory.AddComponent(documents);

//Add files to Folder 1   
documents.AddComponent(myBook);
documents.AddComponent(myVideo);

//Create a Sub Folder1  
Directory pdfs = new("Pdfs");
//Add files under Sub Folder1  
pdfs.AddComponent(myMusic);
pdfs.AddComponent(myResume);
//Add Sub Folder1 under Folder 1
documents.AddComponent(pdfs);
//Add files to folder 2  
audios.AddComponent(mySoftware);
audios.AddComponent(myDocument);

Console.WriteLine("Composite Objects:");
Console.WriteLine($"Total size of: {rootDirectory}");
Console.WriteLine($"Total size of: {documents}");
Console.WriteLine($"Total size of: {audios}");
Console.WriteLine($"Total size of: {pdfs}");
Console.WriteLine("\nLeaf Objects:");
Console.WriteLine($"Total size of: {myVideo}");
Console.WriteLine($"Total size of: {myResume}");
Console.WriteLine($"Total size of: {myBook}");
Console.WriteLine($"Total size of: {myDocument}");
Console.Read();