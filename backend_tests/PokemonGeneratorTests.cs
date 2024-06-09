namespace backend_tests;
using backend;

public class PokemonGeneratorTests
{
    [Fact]
    public async void Test1()
    {
        PokemonGenerator generator = new PokemonGenerator();
        PlayerPokemonWrapper testPokemon = await generator.GenerateWrapper(await generator.Generate(1));
        Assert.Equal("bulbasaur", testPokemon.Name);
    }
}