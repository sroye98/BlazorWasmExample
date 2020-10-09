using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLogic.Common;
using DataLogic.Interfaces;

namespace DataLogic.Entities
{
    public class Employee : AuditableEntity, IEntity
    {
        public Employee()
        {
        }

        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string AddressLine2 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        [MaxLength(2)]
        public string State { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [MaxLength(10)]
        public string Zip { get; set; }
    }
}
