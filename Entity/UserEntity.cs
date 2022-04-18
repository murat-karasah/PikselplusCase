using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public decimal Deposit { get; set; }
        public List<InvoiceEntity>? ınvoiceEntities { get; set; }

    }
}
