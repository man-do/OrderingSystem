namespace OrderingSystem.Data.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dish")]
    public partial class Dish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dish()
        {
            Igredients = new HashSet<Igredient>();
            Orders = new HashSet<Order>();
        }

        public int DishId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Enabled { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public decimal Rating { get; set; }

        public bool IsVegan { get; set; }

        public bool IsPescatarian { get; set; }

        public bool IsVegetarian { get; set; }

        public bool HasPeanuts { get; set; }

        public bool HasSeafood { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Igredient> Igredients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
