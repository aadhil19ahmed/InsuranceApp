using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACIA.Model
{
    public class PersonalClaimsList
    {
        public string QuoteId { get; set; }
        public object ClaimDate { get; set; }
        public object ClaimAmount { get; set; }
        public object ClaimType { get; set; }
        public string EmailId { get; set; }
    }

    public class PersonalConvictionsList
    {
        public string QuoteId { get; set; }
        public object ConvictionDate { get; set; }
        public object ConvictionType { get; set; }
        public object PointsIncurred { get; set; }
        public object FineIncurred { get; set; }
        public object BanLength { get; set; }
        public object BreathalysedReading { get; set; }
        public object IsBreathalysed { get; set; }
        public object IsAccident { get; set; }
        public string EmailId { get; set; }
    }

    public class SaveCarPersonalModel
    {
        public string UserId { get; set; }
        public string QuoteId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public object Suffix { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public object MobilePhone { get; set; }
        public string EmailId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Citizenship { get; set; }
        public string CitizenByBirth { get; set; }
        public object CitizenshipStartDate { get; set; }
        public string OccupationType { get; set; }
        public object Industry { get; set; }
        public object JobFunctions { get; set; }
        public bool IsNamedDriver { get; set; }
        public object LicenseType { get; set; }
        public object LicenseDuration { get; set; }
        public object LicenseNo { get; set; }
        public bool IsHaveAccidentsOrClaims { get; set; }
        public bool IsHaveConviction { get; set; }
        public bool IsHaveNominee { get; set; }
        public object NomineeTitle { get; set; }
        public object NomineeFirstName { get; set; }
        public object NomineeLastName { get; set; }
        public object NomineeDOB { get; set; }
        public object NomineeGender { get; set; }
        public object NomineeRelationship { get; set; }
        public object AppointeeName { get; set; }
        public bool IsHomeOwner { get; set; }
        public bool IsHaveChild { get; set; }
        public object NoOfCars { get; set; }
        public IList<PersonalClaimsList> PersonalClaimsList { get; set; }
        public IList<PersonalConvictionsList> PersonalConvictionsList { get; set; }

        public static implicit operator SaveCarPersonalModel(Task<SaveCarPersonalModel> v)
        {
            throw new NotImplementedException();
        }
    }
}
