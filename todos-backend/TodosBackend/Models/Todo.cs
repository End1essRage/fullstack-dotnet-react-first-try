using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodosBackend.Models
{
    [Table("todos")]
    public class Todo : BaseModel
    {
        [Column("title")]
        public string Title { get; set; }
        [Column("completed")]
        public bool Completed { get; set; }
        [Column("user_id")]
        [ForeignKey("user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}