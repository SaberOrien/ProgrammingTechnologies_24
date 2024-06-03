﻿using Logic.DTOs_Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs_Implemented
{
    internal class EventDTO : IEventDTO
    {
        public EventDTO(int id, int stateId, int userId, DateTime dateStamp, string eventType)
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
