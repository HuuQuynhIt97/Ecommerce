﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.Data
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        //public DateTime CreatedAt => Id.CreationTime;
    }
}
