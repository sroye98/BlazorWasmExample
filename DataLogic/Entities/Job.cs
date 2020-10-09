using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLogic.Entities
{
    public class Job
    {
        public Job()
        {
        }

        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string JobId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string JobName { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Affiliation { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Modality { get; set; } // Discipline

        [Column(TypeName = "nvarchar(1024)")]
        public string SubModality { get; set; } // Speciality

        [Column(TypeName = "nvarchar(5)")]
        public string HotJob { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string JobType { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string ContractLength { get; set; }

        public DateTime? StartDate { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Facility { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public string State { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string ZipCode { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string BedSize { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string TraumaLevel { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string InterviewDates { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string StateLicenseDetails { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Discipline { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string Specialty { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string Shift { get; set; }

        public int? Positions { get; set; }

        public int? Submissions { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string RateType { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string OvertimeType { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? HolidayRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? OvertimeRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? CallbackRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? CallbackMinimum { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? FacilityRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? FacilityHourlyRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ChargeRateDifferential { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ShiftRateDifferential { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? OnCallRate { get; set; }

        public string OnCallRateType { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? FromRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ToRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? CallbackFromRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? CallbackToRate { get; set; }

        public int? PatientContactHours { get; set; }

        public int? GuaranteedHoursByWeek { get; set; }

        public int? GuaranteedHoursByDay { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ShiftOvertimeRateBase { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ShiftOvertimeRateFromRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ShiftOvertimeRateToRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ShiftOvertimeRateExtended { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ShiftOvertimeRateExtendedFromRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? ShiftOvertimeRateExtendedToRate { get; set; }

        public int? OvertimeBaseStartHour { get; set; }

        public int? OvertimeBaseEndHour { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? AgencyFee { get; set; }

        public string FeeType { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? CompletionBonus { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? HolidayBonus { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? OverCasesBonus { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? RelocationBonus { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? RetainerFeeBonus { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? SignOnBonus { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? WeekendBonus { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? AirfareExpense { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? HousingExpense { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? OtherExpense { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? PerDiemExpense { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? RentalCarExpense { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string ClientName { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string AdditionalInformation { get; set; }

        public int? MinimumExperienceRequired { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PositionUrgency { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string CertificationRequirements { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string UnitDescription { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Slug { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? Modified { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Source { get; set; }
    }
}
