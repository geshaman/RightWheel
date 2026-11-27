using RightWheel.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RightWheel.Application.DTO.Request
{
    public class OrderRequest
    {
        [Required]
        public string ClientPhoneNumber { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required, MinLength(1)]
        public List<OrderItemRequest> Items {  get; set; }

        public string? VIN { get; set; }

    }
}
