using System.Text.RegularExpressions;

namespace Book_Shelf.Models
{
    public readonly struct Isbin
    {
        private readonly string? _value;
        public string Value => _value ?? string.Empty;
        private Isbin(string value)
        {
            _value = value;
        }

        public static (bool IsValid, Isbin? Value) TryCreate(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return (false, null);
            var trimmed = input.Trim();
            if (!Regex.IsMatch(trimmed, @"^\d{13}$")) return (false, null);
            return (true, new Isbin(trimmed));
        }
    }
}
