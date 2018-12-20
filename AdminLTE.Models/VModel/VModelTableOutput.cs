﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models.VModel
{
    public class VModelTableOutput<T>
    {
        public List<T> data { get; set; }
        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public int draw { get; set; }

        public VModelTableOutput() { }

        public VModelTableOutput(List<T> data, int draw, int pageCount)
        {
            this.data = data;
            this.recordsFiltered = pageCount;
            this.recordsTotal = pageCount;
            this.draw = draw;
        }
    }
}
