using System;

namespace Models
{
    public abstract class BaseEntity : Base
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? ModifyOn { get; set; }
    }
}
