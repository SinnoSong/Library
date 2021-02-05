using AutoMapper;
using Library.API.Configs;
using Library.API.Controllers;
using Library.API.Entities;
using Library.API.Helper;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Library.API.Testing
{
    public class AuthorController_UnitTests
    {
        private AuthorController _authorController;
        private Mock<IDistributedCache> _mockDistributedCache;
        private Mock<HashFactory> _mockHashFactory;
        private Mock<ILogger<AuthorController>> _mockILogger;
        private Mock<IMapper> _mockMapper;
        private Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private Mock<IUrlHelper> _mockUrlHelper;

        public AuthorController_UnitTests()
        {
            _mockDistributedCache = new Mock<IDistributedCache>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockHashFactory = new Mock<HashFactory>();
            _mockILogger = new Mock<ILogger<AuthorController>>();
            _mockMapper = new Mock<IMapper>();
            _authorController = new AuthorController(_mockRepositoryWrapper.Object,
                _mockMapper.Object,
                _mockILogger.Object,
                _mockDistributedCache.Object,
                _mockHashFactory.Object);
            _authorController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public async Task Test_GetAllAuthors()
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Author test 1",
                Email = "author@xxx.com",
                BirthPlace = "&#x5317;&#x4EAC;"
            };
            var authorDto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                BirthPlace = author.BirthPlace
            };
            var authorList = new List<Author> { author };
            var authorDtoList = new List<AuthorDto> { authorDto };
            var parameters = new AuthorResourceParameters();
            var authors = new PagedList<Author>(authorList,
                totalCount: authorList.Count,
                pageNumber: parameters.PageNumber,
                pageSize: parameters.PageSize
                );
            _mockRepositoryWrapper.Setup(m => m.Author.GetAllAsync(It.IsAny<AuthorResourceParameters>())).Returns(Task.FromResult(authors));
            _mockMapper.Setup(m => m.Map<IEnumerable<AuthorDto>>(It.IsAny<IEnumerable<Author>>())).Returns(authorDtoList);
            _mockUrlHelper.Setup(m => m.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("demo url");
            _authorController.Url = _mockUrlHelper.Object;
            // Act
            var actionResult = await _authorController.GetAuthorsAsync(parameters);
            //Assert
            ResourceCollection<AuthorDto> resourceCollection = actionResult.Value;
            Assert.True(1 == resourceCollection.Items.Count);
            Assert.Equal(authorDto, resourceCollection.Items[0]);
            Assert.True(_authorController.Response.Headers.ContainsKey("X-Pagination"));
        }
    }
}