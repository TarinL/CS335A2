using A2Template.Models;

namespace A2Template.Data
{
    public interface IA2Repo
    {
        IEnumerable<User> GetUsers();
        IEnumerable<Staff> GetStaff();
        IEnumerable<Event> GetEvents();
        User GetUserById(string id);
        Staff GetStaffById(string id);
        Event GetEventById(string id);
        User AddUser (User user);
        
        
        
        void SaveChanges();
    } 
}