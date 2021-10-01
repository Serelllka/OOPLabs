using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class GsaCourse
    {
        private List<GsaFlow> _gsaFlows;

        public GsaCourse(string name)
        {
            Name = name ?? throw new IsuExtraException("Name can't be null");
            Id = Guid.NewGuid();
            _gsaFlows = new List<GsaFlow>();
        }

        public IReadOnlyList<GsaFlow> GsaFlows => _gsaFlows;
        public string Name { get; }
        public Guid Id { get; }

        public bool ContainsStudent(Student student)
        {
            return _gsaFlows.Any(item => item.ContainsStudent(student));
        }

        public GsaFlow GetFlowByStudent(Student student)
        {
            GsaFlow obtainedObject = _gsaFlows.FirstOrDefault(item => item.ContainsStudent(student));
            return obtainedObject ?? throw new IsuExtraException("Can't get this flow");
        }

        public GsaFlow CreateNewFlow(Schedule schedule)
        {
            if (schedule is null)
            {
                throw new IsuExtraException("this schedule is null");
            }

            var item = new GsaFlow(schedule, this);
            _gsaFlows.Add(item);
            return item;
        }
    }
}