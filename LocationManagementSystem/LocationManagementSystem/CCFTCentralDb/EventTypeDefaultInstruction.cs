namespace LocationManagementSystem.CCFTCentralDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventTypeDefaultInstruction")]
    public partial class EventTypeDefaultInstruction
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventTypeID { get; set; }

        public int AlarmInstructionID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SiteItemID { get; set; }

        public virtual AlarmInstruction AlarmInstruction { get; set; }

        public virtual EventType EventType { get; set; }

        public virtual SiteItem SiteItem { get; set; }
    }
}
