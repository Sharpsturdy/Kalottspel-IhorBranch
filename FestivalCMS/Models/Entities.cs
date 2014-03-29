using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FestivalCMS.Models
{
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("webpages_Membership")]
    public class Membership
    {
        public Membership()
        {
            Roles = new List<Role>();
            OAuthMemberships = new List<OAuthMembership>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        [StringLength(128)]
        public string ConfirmationToken { get; set; }
        public bool? IsConfirmed { get; set; }
        public DateTime? LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        [Required, StringLength(128)]
        public string Password { get; set; }
        public DateTime? PasswordChangedDate { get; set; }
        [Required, StringLength(128)]
        public string PasswordSalt { get; set; }
        [StringLength(128)]
        public string PasswordVerificationToken { get; set; }
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public ICollection<Role> Roles { get; set; }

        [ForeignKey("UserId")]
        public ICollection<OAuthMembership> OAuthMemberships { get; set; }
    }

    [Table("webpages_OAuthMembership")]
    public class OAuthMembership
    {
        [Key, Column(Order = 0), StringLength(30)]
        public string Provider { get; set; }

        [Key, Column(Order = 1), StringLength(100)]
        public string ProviderUserId { get; set; }

        public int UserId { get; set; }

        [Column("UserId"), InverseProperty("OAuthMemberships")]
        public Membership User { get; set; }
    }

    [Table("webpages_Roles")]
    public class Role
    {
        public Role()
        {
            Members = new List<Membership>();
        }

        [Key]
        public int RoleId { get; set; }
        [StringLength(256)]
        public string RoleName { get; set; }

        public ICollection<Membership> Members { get; set; }
    }

    public class Footer
    {
        public int ID { get; set; }
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Post address")]
        public string PostAddress { get; set; }
        [Display(Name = "Legal address")]
        public string LegalAddress { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Contact phone")]
        public string COntactPhone { get; set; }
        [Display(Name = "Cell phone")]
        public string CellPhone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Support Email")]
        public string SupportEmail { get; set; }
        [Display(Name = "GMap")]
        public string GoogleMapIFrameLink { get; set; }
        [Display(Name = "Facebook")]
        public string FacebookLink { get; set; }
        [Display(Name = "Ext. link")]
        public string ExternalLink { get; set; }
        [Display(Name = "Soc. link")]
        public string SocialLink { get; set; }
    }

    public class Metatag
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Category name")]
        [Required(ErrorMessage = "Please input category name")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int OrderNum { get; set; }
        [Display(Name = "Media Type")]
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public int ContentID { get; set; }
        public bool IsPartner { get; set; }
        public bool IsSponsor { get; set; }


        public virtual ICollection<Article> Articles { get; set; }
        public virtual MediaType MediaType { get; set; }
    }

    public class Article
    {
        public int ID { get; set; }
        [Display(Name = "Headline")]
        [Required(ErrorMessage = "Please input Headline")]
        public string Headline { get; set; }
        [Display(Name = "Ingress")]
        public string Ingress { get; set; }
        [Display(Name = "Text")]
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public int OrderNum { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [Display(Name = "External Link")]
        public string ExtLink { get; set; }
        [ForeignKey("MediaType")]
        [Display(Name = "Media Type")]
        public int MediaTypeID { get; set; }
        public int ContentID { get; set; }

        public virtual Category Category { get; set; }
        public virtual MediaType MediaType { get; set; }
    }

    public class MediaType
    {
        public int ID { get; set; }
        public string Type { get; set; }

        // public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<PhotoGallery> PhotoGalleries { get; set; }
        public virtual ICollection<VideoLink> VideoLinks { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }
    }

    //public class Media
    //{
    //    public int ID { get; set; }
    //    [ForeignKey("MediaType")]
    //    public int MediaTypeID { get; set; }

    //    public virtual ICollection<MediaType> MediaType { get; set; }

    //}

    public class Photo
    {
        public int ID { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public string FileName { get; set; }

        public virtual MediaType MediaType { get; set; }
    }

    public class VideoLink
    {
        public int ID { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public string Link { get; set; }
        public string Hosting { get; set; }

        public virtual MediaType MediaType { get; set; }
    }

    public class PhotoGallery
    {
        public int ID { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }

        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<GalleryPhoto> GalleryPhotos { get; set; }
    }

    public class GalleryPhoto
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        [ForeignKey("PhotoGallery")]
        public int PhotoGalleryID { get; set; }

        public virtual PhotoGallery PhotoGallery { get; set; }
    }

    public class Artist
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please input artist name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtLink { get; set; }
        public int OrderNum { get; set; }
        public bool isActive { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public int ContentID { get; set; }

        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<ArtistOnEvent> ArtistEvents { get; set; }
        public virtual ICollection<ArtistOnFestival> ArtistFestivals { get; set; }

    }

    public class Event
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please input event name")]
        public string Title { get; set; }
        public DateTime StartOn { get; set; }
        public int Duration { get; set; }
        public int OrderNum { get; set; }
        public bool isActive { get; set; }
        public string Stage { get; set; }
        public string TicketCode { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ExtLink { get; set; }
        [ForeignKey("Festival")]
        public int? FestivalID { get; set; }

        public virtual Festival Festival { get; set; }
        public virtual ICollection<ArtistOnEvent> EventArtists { get; set; }
    }

    public class Festival
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please input festival name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "DD.MM.YYYY")]
        public DateTime From { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "DD.MM.YYYY")]
        public DateTime Untill { get; set; }
        public int OrderNum { get; set; }
        public bool isActive { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }

    public class ArtistOnEvent
    {
        public int ID { get; set; }
        [ForeignKey("Artist")]
        public int ArtistID { get; set; }
        [ForeignKey("Event")]
        public int EventID { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Event Event { get; set; }
    }

    public class ArtistOnFestival
    {
        public int ID { get; set; }
        [ForeignKey("Artist")]
        public int ArtistID { get; set; }
        [ForeignKey("Festival")]
        public int FestivalID { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Festival Festival { get; set; }
    }

    public class Seminar
    {
        public int ID { get; set; }
        [Display(Name="Headline")]
        [Required(ErrorMessage="Please input headline")]
        public string Headline { get; set; }
        [Required(ErrorMessage="Please input date and time of Seminar")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "DD.MM.YYYY HH:MM")]
        public DateTime StartOn { get; set; }
        public string Text { get; set; }
        public string ExtLink { get; set; }
        public bool IsActive { get; set; }
        public int Duration { get; set; }
        public int OrderNum { get; set; }
    }
}