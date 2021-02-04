using GraphQL;
using GraphQL.Types;
using Library.API.GraphQLSchema;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _librarySchema;
        private readonly IDocumentExecuter _documentExecuter;

        public GraphQLController(ISchema librarySchema, IDocumentExecuter documentExecuter)
        {
            _librarySchema = librarySchema;
            _documentExecuter = documentExecuter;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest query)
        {
            var result = await _documentExecuter.ExecuteAsync(options =>
            {
                options.Schema = _librarySchema;
                options.Query = query.Query;
            });
            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}