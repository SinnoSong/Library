//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//// 通过路由模板中URL指定api版本
//namespace Library.API.Controllers.V1
//{
//    [Route("api/v{version:apiVersion}/students")]
//    [ApiController]
//    [ApiVersion("1.0")]
//    public class StudentController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult<string> Get() => "Result from V1";
//    }
//}

//namespace Library.API.Controllers.V2
//{
//    [Route("api/v{version:apiVersion}/students")]
//    [ApiController]
//    [ApiVersion("2.0")]
//    public class StudentController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult<string> Get() => "Result from V2";
//    }
//}