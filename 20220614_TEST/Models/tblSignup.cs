using System.ComponentModel.DataAnnotations;

namespace _20220614_TEST.Models
{
    public class tblSignup
    {
        [Key]
        [StringLength(10)]
        public string cMobile { get; set; }//,varchar(10)，手機 ,PK
        [StringLength(20)]
        [Required]
        public string cName { get; set; }//, nvarchar(20)，姓名
        [StringLength(50)]
        [Required]
        public string cEmail { get; set; }//, nvarchar(50)，Email
        [Required]
        public DateTime cCreateDT { get; set; }//, datetime, 報名時間(新增當下)

    }
}
