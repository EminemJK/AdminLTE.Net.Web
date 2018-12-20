using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow.Models;

namespace AdminLTE.Models.Entity
{
    [Table("T_MISNews")]
    public class MisNewsInfo : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string Tags { get; set; }
        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int AdminId { get; set; }

    }
}
