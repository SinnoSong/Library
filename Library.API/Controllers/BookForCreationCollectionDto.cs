﻿using Library.API.Models;
using System.Collections.Generic;

namespace Library.API.Controllers
{
    public class BookForCreationCollectionDto
    {
        public IEnumerable<BookForCreationDto> Books { get; set; }
    }
}