using NUnit.Framework;

[TestFixture]
public class BookTests
{
    [Test]
    public void BookConstructor_SetsPropertiesCorrectly()
    {
        // Implement test
        string isbn = "testISBN";
        string title = "testTitle";
        string author = "testAuthor";
        var result = new Book(isbn, title, author);

        Assert.That(result.ISBN, Is.EqualTo(isbn), "Book's isbn property is not set correctly");
        Assert.That(result.Title, Is.EqualTo(title), "Book's title property is not set correctly");
        Assert.That(result.Author, Is.EqualTo(author), "Book's author property is not set correctly");
    }
}