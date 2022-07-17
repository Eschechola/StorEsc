using System.Text;
using EscNet.IoC.Hashers;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StorEsc.IoC.Dependencies.Hashers;

public static class Argon2IdHasherDependencies
{
    public static IServiceCollection AddArgon2Id(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = int.Parse(configuration["Hash:Argon2Id:TimeCost"]),
            Threads = Environment.ProcessorCount,
            Salt = Encoding.UTF8.GetBytes(configuration["Hash:Argon2Id:Salt"]),
        };
        
        services.AddArgon2IdHasher(config);
        
        return services;
    }
}