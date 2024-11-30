namespace SmartEnums;

public class Role : Enumeration<Role>
{
    public static Role Guest => new Role(1, nameof(Guest));
    public static Role Contributor => new Role(2, nameof(Contributor));
    public static Role Admin => new Role(3, nameof(Admin));

    private Role(int id, string name, string? description = null)
        : base(id, name, description)
    {
    }
}