using Microsoft.EntityFrameworkCore;

namespace WebSwagger.Models
{
    public class SimonSay
    {
        public string CommandTo { get; set; } = "";
        public string DoAction { get; set; } = "";
    }

    public class Customer
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
    }
}
