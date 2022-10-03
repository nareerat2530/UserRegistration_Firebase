﻿using Google.Cloud.Firestore;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.DTO;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly FirestoreDb _db;
        private readonly EventsMapper _eventsMapper;

        public EventsController(EventsMapper eventsMapper, FirestoreDb db)
        {
            _db = db;
        
            _eventsMapper = eventsMapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEvent([FromBody] EventPostDto model)
        {


            var docRef = _db.Collection("calEvent").Document();
            var addNewEvent = _eventsMapper.Map(model);
            await docRef.SetAsync(addNewEvent);

            return StatusCode(200);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var collection = _db.Collection("calEvent");
            var snapshot = await collection.GetSnapshotAsync();

           
            var eventsList = snapshot.Documents.Select(x => x.ConvertTo<Events>()).ToList();
            var eventReadDtoList = _eventsMapper.Map(eventsList);
            return Ok(eventReadDtoList);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var eventRef = _db.Collection("calEvent").Document(id);
            await eventRef.DeleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] EventUpdateDTO model)
        {
            var docRef = _db.Collection("calEvent").Document(id);
            var updateEvent = _eventsMapper.Map(model, docRef);

            
            await docRef.SetAsync(updateEvent);
            return Ok();

        }
    }
}

