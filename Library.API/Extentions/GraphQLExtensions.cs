using GraphQL;
using GraphQL.Types;
using Library.API.GraphQLSchema;
using Microsoft.Extensions.DependencyInjection;

namespace Library.API.Extentions
{
    public static class GraphQLExtensions
    {
        public static void AddGraphQLSchemaAndTypes(this IServiceCollection services)
        {
            services.AddScoped<AuthorType>();
            services.AddSingleton<BookType>();
            services.AddScoped<LibraryQuery>();
            services.AddScoped<ISchema, LibrarySchema>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
        }
    }
}