using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Entities
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedAte { get; set; }
    }
}
