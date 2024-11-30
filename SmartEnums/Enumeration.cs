using System.Collections.ObjectModel;
using System.Reflection;

namespace SmartEnums;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
where TEnum : Enumeration<TEnum>
{
    private static readonly ReadOnlyCollection<TEnum> Values = SetEnumValues();

    protected Enumeration(int id, string name, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
    public int Id { get; }
    
    public string Name { get; }
    
    public string? Description { get; }
    
    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    public override string ToString() => Name;
    
    public static IEnumerable<string> GetNames() => Values.Select(x => x.Name);
    
    public static TEnum GetValue(int id) => Values.First(x => x.Id == id);
    
    public static TEnum GetValue(string name) => Values.First(x => x.Name == name);
    
    public static implicit operator string(Enumeration<TEnum> value) => value.Name;
    
    public static implicit operator int(Enumeration<TEnum> value) => value.Id;
    
    public static implicit operator Enumeration<TEnum>(int value) => GetValue(value);
    
    private static ReadOnlyCollection<TEnum> SetEnumValues()
    {
        var enumType = typeof(TEnum);
        
        return enumType.GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Select(prop => (TEnum)prop.GetValue(default)!)
            .ToList()
            .AsReadOnly();
    }
}