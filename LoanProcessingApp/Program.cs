using LoanProcessingApp.Services;
using LoanProcessingApp.Validation;
using LoanProcessingApp.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoanProcessingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ILoanApplicationValidator, LoanApplicationValidator>();
            builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();

            builder.Services.AddSingleton<ILoanProcessingRepository, LoanProcessingRepository>();
            builder.Services.AddScoped<ILenderService, LenderService>();
            builder.Services.AddScoped<IBorrowerService, BorrowerService>();
            builder.Services.AddScoped<ILoanBookingService, LoanBookingService>();
            builder.Services.AddScoped<ILoanApprovalService, LoanApprovalService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
