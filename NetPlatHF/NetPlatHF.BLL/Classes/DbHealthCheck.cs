using Microsoft.Extensions.Diagnostics.HealthChecks;
using NetPlatHF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlatHF.BLL.Classes;


public class DbHealthCheck : IHealthCheck
{

    private readonly AppDbContext _ctx;


    public DbHealthCheck(AppDbContext context)
    {
        _ctx = context;
    }


    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        bool dbAvailable = _ctx.Database.CanConnect();

        // tovabbi adabazis eleres tesztek

        if (dbAvailable)
        {
            return Task.FromResult(HealthCheckResult.Healthy("DB Healthy"));
        }

        return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "DB Unhealthy"));
    }
}
