using Google.Apis.Calendar.v3.Data;
using StruggleApplication.api;
using StruggleApplication.framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StruggleApplication.domain
{
    class Plan
    {

        private ICalendarInstance _instance;

        private int _pomodoroTimeLearing;
        private int _pomodoroTimeBreak;
        private int _pomodoroTime;

        public Plan()
        {
            this._instance = new GoogleCalendarInstance();
            this._instance.Initialize();
            this._pomodoroTimeBreak = 10;
            this._pomodoroTimeLearing = 50;
            this._pomodoroTime = this._pomodoroTimeLearing + this._pomodoroTimeBreak;
        }



        public void planExam(String titleOfExam, DateTime from, DateTime to,
            int effortInMinutes, int maxPomodorosPerDay)
        {

        }

        // Returns the carry over of pomodoros
        public int planPomodoroOfDay(DateTime startTimeOfDay, DateTime endTimeOfDay, List<Event> events, int numberOfPomodoros, String titleOfExam)
        {
            endTimeOfDay = endTimeOfDay.AddHours(-1);
            DateTime currentFrom = startTimeOfDay;
            DateTime currentTo = currentFrom.AddMinutes(this._pomodoroTime);
            int createdPomodoros = 0;

            if (events.Count == 0) // no events
            {
                while (currentFrom.CompareTo(endTimeOfDay) == -1)
                {
                    AddPomodoro(currentFrom, titleOfExam);
                    createdPomodoros++;
                    // next pomodoro start time
                    currentFrom = currentFrom.AddMinutes(this._pomodoroTime);
                }
            }
            else // there is a event / are more events
            {
                List<Event>.Enumerator e = events.GetEnumerator();

                while (currentFrom.CompareTo(endTimeOfDay) == -1)
                {
                    if (e.MoveNext())
                    {
                        DateTime eventFrom = e.Current.Start.DateTime.Value;
                        DateTime eventTo = e.Current.End.DateTime.Value;

                        if (currentFrom.Subtract(eventTo).TotalMinutes > 0)
                        {
                            // placeable
                            AddPomodoro(currentFrom, titleOfExam);
                            createdPomodoros++;
                            // next pomodoro start time
                            currentFrom = currentFrom.AddMinutes(this._pomodoroTime);
                        }
                        if (eventFrom.Subtract(currentFrom).TotalMinutes > this._pomodoroTime)
                        {
                            // placeable
                            AddPomodoro(currentFrom, titleOfExam);
                            createdPomodoros++;
                            // next pomodoro start time
                            currentFrom = currentFrom.AddMinutes(this._pomodoroTime);
                        }
                        else // if (eventFrom.Subtract(currentFrom).TotalMinutes < this._pomodoroTime)
                        {
                            // collision
                            currentFrom = currentFrom.AddMinutes(5);
                            continue;
                        }
                        if (eventTo.Subtract(currentFrom).TotalMinutes >= 0 &&
                            currentFrom.Subtract(eventFrom).TotalMinutes >= 0)
                        {
                            // collision
                            currentFrom = currentFrom.AddMinutes(5);
                            continue;
                        }
                    }//if move next
                    else
                    {
                        // no events anymore
                        // ...
                    }

                    if (createdPomodoros == numberOfPomodoros)
                        break;
                }//while currentFrom head is lower than end time of day
            }//else

            return numberOfPomodoros - createdPomodoros;
        }


        // E.g.: start time is 08:00 o'clock to end time 21:00 o'clock 
        public TimeSpan CalculateAvailableTime(DateTime startTimeOfDay, DateTime endTimeOfDay, List<Event> events)
        {
            TimeSpan time = startTimeOfDay.Subtract(endTimeOfDay);

            // TODO: overlapping events
            // ATM: The calculation is wrong if the input events are overlapping

            foreach (Object obj in events)
            {
                Event currentEvent = (Event)obj;
                DateTime eventstart = currentEvent.Start.DateTime.Value;
                DateTime eventEnd = currentEvent.End.DateTime.Value;
                time = time.Subtract(eventEnd.Subtract(eventstart));
            }

            return time;
        }

        public int GetNumberOfDays(DateTime from, DateTime to)
        {
            double diff = to.Subtract(from).TotalDays;
            int numberOfDays = (int)Math.Ceiling(diff) + 1;

            return numberOfDays;
        }

        public int GetNumberOfWeeks(DateTime from, DateTime to)
        {
            double diff = to.AddDays(-1).Subtract(from).TotalDays;
            return (int)Math.Ceiling(diff / 7);
        }

        // Adds a learning and a break event
        public void AddPomodoro(DateTime startDateTime, String titleOfExam)
        {
            // learn
            AddEvent(startDateTime, this._pomodoroTimeLearing, "Lernen", titleOfExam);
            // break
            startDateTime = startDateTime.AddMinutes(this._pomodoroTimeLearing);
            AddEvent(startDateTime, this._pomodoroTimeBreak, "Pause", "");
        }

        // Adds an event
        private void AddEvent(DateTime startDateTime, int durationMinutes, String title, String desc)
        {
            EventDateTime eventStart = new EventDateTime();
            EventDateTime eventEnd = new EventDateTime();

            eventStart.DateTime = startDateTime;
            eventEnd.DateTime = eventStart.DateTime.Value.AddMinutes(durationMinutes);

            // create event
            this._instance.CreateEvent(title, desc, eventStart, eventEnd);
        }


    }//class
}//namespace
