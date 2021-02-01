using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Configs.Filters
{
    public class CheckAuthorExistFilterAttribute : ActionFilterAttribute
    {
        public CheckAuthorExistFilterAttribute(IRepositoryWrapper repositoryWrapper)
        {
            _authorRepository = repositoryWrapper.Author;
        }

        private readonly IAuthorRepository _authorRepository;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authorIdParameter = context.ActionArguments.Single(m => m.Key == "authorId");
            Guid authorId = (Guid)authorIdParameter.Value;
            var isExist = await _authorRepository.IsExistAsync(authorId);
            if (!isExist)
            {
                context.Result = new NotFoundResult();
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}