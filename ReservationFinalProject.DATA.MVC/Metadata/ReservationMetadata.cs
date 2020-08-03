using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReservationFinalProject.DATA.EF
{

    #region Location
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }
    
    public class LocationMetadata
    {
        [Display(Name = "Location")]
        [Required(ErrorMessage = "*** Location Name is Required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 Characters ***")]
        public string LocationName { get; set; }
    
        [Required(ErrorMessage = "*** Address is Required ***")]
        [StringLength(100, ErrorMessage = "*** Max 100 Characters ***")]
        public string Address { get; set; }
    
        [Required(ErrorMessage = "*** City is Required ***")]
        [StringLength(100, ErrorMessage = "*** Max 100 Characters ***")]
        public string City { get; set; }
    
        [Required(ErrorMessage = "*** State is Required ***")]
        [StringLength(2, ErrorMessage = "*** Max 2 Characters ***")]
        public string State { get; set; }
    
        [Required(ErrorMessage = "*** Zipcode is Required ***")]
        [StringLength(5, ErrorMessage = "*** Max 5 Characters ***")]
        public string Zip { get; set; }
    
        [Display(Name = "Reservation Limit")]
        [Required(ErrorMessage = "*** Reservation Limit is Required ***")]
        [Range(0, 255, ErrorMessage = "*** Must be between 0 and 255 ***")]
        public byte ReservationLimit { get; set; }
    }
    #endregion
    
    #region OwnerAssets
    [MetadataType(typeof(OwnerAssetMetadata))]
    public partial class OwnerAsset { }
    
    public class OwnerAssetMetadata
    {
        [Display(Name = "Child")]
        [Required(ErrorMessage = "*** Child Name is Required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 Characters ***")]
        public string AssetName { get; set; }
    
        [Display(Name = "Photo")]
        public string AssetPhoto { get; set; }
    
        [Display(Name = "Notes")]
        [StringLength(300, ErrorMessage = "*** Max 300 Characters ***")]
        public string SpecialNotes { get; set; }
        public bool IsActive { get; set; }
    
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DateAdded { get; set; }
    }
    #endregion
    
    #region Reservations
    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation { }
    
    public class ReservationMetadata
    {
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "*** A date is required ***")]
        public System.DateTime ReservationDate { get; set; }
    }
    #endregion

    #region UserDetails
    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {
        [Display(Name = "Parent")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public class UserDetailMetadata
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*** First Name is Required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 Characters ***")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*** Last Name is Required ***")]
        [StringLength(50, ErrorMessage = "*** Max 50 Characters ***")]
        public string LastName { get; set; }
    }
    #endregion
}
