//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//// 通过查询字符串调用不同版本API
//namespace Library.API.Controllers.V1
//{
//    [Route("api/person")]
//    [ApiController]
//    [ApiVersion("1.0")]
//    public class PersonController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult<string> Get() => "Result from V1";
//    }
//}

//namespace Library.API.Controllers.V2
//{
//    [Route("api/person")]
//    [ApiController]
//    [ApiVersion("2.0")]
//    public class PersonController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult<string> Get() => "Result from V2";
//    }
//}