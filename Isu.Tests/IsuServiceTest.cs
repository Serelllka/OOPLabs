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
            Assert.IsTrue(group.ListOfStudents.Contains(student));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                int studentIndex = 1;
                Group @group = _isuService.AddGroup("M3200"); 
                while (true)
                {
                    _isuService.AddStudent(group, $"The {studentIndex}st Fredy's Slave");
                }
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