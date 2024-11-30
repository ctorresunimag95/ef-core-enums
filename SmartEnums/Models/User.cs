namespace SmartEnums.Models;

public class User
{

    public int Id { get; }
    
    public string Name { get; }
    
    private int RoleId { get; }
    public Role Role => (Role)RoleId;
    
    public User()
    {
    }
    
    public User(string name, Role role)
    {
        Name = name;
        RoleId = role;
    }
}
