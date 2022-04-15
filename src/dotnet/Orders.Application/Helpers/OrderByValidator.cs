namespace Orders.Application.Helpers;

public static class OrderByValidator
{
    public static string? Validate(string[] allowedValues, string? orderBy)
    {
        if (!allowedValues.Contains(orderBy))
        {
            throw new ArgumentException("Invalid sort option", nameof(orderBy));
        }

        return orderBy?.ToLower();
    }
}