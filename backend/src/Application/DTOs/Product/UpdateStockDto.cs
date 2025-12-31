namespace Application.DTOs.Product
{
    public class UpdateStockDto
    {
        public int Quantity { get; set; }
        public string Operation { get; set; } = "set"; // "set", "increase", "decrease"
        public string Notes { get; set; } = string.Empty;
    }
}