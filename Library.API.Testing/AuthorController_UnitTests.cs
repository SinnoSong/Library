using AutoMapper;
using Library.API.Controllers;
using Library.API.Helper;
using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.API.Testing
{
    internal class AuthorController_UnitTests
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
    }
}