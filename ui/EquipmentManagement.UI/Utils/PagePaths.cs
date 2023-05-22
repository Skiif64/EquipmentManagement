namespace EquipmentManagement.UI.Utils;

public static class PagePaths
{
    public const string Home = "/";
    public static class Auth
    {
        public const string Login = "/auth/login";
        public const string Logout = "/auth/logout";
        public const string Register = "/auth/register";
    }
    public static class Employee
    {
        public const string List = "/employee";
        public const string Add = "/employee/add";
        public const string Edit = "/employee/edit";
    }
    public static class Equipment
    {
        public const string List = "/equipment";
        public const string Add = "/equipment/add";
        public const string Edit = "/equipment/edit";
        public const string EmployeeEquipment = "/equipment/employee";
    }
    public static class EquipmentRecord
    {
        public const string History = "/record/history";
        public const string Add = "/record/add";       
    }
    public static class User
    {
        public const string List = "/user/";        
    }

    public static class Journal
    {
        public const string List = "/journal/";
    }
}
