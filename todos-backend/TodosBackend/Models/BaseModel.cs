﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TodosBackend.Models
{
    public class BaseModel
    {
        [Column("_id")]
        public int Id { get; set; }
    }
}
