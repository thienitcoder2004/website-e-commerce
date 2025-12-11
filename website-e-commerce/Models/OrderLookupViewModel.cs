namespace website_e_commerce.Models.ViewModels
{
    public class OrderLookupViewModel
    {
        public string OrderCode { get; set; }      
        public string PhoneOrEmail { get; set; }
        public Order? Order { get; set; }
        public string? Message { get; set; }
    }

}
