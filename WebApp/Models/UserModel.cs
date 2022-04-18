using Entity;

namespace WebApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public decimal Deposit { get; set; }
        public List<InvoiceEntity>? ınvoiceEntities { get; set; }
    }
}
