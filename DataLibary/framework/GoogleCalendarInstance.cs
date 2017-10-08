using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using Ical.Net;
using StruggleApplication.api;
using Calendar = Ical.Net.Calendar;

namespace StruggleApplication.framework
{
    
    // TODO use _googleClient for api requests
    // TODO use this class for request prepartion
    
    public class GoogleCalendarInstance : ICalendarInstance
    {
        private GoogleClient _googleClient = new GoogleClient();
        private ICalendarInstance _calendarInstanceImplementation;

        public async Task Initialize(String code)
        {
            //TODO: delete
            Console.WriteLine("We are initializing");
            await _googleClient.SendAuthenticationRequest(code);
            Console.WriteLine("We initialized");
        }
        
        public List<Event> GetCalendar()
        {
            return _googleClient.getEventsRequest(DateTime.Now, false);
        }

        public List<Event> GetEventsForDate(DateTime date)
        {
            return _googleClient.getEventsRequest(date, true);
        }

        // TODO probably not needed for prototype
        public void CreateCalendar(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public void CreateEvent(CalendarEvent e)
        {
            throw new NotImplementedException();
        }

        public void CreateEvent(String title, String description, EventDateTime startTime, EventDateTime endTime)
        {
            Event newEvent = new Event()
            {
                Summary = title,
                Description = description,
                Start = startTime,
                End = endTime,
                Reminders = new Event.RemindersData()
                {
                    UseDefault = true,
                }
            };

            _googleClient.sendInsertRequest(newEvent);
        }

        public void DeleteCalendar(string guid)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(string guid)
        {
            _googleClient.deleteEventRequest(guid);
        }

        public string GenerateAuthLink()
        {
            return OAuthoriser.GenerateLink();
        }
    }
}