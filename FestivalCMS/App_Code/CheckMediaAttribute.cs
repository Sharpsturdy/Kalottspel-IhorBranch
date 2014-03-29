using System;
using System.ComponentModel.DataAnnotations;
using FestivalCMS.Models;

namespace FestivalCMS.App_Code
{
    public class CheckMediaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CategoryModel model = (CategoryModel)validationContext.ObjectInstance;
            switch (model.Category.MediaTypeID)
            {
                case 1:
                    if (model.Content.Photo != null && model.Content.Photo.FileName != null && model.Content.Photo.PhotoFile != null)
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult("Please choose image");
                case 2:
                    if (model.Content.VideoLink != null && model.Content.VideoLink.Link != null)
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult("Please input video link");
                case 3:
                    if(model.Content.Gallery.PhotoFiles.Count==0) return new ValidationResult("No images in gallery");
                    foreach (var file in model.Content.Gallery.PhotoFiles)
                    {
                        if (file.FileName == null || file.PhotoFile == null)
                        {
                            return new ValidationResult("Error during upload gallery process");
                        }
                    }
                    return ValidationResult.Success;
                case 0:
                    return ValidationResult.Success;
                default:
                    return new ValidationResult("Unknown Media");
            }
        }
    }
}