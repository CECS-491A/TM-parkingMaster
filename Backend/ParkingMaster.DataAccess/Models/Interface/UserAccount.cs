namespace ParkingMaster.DataAccess.Models.Interface
{
    
    public interface UserAccount
    {
        string Email { get; }
        string Password { get; }
        // TODO: suggestion for SSO
        // bool? Registered { get; }
        // bool? NewUser { get; }
        string Role { get; }
    }
}
