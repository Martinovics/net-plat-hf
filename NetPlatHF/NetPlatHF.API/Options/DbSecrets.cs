namespace NetPlatHF.API.Options;



// ugyanolyan sémálya kell hogy legyen mint a json fájlnak, a nevek is meg kell egyezzenek
// regisztrálni kell a Services-be, hogy DI-ként lehessen használni
public class DbSecrets
{
    public string? BasicConnectionString { get; set; }
    public string? DefaultConnectionString { get; set; }
}
