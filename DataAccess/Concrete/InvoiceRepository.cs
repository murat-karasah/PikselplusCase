using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class InvoiceRepository : IInvoiceRepository
    {
        

        public InvoiceEntity GetById(int id)
        {
            using (var db = new PikselDbContext())
            {
                return db.InvoiceEntities.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<InvoiceEntity> GetList(int id)
        {
            using (var db = new PikselDbContext())
            {
                return db.InvoiceEntities.Where(x => x.UserId == id).ToList();
            }
        }

        public InvoiceEntity PaymentInvoice(int id)
        {
            using (var db = new PikselDbContext())
            {
                var invoice = GetById(id);
                invoice.Status = true;
                db.InvoiceEntities.Update(invoice);
                db.SaveChanges();
                return invoice;
            }
        }
    }
}
