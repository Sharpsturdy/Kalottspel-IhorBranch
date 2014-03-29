using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class EventsInFestivalModel
    {
        public List<Event> Events { get; set; }
        public Festival Festival { get; set; }
    }
}