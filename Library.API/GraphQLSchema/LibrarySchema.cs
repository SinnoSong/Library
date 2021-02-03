using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace Library.API.GraphQLSchema
{
    public class LibrarySchema : Schema
    {
        public LibrarySchema(IServiceProvider services, LibraryQuery query) : base(services)
        {
            Query = query;
        }
    }
}