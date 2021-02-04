using GraphQL.Types;
using Library.API.Entities;

namespace Library.API.GraphQLSchema
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Title);
            Field(x => x.Description);
            Field(x => x.Pages);
        }
    }
}