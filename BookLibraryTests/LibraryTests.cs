using NUnit.Framework;

[TestFixture]
public class LibraryTests
{
    public void LibraryInit()
    {
        string isbn = "testISBN";
        string title = "testTitle";
        string author = "testAuthor";
        Book book = new Book(isbn, title, author);
        Library.AddBook(book);
    }

    [Test]
    public void AddBook_IncreasesTotalBooks()
    {
        int oldCount = Library.GetTotalBooks();
        LibraryInit()
        Assert.That(oldCount, Is.EqualTo(Library.GetTotalBooks()), "Adding a book does not increase Library's total books") 
    }

    [Test]
    public void FindBookByISBN_ReturnsCorrectBook()
    {
        string isbn = "testISBN";
        LibraryInit()
        foundBook = Library.FindBookByISBN("testISBN")
        Assert.That(isbn, Is.EqualTo(foundBook.ISBN), "FindBookByISBN does not return the correct book")
    }

    [Test]
    public void FindBooksByAuthor_ReturnsCorrectBooks()
    {
        // Implement test
    }
}

[TestFixture]
public class LibraryIntegrationTests
{
    [Test]
    public void AddMultipleBooks_FindByAuthor_ReturnsCorrectBooks()
    {
        // Implement test
    }

    [Test]
    public void AddBook_FindByISBN_ReturnsCorrectBook()
    {
        // Implement test
    }
}

using NUnit.Framework;

[TestFixture]
public class LibrarySystemE2ETests
{
    [Test]
    public void CreateLibrary_AddBooks_SearchAndRetrieveBooks()
    {
        // Implement test
    }

    [Test]
    public void CreateLibrary_AddDuplicateBooks_EnsureNoDuplicates()
    {
        // Implement test
    }
}