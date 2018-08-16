using System;
namespace ACIA.Model
{
    public class CarAdditionalInfoModel
    {
        public string UserId { get; set; }
        public string QuoteId { get; set; }
        public bool AddBreakdownCoverage { get; set; }
        public bool AddCNGFitment { get; set; }
        public bool AddElectricalAccessories { get; set; }
        public bool AddNamedPassenger { get; set; }
        public bool AddNonElectricalAccessories { get; set; }
        public bool AddPaidDriverCover { get; set; }
        public bool AddPersonalAccidentCover { get; set; }
        public bool AddUnNamedPassenger { get; set; }
        public string AnotherVechile { get; set; }
        public string CarUsage { get; set; }
        public string CoverStartDate { get; set; }
        public string CoverType { get; set; }
        public string DayTimeParking { get; set; }
        public string ExcessPay { get; set; }
        public object HasExistingPolicyUs { get; set; }
        public bool HasNCB { get; set; }
        public object HasProtectedNCB { get; set; }      
        public object NeedToProtectNCB { get; set; }     
        public object ExistingCoverType { get; set; }    
        public string Mileage { get; set; }
        public string NCBDuration { get; set; }
        public string NightTimeParking { get; set; }
        public object NumberOfDrivers { get; set; }
        public object NumberOfNamedPassenger { get; set; }
        public object NumberofUnNamedPassenger { get; set; }   
        public string PaymentMethod { get; set; }
        public object PolicyEndDate { get; set; }   
        public object PolicyNumber { get; set; }     
        public object PolicyStartDate { get; set; }      
        public string QuoteType { get; set; }
        public object SumNamedPassenger { get; set; }       
        public object SumUnNamedPassenger { get; set; }       
        public object SumPaidDriver { get; set; }
        public object ValueCNGFitment { get; set; }
        public object ValueElectricalAccessories { get; set; }
        public object ValueNonElectricalAccessories { get; set; }
      
    }
}
