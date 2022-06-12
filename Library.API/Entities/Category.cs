﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        #region ctor
        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion
    }
}
