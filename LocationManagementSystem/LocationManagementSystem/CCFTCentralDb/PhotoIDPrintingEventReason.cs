namespace LocationManagementSystem.CCFTCentralDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhotoIDPrintingEventReason
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ForceReasons { get; set; }

        public byte[] Reasons { get; set; }
    }
}
