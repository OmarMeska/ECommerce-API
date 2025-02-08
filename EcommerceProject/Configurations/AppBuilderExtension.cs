namespace EcommerceProject.Configurations
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder AddGlobalErrorHandlerMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
