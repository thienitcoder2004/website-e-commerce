namespace website_e_commerce.Models
{
    public class UserListVM
    {
        public string Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }

        public string Role { get; set; }

        public bool IsLocked { get; set; }

        public string RoleDisplay =>
            Role switch
            {
                SD.Role_Admin => "Quản trị viên",
                SD.Role_Employee => "Nhân viên bán hàng",
                SD.Role_StockManager => "Nhân viên kho hàng",
                SD.Role_Customer => "Khách hàng",
                _ => Role
            };
    }
}
