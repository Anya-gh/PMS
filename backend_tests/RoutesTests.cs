namespace backend_tests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Moq;
using PokeApiNet;

public class RoutesTests
{
    [Fact]
    public async void GetBox_ReturnsItemsFromDB() {
      // Arrange
      var pokemon_1 = new PlayerPokemon() { ID = 1 };
      var pokemon_2 = new PlayerPokemon() { ID = 2 };

      var data = new List<PlayerPokemon> {
        pokemon_1,
        pokemon_2
      }.AsQueryable();

      // Mocking the database context
      var mockSet = new Mock<DbSet<PlayerPokemon>>();
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.Provider).Returns(data.Provider);
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.Expression).Returns(data.Expression);
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.ElementType).Returns(data.ElementType);
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

      var mockContext = new Mock<PMSDb>(new DbContextOptions<PMSDb>());
      mockContext.Setup(m => m.PokemonItems).Returns(mockSet.Object);

      // Mocking GenerateWrapper to return a dummy PlayerPokemonWrapper
      // mocking because of request from external API that can't be served in this test without a HttpClient
      var mockGenerator = new Mock<IPokemonGenerator>();
      mockGenerator.Setup(mock => mock.GenerateWrapper(It.IsAny<PlayerPokemon>())).ReturnsAsync((PlayerPokemon p) => new PlayerPokemonWrapper(p, new List<PokemonMove>(), "", new List<string>(), ""));

      // Act
      var result = await RouteHandlers.GetBox(mockContext.Object, mockGenerator.Object);

      var okResult = result as Ok<List<PlayerPokemonWrapper>>;

      Assert.NotNull(okResult);

      var resultList = okResult.Value;

      Assert.NotNull(resultList);
      
      // Assert
      Assert.Equal(data.Count(), resultList.Count);
    }

    [Fact]
    public async void GetEncounter_WritesToDatabase() {
            // Arrange
      var listData = new List<PlayerPokemon>();
      var data = listData.AsQueryable();
      var pokemon_1 = new PlayerPokemon() { ID = 1 };
      // Mocking the database context
      var mockSet = new Mock<DbSet<PlayerPokemon>>();
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.Provider).Returns(data.Provider);
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.Expression).Returns(data.Expression);
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.ElementType).Returns(data.ElementType);
      mockSet.As<IQueryable<PlayerPokemon>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

      var mockContext = new Mock<PMSDb>(new DbContextOptions<PMSDb>());
      mockContext.Setup(m => m.PokemonItems).Returns(mockSet.Object);
      // why does this work ???
      mockContext.Setup(m => m.PokemonItems.Add(It.IsAny<PlayerPokemon>())).Callback<PlayerPokemon>(listData.Add);

      // Mocking GenerateWrapper to return a dummy PlayerPokemonWrapper
      // mocking because of request from external API that can't be served in this test without a HttpClient
      var mockGenerator = new Mock<IPokemonGenerator>();
      mockGenerator.Setup(mock => mock.GenerateWrapper(It.IsAny<PlayerPokemon>())).ReturnsAsync((PlayerPokemon p) => new PlayerPokemonWrapper(p, new List<PokemonMove>(), "", new List<string>(), ""));
      mockGenerator.Setup(mock => mock.GenerateEncounter(It.IsAny<Zone>())).ReturnsAsync(() => pokemon_1);

      // Act
      var result = await RouteHandlers.GetEncounter(1, mockContext.Object, mockGenerator.Object);
      Console.WriteLine(result.GetType());

      var okResult = result as Ok<PlayerPokemonWrapper>;

      Assert.NotNull(okResult);

      Assert.NotNull(okResult.Value);

      Assert.Equal(pokemon_1, okResult.Value.PokemonDetails);

      Assert.Equal(1, data.Count());
    }
}