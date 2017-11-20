using Microsoft.Practices.ObjectBuilder2;
using System.Collections.Generic;
using TabbedPages.Daos;
using TabbedPages.Models;

namespace TabbedPages.Mappper
{
    public class TaskMapper : ITaskMapper
    {
        public TaskDao Convert(TaskModel model)
        {
            TaskDao dao = new TaskDao()
            {
                ID = model.ID,
                Name = model.Name,
                Description = model.Description,
                Start = model.Start,
                End = model.End,
                IsDeleted = model.IsDeleted
            };

            return dao;
        }

        public TaskModel Convert(TaskDao dao)
        {
            TaskModel model = new TaskModel()
            {
                ID = dao.ID,
                Name = dao.Name,
                Description = dao.Description,
                Start = dao.Start,
                End = dao.End,
                IsDeleted = dao.IsDeleted

            };

            return model;
        }

        public IEnumerable<TaskDao> Convert(IEnumerable<TaskModel> model)
        {
            var list = new List<TaskDao>();
            model.ForEach(p => list.Add(Convert(p)));

            return list;
        }

        public IEnumerable<TaskModel> Convert(IEnumerable<TaskDao> daos)
        {
            var list = new List<TaskModel>();
            daos.ForEach(p => list.Add(Convert(p)));

            return list;
        }
    }
}
