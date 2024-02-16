namespace Identity.Microservice;

public class IdentityMicroserviceDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UsersCollection { get; set; } = null!;

}
