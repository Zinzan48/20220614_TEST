using System.ComponentModel.DataAnnotations;

namespace _20220614_TEST.Models
{
    public class Sign
    {
        public int cItemID { get; set; }
        [Display(Name = "活動名稱")]
        public string cItemName { get; set; }
        [Display(Name = "報名人數")]
        public int? cItemNum { get; set; }
    }
    public class SignDetail
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "手機號碼")]
        public string cMobile { get; set; }//,varchar(10)，手機 ,PK
        [StringLength(20)]
        [Required]
        [Display(Name = "姓名")]
        public string cName { get; set; }//, nvarchar(20)，姓名
        [StringLength(50)]
        [Required]
        [Display(Name = "Email")]
        public string cEmail { get; set; }//, nvarchar(50)，Email
        [Required]
        [Display(Name = "報名時間")]
        public DateTime cCreateDT { get; set; }//, datetime, 報名時間(新增當下)
        /// <summary>
        /// 項目ID
        /// </summary>
        [Display(Name = "活動項目")]
        public int cItemID { get; set; }
        [Display(Name = "活動項目")]
        public string? cItemData { get; set; }
    }

}
