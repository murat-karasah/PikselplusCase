using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IInvoiceService
    {
        List<InvoiceEntity> GetList(int id);
        InvoiceEntity GetById(int id);
        InvoiceEntity PaymentInvoice(int id);
 
    }
}
