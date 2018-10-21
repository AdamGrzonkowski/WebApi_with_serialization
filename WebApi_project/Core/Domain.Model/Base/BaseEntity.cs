using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Index { get; set; }
    }
}
