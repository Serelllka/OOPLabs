using System;

namespace Reports.DAL.Entities
{
    public class Entry
    {
        private readonly Employee _author;
        private readonly string _content;

        private Entry()
        {
        }
        public Entry(Guid id, Employee author, string content)
        {
            _author = author;
            _content = content;

            Id = id;
        }
        
        public Guid Id { get; private init; }
    }
}