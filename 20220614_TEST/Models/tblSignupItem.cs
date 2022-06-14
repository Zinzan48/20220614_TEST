using System.ComponentModel.DataAnnotations;

namespace _20220614_TEST.Models
{
    public class tblSignupItem
    {
        [Required]
        public string cMobile { get; set; }
        [Required]
        public int cItemID { get; set; }
    }
}
