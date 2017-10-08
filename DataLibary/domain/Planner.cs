using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ical.Net;
using StruggleApplication.api;
using StruggleApplication.framework;
using Google.Apis.Calendar.v3.Data;

namespace StruggleApplication.domain
{
    class Planner
    {
        private int _pomodoroTimeMinutesLearing;
        private int _pomodoroTimeMinutesBreak;
        private int _maximumPomodorosPerDay;

        private int _currentLearingTime;

        private String _description;
        private int _limitOfLearingTime;

        private ICalendarInstance _instance;

        // 
        public Planner()
        {
            this._pomodoroTimeMinutesLearing = 50;
            this._pomodoroTimeMinutesBreak = 10;
            this._maximumPomodorosPerDay = 6;

            this._instance = new GoogleCalendarInstance();
            this._instance.Initialize();
        }

        /*
         * ---------------------------------
         *  Nicht der beste Code...
         * ---------------------------------
         */
        public void planExamPreparation(DateTime start, DateTime exam, String examTitle, int effortMinutes, int maxLearningMinutesPerDay)
        {
            this._description = examTitle;
            this._currentLearingTime = 0;

            effortMinutes = (int)(effortMinutes * 1.2);
            this._limitOfLearingTime = effortMinutes - this._pomodoroTimeMinutesLearing - this._pomodoroTimeMinutesBreak;


            Console.WriteLine("Start: " + start);
            Console.WriteLine("Exam '" + examTitle + "': " + exam);
            Console.WriteLine("Effort with buffer: " + effortMinutes + " min");
            Console.WriteLine("--------------------------------------------------");

            int days = GetNumberOfDays(start, exam);


            // last learing day
            DateTime currentDay = exam.AddDays(-1);

            while (currentDay.CompareTo(start) >= 0 && this._currentLearingTime <= this._limitOfLearingTime)
            {                
                // plan one day
                List<Event> events = this._instance.GetEventsForDate(currentDay);
                double availableTimeOfDayMinutes = GetAvailableTime(events).TotalMinutes;
                if (availableTimeOfDayMinutes > maxLearningMinutesPerDay)
                    availableTimeOfDayMinutes = maxLearningMinutesPerDay;

                int possibleNumberOfPomodoros = (int)availableTimeOfDayMinutes / this._pomodoroTimeMinutesLearing;
                if (possibleNumberOfPomodoros > _maximumPomodorosPerDay)
                    possibleNumberOfPomodoros = _maximumPomodorosPerDay;

                Console.WriteLine("Day: " + currentDay);
                Console.WriteLine("Possible pomodoros: " + possibleNumberOfPomodoros);
                Console.WriteLine("Available time: " + availableTimeOfDayMinutes);

                if (possibleNumberOfPomodoros > 0)
                {
                    AddPomodorosToDay(possibleNumberOfPomodoros, availableTimeOfDayMinutes, currentDay, events, maxLearningMinutesPerDay);
                }
                Console.WriteLine("--------------------------------------------------");

                // move to next day
                currentDay = currentDay.AddDays(-1);

                if (currentDay.DayOfWeek == DayOfWeek.Saturday)
                    currentDay = currentDay.AddDays(-1);
                if (currentDay.DayOfWeek == DayOfWeek.Sunday)
                    currentDay = currentDay.AddDays(-2);
            }//while

            Console.ReadKey();

        }

        //
        private int AddPomodorosToDay(int possibleNumberOfPomodoros, double availableTimeOfDay, DateTime currentDay, List<Event> events, int maxLearningMinutesPerDay)
        {
            int actualNumberOfPomodoros = 0;
            for (int i = 0; i < possibleNumberOfPomodoros && availableTimeOfDay > this._pomodoroTimeMinutesLearing; i++)
            {
                bool success = AddPomodoroToDay(currentDay, events);
                if (success)
                {
                    actualNumberOfPomodoros++;
                    this._currentLearingTime += this._pomodoroTimeMinutesLearing;
                    if (this._currentLearingTime > this._limitOfLearingTime)
                        break;

                }
            }//for

            return possibleNumberOfPomodoros - actualNumberOfPomodoros; // carry over of pomodoros
        }

        // 
        public bool AddPomodoroToDay(DateTime currentDay, List<Event> events)
        {
            // TODO

            EventDateTime eventStart = new EventDateTime();
            EventDateTime eventEnd = new EventDateTime();

            int hours = 12; // TODO
            int minutes = 0; // TODO
            int seconds = 0; // TODO

            eventStart.DateTime = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, hours, minutes, seconds);
            eventEnd.DateTime = eventStart.DateTime.Value.AddMinutes(this._pomodoroTimeMinutesLearing);

            this._instance.CreateEvent("Lernen", this._description, eventStart, eventEnd);

            eventStart.DateTime = eventEnd.DateTime;
            eventEnd.DateTime = eventEnd.DateTime.Value.AddMinutes(this._pomodoroTimeMinutesBreak);

            this._instance.CreateEvent("Pause", "", eventStart, eventEnd);

            Console.WriteLine("Add a pomodoro meeting at: " + currentDay + " " + eventStart + " - " + eventEnd);


            // TODO

            return true;
        }
        
        /*
        public int GetNumberOfWeeks(DateTime start, DateTime exam)
        {
            // Weeks(02.10.17, 09.10.17) = 1
            // Weeks(02.10.17, 03.10.17) = 0
            TimeSpan diff = exam.AddDays(-1).Subtract(start);
            return (int)Math.Ceiling(diff.TotalDays / 7);
        }
        */

        public int GetNumberOfDays(DateTime start, DateTime exam)
        {
            TimeSpan availableTime = new TimeSpan(0, 0, 0);

            double dayDiff = exam.Subtract(start).TotalDays;
            int numberOfDays = (int)Math.Ceiling(dayDiff) + 1;

            return numberOfDays;
        }

        /*
        //
        public TimeSpan GetAvailableTimeOf(DateTime start, DateTime end)
        {
            TimeSpan availableTime = new TimeSpan(0, 0, 0);

            double dayDiff = end.Subtract(start).TotalDays;
            int numberOfDays = (int)Math.Ceiling(dayDiff) + 1;
            // Diff(08.10.17, 02.10.17) = 6 days ; 6 + 1 = 7 days (start and end inclusive)
            // Diff(05.10.17, 02.10.17) = 3 days ; 3 + 1 = 4 days (start and end inclusive)
            // ...

            // available time of start
            DateTime currentDay = start;
            List<Event> events = this._instance.GetEventsForDate(currentDay);
            availableTime = availableTime.Add(GetAvailableTime(events));

            // available time till end (inclusive)
            for (int i = 1; i < numberOfDays; i++)
            {
                currentDay = currentDay.AddDays(1);
                events = this._instance.GetEventsForDate(currentDay);
                availableTime = availableTime.Add(GetAvailableTime(events));
            }

            return availableTime;
        }
        */
        // 
        public TimeSpan GetAvailableTime(List<Event> events)
        {
            int hours = (this._maximumPomodorosPerDay * this._pomodoroTimeMinutesLearing) / 60;
            hours += (this._maximumPomodorosPerDay * this._pomodoroTimeMinutesBreak) / 60;
            TimeSpan availableTime = new TimeSpan(hours, 0, 0);
            
            DateTime timeEnd = new DateTime();
            DateTime timeStart = new DateTime();

            foreach (Object obj in events)
            {
                Event currentEvent = (Event)obj;

                Console.WriteLine("Event: " + currentEvent.Summary);
                Console.WriteLine("Event: " + currentEvent.End.DateTime);
                Console.WriteLine("Total Events: " + events.Count);

                if (currentEvent.End.DateTime.Value > timeEnd &&
                    currentEvent.Start.DateTime.Value > timeStart)
                {
                    timeEnd = currentEvent.End.DateTime.Value;
                    timeStart = currentEvent.Start.DateTime.Value;

                    availableTime = availableTime.Subtract(timeEnd.Subtract(timeStart));
                }
            }

            if (availableTime.TotalMilliseconds < 0)
            {
                return new TimeSpan(0, 0, 0);
            }

            return availableTime;
        }






    }//class
}//namespace
