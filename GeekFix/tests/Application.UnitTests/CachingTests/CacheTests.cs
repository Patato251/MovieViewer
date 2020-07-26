using NUnit.Framework;
using Moq;
using GeekFix.Application.Common.Interfaces;
using GeekFix.Infrastructure.Repository;
using GeekFix.Domain.Entities.Detailed;
using System.IO;
using Newtonsoft.Json;
using GeekFix.Domain.Entities.Cache;
using FluentAssertions;

namespace GeekFix.Application.UnitTests.CachingTests
{
  [TestFixture]
  public class CacheTests
  {
    [SetUp]
    public void Init()
    {

    }
    // Cache Get a single entry from the cache storage
    [Test]
    public void TestCache_GetExistingElement_GetsElementWithId()
    {
      //Arrange
      // Expected Id
      var expectedId = 187017;

      // Expected Result
      MovieInfo expectedResult = FileConversion();

      // Mock Setup
      var testClass = TmDbServiceMock(expectedResult, expectedId);

      // Act
      CachedMovieDetails testCache = testClass.GetSingleMovie(expectedId); // Ref = 1
      testCache = testClass.GetSingleMovie(expectedId); // Ref = 2

      // Assert
      testCache.Should().NotBeNull();
      testClass.CheckCacheCount().Should().Be(1);
      testClass.CheckReferenceCount(expectedId).Should().Be(2);
    }

    // Cache Adding a single entry, check if entry has correct properties and values
    [Test]
    public void TestCache_AddElementToCache_AddsElement()
    {
      //Arrange
      // Expected Id
      var expectedId = 187017;

      // Expected Result
      MovieInfo expectedResult = FileConversion();

      // Mock Setup
      var testClass = TmDbServiceMock(expectedResult, expectedId);

      // Act
      CachedMovieDetails testCache = testClass.GetSingleMovie(expectedId); // Ref = 1

      // Assert
      testCache.Should().NotBeNull();
      testClass.CheckCacheCount().Should().Be(1);
      testClass.CheckReferenceCount(expectedId).Should().Be(1);
    }

    // Cache remove a single entry
    [Test]
    public void TestCache_RemoveElementFromCache_RemovesElement()
    {
      // Arrange
      // Expected Id
      var expectedId = 187017;

      // Expected Result
      MovieInfo expectedResult = FileConversion();

      // Mock Setup
      var testClass = TmDbServiceMock(expectedResult, expectedId);

      // Act
      CachedMovieDetails testCache = testClass.GetSingleMovie(expectedId); // Ref = 1

      testClass.RemoveMovieFromCache(expectedId);

      // Assert
      testClass.CheckCacheCount().Should().Be(0);
    }

    // Cache prune all entries below a certain threshold
    [Test]
    public void TestCache_PruneElementsFromCache_RemovesAllElementsAfterMinRequirements()
    {
      // Call add movie method into cache with altered Id's to have up to 5 unique entries

    }

    // Cache check for size status of cache if exceeds maximum cache size
    [Test]
    public void TestCache_CheckMaxSizeExceeded_ReturnsTrueWhenExceedingMax()
    {

    }
    // Cache Edit max allowable cache size
    [Test]
    public void TestCache_AlterMaxCacheSize_ChangesMaxAcceptableSize()
    {

    }
    // Cache Adding existing/duplicate element
    [Test]
    public void TestCache_AddExistingCacheElement_IncrementElementReferenceCount()
    {

    }
    // Cache Sort by popularity of elements
    [Test]
    public void TestCache_CheckSortOrder_ReturnsTrueWhenSortedDescendingOrder()
    {

    }


    private MovieInfo FileConversion()
    {
      var path = System.IO.Directory.GetCurrentDirectory();
      string convertedString = System.IO.File.ReadAllText(Path.Combine(path, @"../../../CachingTests/movie187017.json"));
      MovieInfo convertedFile = JsonConvert.DeserializeObject<MovieInfo>(convertedString);

      return convertedFile;
    }

    private MovieCacheHandler TmDbServiceMock(MovieInfo expectedResult, int expectedId)
    {
      var tmdbMock = new Mock<ITmDbData>();
      tmdbMock.Setup(m => m.CallApiMovie(expectedId)).Returns(expectedResult);
      var testClass = new MovieCacheHandler(tmdbMock.Object, 25, 5, 20);

      return testClass;
    }
  }
}