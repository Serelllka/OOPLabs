using System;

namespace Reports.DAL.Entities
{
    public class Entry
    {
        private Entry()
        {
        }
        public Entry(Guid id, Employee author, string content)
        {
            Author = author;
            Content = content;

            Id = id;
        }
        
        public Guid Id { get; private init; }
        public string Content { get; private set; }
        public Employee Author { get; private set; }
    }
}