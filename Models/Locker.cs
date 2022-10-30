using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FACL_Locker_Room_API.Models
{
    public class Locker
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
