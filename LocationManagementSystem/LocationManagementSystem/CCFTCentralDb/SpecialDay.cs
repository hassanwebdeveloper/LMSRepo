namespace LocationManagementSystem.CCFTCentralDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpecialDay")]
    public partial class SpecialDay
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CalendarID { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Day { get; set; }

        public int DayCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public virtual Calendar Calendar { get; set; }

        public virtual DayCategory DayCategory { get; set; }
    }
}
