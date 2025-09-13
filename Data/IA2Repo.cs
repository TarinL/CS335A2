using A2Template.Models;

namespace A2Template.Data
{
    public interface IA2Repo
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<Staff> GetAllStaff();
        IEnumerable<Event> GetAllEvents();
        User GetUserById(string UserName);
        Staff GetStaffById(string id);
        Event GetEventById(int id);
        User AddUser (User user);
        Event AddEvent(Event e);
        bool IsValidLogin(string userName, string password);
        void SaveChanges();
    } 
}