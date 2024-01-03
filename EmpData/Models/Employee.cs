namespace EmpData.Models
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int Contact { get; set; }
        public string? Email { get; set; }
        public double? Salary { get; set; }
    }
    public class ApiResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<Employee> data { get; set; }
    }
}
