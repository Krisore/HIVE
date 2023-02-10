﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIVE.Shared.Model
{
    public class Reference
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private List<Document> Documents { get; set; } = new List<Document>();
    }
}