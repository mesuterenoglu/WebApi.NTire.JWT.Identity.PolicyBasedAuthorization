

using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Abstract
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

    }
}
