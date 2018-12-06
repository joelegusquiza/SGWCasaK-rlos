using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserCreatedId { get; set; }
        public int UserModifiedId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; } = true;
    }
}
