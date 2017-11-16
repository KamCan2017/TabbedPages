using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using ToDoAPi.Models;

namespace ToDoAPi.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TaskController : ApiController
    {
        private static ConcurrentDictionary<Guid, TaskDao> _toDoItems = new ConcurrentDictionary<Guid, TaskDao>();


        [Route("{id}", Name = "GetById")]
        public IHttpActionResult Get(string id)
        {
            TaskDao item = null;
            if (_toDoItems.TryGetValue(Guid.Parse(id), out item))
            {
                return Ok(item);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_toDoItems.Values.ToList());
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(TaskDao item)
        {
            if (item == null)
            {
                return BadRequest("ToDo cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(_toDoItems.Keys.Contains(item.ID))
            {
                _toDoItems[item.ID] = item;
                return Ok(item);
            }
            item.ID = Guid.NewGuid();
            _toDoItems[item.ID] = item;
            CreatedAtRoute("GetById", new { id = item.ID }, item);
            return Ok(item);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(string id, TaskDao item)
        {
            if (item == null)
            {
                return BadRequest("ToDo cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (item.ID != Guid.Parse(id))
            {
                return BadRequest("item.ID does not match id parameter");
            }

            if (!_toDoItems.Keys.Contains(Guid.Parse(id)))
            {
                return NotFound();
            }

            _toDoItems[Guid.Parse(id)] = item;
            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            TaskDao item = null;
            _toDoItems.TryRemove(Guid.Parse(id), out item);
            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }
    }
}
