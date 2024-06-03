﻿using Data.AbstractInterfaces;

namespace Data.ImplementedInterfaces
{
    internal class Event : IEvent
    {
        public Event(int id, int stateId, int userId, DateTime dateStamp, string eventType)
        {
            Id = id;
            StateId = stateId;
            UserId = userId;
            DateStamp = dateStamp;
            EventType = eventType;
        }

        public int Id { get; set; }
        public int StateId { get; set; }
        public int UserId { get; set; }
        public DateTime DateStamp { get; set; }
        public string EventType { get; set; }
    }
}
