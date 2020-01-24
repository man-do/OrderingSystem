namespace OrderingSystem.Data.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_Capacity
    {
        [Key]
        public string UserId { get; set; }

        public int Capacity { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
