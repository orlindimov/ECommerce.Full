﻿
using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Core.Models
{
    public class Product:BaseEntity
    {
      
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }


        public int? CategoryId { get; set; }


        public Category Category { get; set; }


    }
}
