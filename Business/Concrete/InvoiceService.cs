using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository rep;

        public InvoiceService(IInvoiceRepository _rep)
        {
            rep = _rep;
        }

        

        public InvoiceEntity GetById(int id)
        {
            return rep.GetById(id);
        }

        public List<InvoiceEntity> GetList(int id)
        {
            return rep.GetList(id);
        }

        public InvoiceEntity PaymentInvoice(int id)
        {
            return rep.PaymentInvoice(id);
        }

       
    }
}
