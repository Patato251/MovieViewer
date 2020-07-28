using NUnit.Framework;
using Moq;
using GeekFix.Application.Common.Interfaces;
using GeekFix.Infrastructure.Repository;
using GeekFix.Domain.Entities.Detailed;
using System.IO;
using Newtonsoft.Json;
using GeekFix.Domain.Entities.Cache;
using FluentAssertions;
using System.Collections.Generic;

namespace GeekFix.Application.UnitTests.CachingTests
{
  [TestFixture]
  public class CacheTests
  {
    // Need to remove and make sure the cache database is empty before new test commences
    [SetUp]
    public void Init()
    {
      var testClass = new MovieCacheHandler(null);
      testClass.ClearMovieCache();
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
      var testMock = TmDbServiceMock(expectedResult, expectedId, 10, 5);
      var testClass = new MovieCacheHandler(testMock.Object);

      // Act
      CachedMovieDetails testCache = testClass.GetSingleMovie(expectedId); // Ref = 1

      // Assert
      testCache.Should().NotBeNull();
      testClass.CheckCacheCount().Should().Be(1);
      testClass.CheckReferenceCount(expectedId).Should().Be(1);
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
      var testMock = TmDbServiceMock(expectedResult, expectedId, 10, 5);
      var testClass = new MovieCacheHandler(testMock.Object, 10, 5, 20);

      // Act
      CachedMovieDetails testCache = testClass.GetSingleMovie(expectedId); // Ref = 1
      testCache = testClass.GetSingleMovie(expectedId); // Ref = 2

      // Assert
      testCache.Should().NotBeNull();
      testClass.CheckCacheCount().Should().Be(1);
      testClass.CheckReferenceCount(expectedId).Should().Be(2);
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
      var testMock = TmDbServiceMock(expectedResult, expectedId, 10, 5);
      var testClass = new MovieCacheHandler(testMock.Object, 10, 5, 20);

      // Act
      CachedMovieDetails testCache = testClass.GetSingleMovie(expectedId); // Ref = 1

      testClass.RemoveMovieFromCache(expectedId);

      // Assert
      testClass.CheckCacheCount().Should().Be(0);
    }

    // Check if multiple entries can be added to the cache
    [Test]
    public void TestCache_AddingMultipleEntriesIntoCache_AddsNewEntriesBasedOnId()
    {
      // Call add movie method into cache with altered Id's to have up to 5 unique entries

      // Expected Result
      MovieInfo expectedResult = FileConversion();

      // Setup mock for multiple files
      var testMock = TmDbMultiServiceMock(expectedResult, 10, 3);
      var testClass = new MovieCacheHandler(testMock.Object, 10, 3, 20);

      CachedMovieDetails testCache1 = testClass.GetSingleMovie(1);
      CachedMovieDetails testCache2 = testClass.GetSingleMovie(2);
      CachedMovieDetails testCache3 = testClass.GetSingleMovie(3);
      CachedMovieDetails testCache4 = testClass.GetSingleMovie(4);
      CachedMovieDetails testCache5 = testClass.GetSingleMovie(5);

      testClass.CheckCacheCount().Should().Be(5);
    }

    // Cache Clear all entries within the cache
    [Test]
    public void TestCache_ClearAllElements_RemovesAllElements()
    {
      // Arrange
      // Expected Result
      MovieInfo expectedResult = FileConversion();

      // Setup mock for multiple files
      var testMock = TmDbMultiServiceMock(expectedResult, 10, 3);
      var testClass = new MovieCacheHandler(testMock.Object, 10, 5, 10);
      
      // Act
      CachedMovieDetails testCache1 = testClass.GetSingleMovie(1);
      CachedMovieDetails testCache2 = testClass.GetSingleMovie(2);
      CachedMovieDetails testCache3 = testClass.GetSingleMovie(3);
      CachedMovieDetails testCache4 = testClass.GetSingleMovie(4);
      CachedMovieDetails testCache5 = testClass.GetSingleMovie(5);
      
      testClass.ClearMovieCache();
      // Assert
      testClass.CheckCacheCount().Should().Be(0);
    }

    // Cache prune all entries below a certain threshold
    [Test]
    public void TestCache_PruneElementsFromCache_RemovesElementsAfterMinKeepAndAddsNewElement()
    {
      // Expected Result
      MovieInfo expectedResult = FileConversion();

      // Setup mock for multiple files
      var testMock = TmDbMultiServiceMock(expectedResult, 10, 3);
      var testClass = new MovieCacheHandler(testMock.Object, 4, 3, 20);

      CachedMovieDetails testCache1 = testClass.GetSingleMovie(1);
      testCache1 = testClass.GetSingleMovie(1);
      testCache1 = testClass.GetSingleMovie(1);
      testCache1 = testClass.GetSingleMovie(1);

      CachedMovieDetails testCache2 = testClass.GetSingleMovie(2);
      testCache2 = testClass.GetSingleMovie(2);
      testCache2 = testClass.GetSingleMovie(2);

      CachedMovieDetails testCache3 = testClass.GetSingleMovie(3);
      testCache3 = testClass.GetSingleMovie(3);

      CachedMovieDetails testCache4 = testClass.GetSingleMovie(4);
      CachedMovieDetails testCache5 = testClass.GetSingleMovie(5);

      testClass.Should().NotBeNull();
      testClass.CheckCacheCount().Should().Be(4);
    }

    // Cache Edit max allowable cache size
    [Test]
    public void TestCache_AlterMaxCacheSize_ChangesMaxAcceptableSize()
    {
      int newCacheSize = 20;

      // Mock Setup
      MovieInfo randomResult = FileConversion();
      var testMock = TmDbServiceMock(randomResult, 187017, 10, 5);
      var testClass = new MovieCacheHandler(testMock.Object, 10, 5, 20); // Max size 20

      testClass.ChangeMaxCacheSize(newCacheSize);

      testClass.CheckMaxCacheSize().Should().Be(newCacheSize);
    }

    // Cache Edit number of elements to store after pruning of cache
    [Test]
    public void TestCache_AlterMinKeepCacheSize_ChangesElementsKeptSize()
    {
      int newKeepSize = 20;

      // Mock Setup
      MovieInfo randomResult = FileConversion();
      var testMock = TmDbServiceMock(randomResult, 187017, 10, 5);
      var testClass = new MovieCacheHandler(testMock.Object, 10, 5, 20); // Max size 20

      testClass.ChangeMinCacheSize(newKeepSize);

      testClass.CheckMinCacheSize().Should().Be(newKeepSize);
    }

    private MovieInfo FileConversion()
    {
      MovieInfo convertedFile = new MovieInfo();
      var path = Directory.GetCurrentDirectory();
      string convertedString = File.ReadAllText(Path.Combine(path, @"../../../CachingTests/movie187017.json"));
      JsonConvert.PopulateObject(convertedString, convertedFile);

      return convertedFile;
    }

    private Mock<ITmDbData> TmDbServiceMock(MovieInfo expectedResult, int expectedId, int max, int min)
    {
      var tmdbMock = new Mock<ITmDbData>();
      tmdbMock.Setup(m => m.CallApiMovie(expectedId)).Returns(expectedResult);

      return tmdbMock;
    }

    private Mock<ITmDbData> TmDbMultiServiceMock(MovieInfo expectedResult, int max, int min)
    {
      // Have five instances of the new desired result, and clone details over with an id change
      MovieInfo resultClone1 = new MovieInfo();
      MovieInfo resultClone2 = new MovieInfo();
      MovieInfo resultClone3 = new MovieInfo();
      MovieInfo resultClone4 = new MovieInfo();
      MovieInfo resultClone5 = new MovieInfo();
      resultClone1 = CloneJsonIdChange(expectedResult, resultClone1, 1);
      resultClone2 = CloneJsonIdChange(expectedResult, resultClone2, 2);
      resultClone3 = CloneJsonIdChange(expectedResult, resultClone3, 3);
      resultClone4 = CloneJsonIdChange(expectedResult, resultClone4, 4);
      resultClone5 = CloneJsonIdChange(expectedResult, resultClone5, 5);

      // Setup Mock Setup
      Queue<MovieInfo> multiExpectedResult = new Queue<MovieInfo>();
      multiExpectedResult.Enqueue(resultClone1);
      multiExpectedResult.Enqueue(resultClone2);
      multiExpectedResult.Enqueue(resultClone3);
      multiExpectedResult.Enqueue(resultClone4);
      multiExpectedResult.Enqueue(resultClone5);

      var tmdbMock = new Mock<ITmDbData>();
      tmdbMock.Setup(m => m.CallApiMovie(It.IsAny<int>())).Returns(multiExpectedResult.Dequeue); // WHy did removing these brackets work?

      return tmdbMock;
    }

    private static MovieInfo CloneJsonIdChange(MovieInfo expectedResult, MovieInfo newResult, int newId)
    {
      newResult.adult = expectedResult.adult;
      newResult.backdrop_path = expectedResult.backdrop_path;
      newResult.belongs_to_collection = expectedResult.belongs_to_collection;
      newResult.budget = expectedResult.budget;
      newResult.genres = expectedResult.genres;
      newResult.homepage = expectedResult.homepage;
      newResult.id = newId;
      newResult.imdb_id = expectedResult.imdb_id;
      newResult.original_language = expectedResult.original_language;
      newResult.original_title = expectedResult.original_title;
      newResult.overview = expectedResult.overview;
      newResult.popularity = expectedResult.popularity;
      newResult.poster_path = expectedResult.poster_path;
      newResult.production_companies = expectedResult.production_companies;
      newResult.production_countries = expectedResult.production_countries;
      newResult.release_date = expectedResult.release_date;
      newResult.revenue = expectedResult.revenue;
      newResult.runtime = expectedResult.runtime;
      newResult.spoken_languages = expectedResult.spoken_languages;
      newResult.status = expectedResult.status;
      newResult.tagline = expectedResult.tagline;
      newResult.title = expectedResult.title;
      newResult.video = expectedResult.video;
      newResult.vote_average = expectedResult.vote_average;
      newResult.vote_count = expectedResult.vote_count;

      return newResult;
    }
  }
}