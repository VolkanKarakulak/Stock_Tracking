﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
