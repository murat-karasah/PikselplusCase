using Entity;

namespace WebApp.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public int? UserId { get; set; }
        public UserEntity? user { get; set; }
    }
}
