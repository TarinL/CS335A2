using A2Template.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A2Template.Data
{
    public class A2Repo : IA2Repo
    {
        private readonly A2DbContext _db;

        public A2Repo(A2DbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }
        public IEnumerable<Event> GetAllEvents()
        {
            return _db.Events.ToList();
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            return _db.Staff.ToList();
        }

        public User GetUserById(string UserName)
        {
            return _db.Users.FirstOrDefault(u => u.UserName == UserName);
        }

        public Staff GetStaffById(string Name)
        {
            return _db.Staff.FirstOrDefault(s => s.Name == Name);
        }

        public Event GetEventById(int Id)
        {
            return _db.Events.FirstOrDefault(e => e.Id == Id);
        }

        public User AddUser(User user)
        {
            EntityEntry<User> entityEntry = _db.Users.Add(user);
            User u = entityEntry.Entity;
            _db.SaveChanges();
            return u;
        }

        public Event AddEvent(Event e)
        {
            EntityEntry<Event> entityEntry = _db.Events.Add(e);
            Event ev = entityEntry.Entity;
            _db.SaveChanges();
            return ev;
        }

        public bool IsValidUser(string userName, string password)
        {
            User u = _db.Users.FirstOrDefault (u => u.UserName == userName && u.Password == password);
            if (u == null)
                return false;
            else
                return true;
        }

        public bool isValidStaff(string name, string password)
        {
            Staff s = _db.Staff.FirstOrDefault(s => s.Name == name && s.Password == password);
            if (s == null)
                return false;
            else
                return true;
        }
    }
}