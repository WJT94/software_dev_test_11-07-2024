using NUnit.Framework;

public class LibraryTestHelper
{
    public static Library LibraryInit()
    {
        Library lib = new Library();
        string isbn = "testISBN";
        string title = "testTitle";
        string author = "testAuthor";
        Book book = new Book(isbn, title, author);
        lib.AddBook(book);
        return lib;
    }

    public static Library LibraryJson()
    {
        Library lib = new Library();
        lib.LoadBooksFromJson();
        return lib;
    }
}

[TestFixture]
public class LibraryTests
{

    [Test]
    public void AddBook_IncreasesTotalBooks()
    {
        Library lib = LibraryTestHelper.LibraryInit();
        int oldCount = lib.GetTotalBooks();
        Book book = new Book("newISBN", "newTitle", "newAuthor");
        lib.AddBook(book);
        Assert.That(oldCount + 1, Is.EqualTo(lib.GetTotalBooks()), "Adding a book does not increase Library's total books");
    }

    [Test]
    public void FindBookByISBN_ReturnsCorrectBook()
    {
        Library lib = LibraryTestHelper.LibraryInit();
        string isbn = "testISBN";
        Book foundBook = lib.FindBookByISBN("testISBN");
        Assert.That(isbn, Is.EqualTo(foundBook.ISBN), "FindBookByISBN does not return the correct book");
    }

    [Test]
    public void FindBooksByAuthor_ReturnsCorrectBooks()
    {
        Library lib = LibraryTestHelper.LibraryJson();
        string author = "George Orwell";
        List<Book> foundBooks = lib.FindBooksByAuthor(author);
        var allHaveAuthor = foundBooks.All(book => book.Author == author);
        Assert.That(allHaveAuthor, Is.True);
    }
}

[TestFixture]
public class LibraryIntegrationTests
{
    [Test]
    public void AddMultipleBooks_FindByAuthor_ReturnsCorrectBooks()
    {
        Library lib = LibraryTestHelper.LibraryJson();
        List<Book> newBooks = [
            new Book("074347712X", "Hamlet", "William Shakespeare"),
            new Book("0743484878", "Henry V", "William Shakespeare"),
            new Book("0743477103", "Macbeth", "William Shakespeare")
        ];
        
        foreach( Book book in newBooks) { lib.AddBook(book); }

        string author = "William Shakespeare";
        List<Book> foundBooks = lib.FindBooksByAuthor(author);
        var allHaveAuthor = foundBooks.All(book => book.Author == author);
        Assert.That(allHaveAuthor, Is.True);
    }

    [Test]
    public void AddBook_FindByISBN_ReturnsCorrectBook()
    {
        Library lib = LibraryTestHelper.LibraryJson();
        string isbn = "9780743273565";
        Book foundBook = lib.FindBookByISBN(isbn);
        var allHaveAuthor = foundBook.ISBN == isbn;
        Assert.That(foundBook.ISBN, Is.EqualTo(isbn));
    }
}

[TestFixture]
public class LibrarySystemE2ETests
{
    [Test]
    public void CreateLibrary_AddBooks_SearchAndRetrieveBooks()
    {
        // Create two new books and add them to the library
        Library library = new Library();
        Book book1 = new Book("123-4567890123", "Test Title 1", "Author One");
        Book book2 = new Book("987-6543210987", "Test Title 2", "Author Two");
        library.AddBook(book1);
        library.AddBook(book2);

        // Find book
        Book foundBook1 = library.FindBookByISBN("123-4567890123");
        List<Book> booksByAuthorTwo = library.FindBooksByAuthor("Author Two");

        // Assert
        Assert.That(foundBook1, Is.Not.Null);
        Assert.That(foundBook1.Title, Is.EqualTo("Test Title 1"));
        Assert.That(booksByAuthorTwo, Has.Count.EqualTo(1));
        Assert.That(booksByAuthorTwo[0].Title, Is.EqualTo("Test Title 2"));
    }

    [Test]
    public void CreateLibrary_AddDuplicateBooks_EnsureNoDuplicates()
    {
        // Create library and try adding duplicate books
        Library library = new Library();
        Book book1 = new Book("123-4567890123", "Test Title 1", "Author One");
        Book bookDuplicate = new Book("123-4567890123", "Test Title 1", "Author One");
        library.AddBook(book1);
        library.AddBook(bookDuplicate);

        // Get total number of books
        int totalBooks = library.GetTotalBooks();

        // Assert that the duplicate book isn't counted
        Assert.That(totalBooks, Is.EqualTo(1));
    }
}