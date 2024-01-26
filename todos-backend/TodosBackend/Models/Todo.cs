using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodosBackend.Models
{
    [Table("todos")]
    public class Todo
    {
        [Column("_id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("completed")]
        public bool Completed { get; set; }
    }
}