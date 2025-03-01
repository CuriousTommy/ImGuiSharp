﻿namespace ImGuiSharp.Generator.Data;

internal class EnumDefinition : BaseDefinition
{
    public List<EnumValue> Values { get; set; } = new();
    public string? Location { get; set; }

    public string NameSpace { get; set; }
    
    public bool IsFlags => FriendlyName.Contains("Flags");
    
    public void SanitizeNames()
    {
        foreach (var member in Values)
        {
            member.Name = SanitizeMemberName(member.Name);
            member.Value = SanitizeMemberName(member.Value);
        }
    }

    public void FixSize()
    {
        foreach (var enumValue in Values)
        {
            try
            {
                var value = enumValue.Value;
                var numbers = value.Split('+').Select(x => x.Trim()).Select(int.Parse).ToList();
                if(numbers.Count > 1)
                {
                    var newValue = numbers.Sum();
                    var plusStart = value.IndexOf('+');
                    var startString = value[..plusStart];
                    var newString = $"{startString} + {newValue}";
                    enumValue.Value = newString;
                }
            }
            catch (Exception e)
            {
                //Ignore
            }
        }
    }

    private string SanitizeMemberName(string memberName)
    {
        var ret = memberName.Replace("_","");
        return ret;
    }
}