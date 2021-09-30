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

        public GsaCourse(Faculty gsaFaculty, string name)
        {
            Name = name ?? throw new IsuExtraException("Name can't be null");
            GsaFaculty = gsaFaculty ?? throw new IsuExtraException("Faculty can't be null");
            GsaFaculty.GsaCourse = this;
            Id = Guid.NewGuid();
            _gsaFlows = new List<GsaFlow>();
        }

        public IReadOnlyList<GsaFlow> GsaFlows => _gsaFlows;
        public Faculty GsaFaculty { get; }
        public string Name { get; }
        public Guid Id { get; }

        public bool ContainsStudent(Student student)
        {
            GsaFlow obtainedObject = _gsaFlows.FirstOrDefault(item => item.ContainsStudent(student));
            return obtainedObject is not null;
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