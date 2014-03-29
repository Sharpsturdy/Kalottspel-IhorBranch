using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FestivalCMS.Models
{
    public class EventModel
    {
        string startTime;
        string startDate;
        int min;
        int hour;
        int day;
        int month;
        int year;

        public EventModel()
        {
            Event = new Event();
        }
        public Event Event { get; set; }
        public IEnumerable<SelectListItem> Artists { get; set; }
        
        [CheckArtistsInEventAtribute]
        public List<int> SelectedArtists { get; set; }
        [Required(ErrorMessage = "Please input date")]
        public string StartDate
        {
            get
            {
                if (Event.StartOn < new DateTime(2000, 1, 1))
                {
                    return DateTime.Now.ToShortDateString();
                }
                return Event.StartOn.ToShortDateString();
            }
            set
            {
                startDate = value;
                ParseAndSetEventStartOn();
            }
        }


        [Required(ErrorMessage = "Please input time")]
        public string StartTime
        {
            get
            {
                if (Event.StartOn < new DateTime(2000, 1, 1))
                {
                    return DateTime.Now.ToShortTimeString();
                }
                return Event.StartOn.ToShortTimeString();
            }
            set
            {
                startTime = value;
                ParseAndSetEventStartOn();
            }
        }
        public int Duration { get; set; }


        private void ParseAndSetEventStartOn()
        {
            if (!string.IsNullOrEmpty(startDate))
            {
                string[] dateSplit = startDate.Split('.');
                day = Convert.ToInt32(dateSplit[0]);
                month = Convert.ToInt32(dateSplit[1]);
                year = Convert.ToInt32(dateSplit[2]);
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                string[] timeSplit = startTime.Split(':');
                hour = Convert.ToInt32(timeSplit[0]);
                min = Convert.ToInt32(timeSplit[1]);
            }
            if (year != 0 && month != 0 && day != 0)
            {
                Event.StartOn = new DateTime(year, month, day, hour, min, 0);
            }
        }
    }
}

public class CheckArtistsInEventAtribute:ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        List<int> artists = (List<int>)value;
        return artists==null||artists.Count==0?new ValidationResult("Please select artist"):ValidationResult.Success;
    }
}