using System;
using DataLogic.Entities;

namespace DataLogic.Common
{
    public class AuditableEntity
    {
        public AuditableEntity()
        {
        }

        public DateTime Created { get; set; } = DateTime.Now;

        public Guid CreatedById { get; set; }

        public AppUser CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public Guid ModifiedById { get; set; }

        public AppUser ModifiedBy { get; set; }
    }
}
