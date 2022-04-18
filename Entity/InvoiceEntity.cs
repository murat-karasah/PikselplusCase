using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class InvoiceEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public int? UserId { get; set; }
        public UserEntity? user { get; set; }
    }
}
