using System.ComponentModel.DataAnnotations;

namespace _20220614_TEST.Models
{
    public class tblActiveItem
    {
        /// <summary>
        /// 項目ID
        /// </summary>
        [Key]
        public int cItemID { get; set; }
        /// <summary>
        /// 活動項目名稱
        /// </summary>
        [Required]
        public string cItemName { get; set; }
        /// <summary>
        /// 活動時間
        /// </summary>
        [Required]
        public string cActiveDt { get; set; }

    }
}
