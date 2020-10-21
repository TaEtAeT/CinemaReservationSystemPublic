using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaReservationSystem.Models.ViewModels.ScreeningVM
{
    public class UpsertVM
    {
        public Screening Screening { get; set; }
        public IEnumerable<SelectListItem> MovieList { get; set; }
        public IEnumerable<SelectListItem> AuditoriumList { get; set; }

        public IEnumerable<SelectListItem> ListOfTimeIntervals
        {
            get
            {
                var list = new List<SelectListItem>();
                // range of hours, multiplied by 4 (e.g. 24 hours = 96)
                int timeRange = 96;

                // range of minutes, e.g. 15 min
                int minuteRange = 15;

                // starting time, e.g. 0:00
                TimeSpan startTime = new TimeSpan(0, 0, 0);

                // placeholder
                list.Add(new SelectListItem { Text = "Choose a time", Value = "0", Disabled = true });

                // get standard ticks
                DateTime startDate = new DateTime(DateTime.MinValue.Ticks);

                // create time format based on range above
                for (int i = 0; i < timeRange; i++)
                {
                    int minutesAdded = minuteRange * i;
                    TimeSpan timeAdded = new TimeSpan(0, minutesAdded, 0);
                    TimeSpan tm = startTime.Add(timeAdded);
                    DateTime result = startDate + tm;

                    list.Add(new SelectListItem { Text = result.ToString("HH:mm"), Value = result.ToString("HH:mm") });
                }

                return list;
            }
        }


        public IEnumerable<SelectListItem> WeekDays = new List<SelectListItem> {
        new SelectListItem{
        Text = "Monday",
        Value="Monday"
        },
         new SelectListItem{
        Text = "Tuesday",
        Value="Tuesdat"
        },
          new SelectListItem{
        Text = "Wednesday",
        Value="Wednesday"
        },
           new SelectListItem{
        Text = "Thursday",
        Value="Thursday"
        },
            new SelectListItem{
        Text = "Friday",
        Value="Friday"
        },
         new SelectListItem{
        Text = "Saturday",
        Value="Saturday"
        },
          new SelectListItem{
        Text = "Sunday",
        Value="Sunday"
        },
        };


    }
}
