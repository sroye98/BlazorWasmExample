using System;
using DataLogic.Interfaces;

namespace DataLogic.Entities
{
    public class Application : IEntity
    {
        public Application()
        {
        }

        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        //public Guid JobId { get; set; }

        //public Job Job { get; set; }
    }
}
