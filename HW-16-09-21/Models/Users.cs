using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_16_09_21.Models
{
    [Table("Users")]
    public class Users
    {
        public int Id { get; set; }
        [Column("First_Name")]
        public string FirstName { get; set; }
        [Column("Last_Name")]
        public string LastName { get; set; }
        [Column("Middle_Name")]
        public string MiddleName { get; set; }
        [Column("Birth_Date")]
        public DateTime BirthDate { get; set; }
        [Column("Created_At")]
        public DateTime CreatedAt { get; set; }

    }
}
