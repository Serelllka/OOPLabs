using System.Linq;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group @group = _isuService.AddGroup("M3200");
            Student student = _isuService.AddStudent(group, "F*ckingSlave");
            
            Assert.AreEqual(student.StudentGroup, group);
            Assert.IsTrue(group.Students.ToList().Contains(student));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group @group = _isuService.AddGroup("M3200");
            for (int i = 0; i < 30; ++i)
            {
                _isuService.AddStudent(group, $"The {i}st Fredy's Slave");
            }
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddStudent(group, $"Hohol");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group @group = _isuService.AddGroup("MMMM");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group @group = _isuService.AddGroup("M3200");
            Group newGroup = _isuService.AddGroup("M3202");
            Student student = _isuService.AddStudent(group, "FuckingSlave");
            Assert.IsTrue(student.StudentGroup == group);
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.IsTrue(student.StudentGroup == newGroup);
        }
    }
}