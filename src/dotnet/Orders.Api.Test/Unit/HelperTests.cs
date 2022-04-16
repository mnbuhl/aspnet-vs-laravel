using System;
using FluentAssertions;
using Orders.Application.Helpers;
using Xunit;

namespace Orders.Api.Test.Unit;

public class HelperTests
{
    [Fact]
    public void OrderByValidator_ReturnsOrderBy_WhenAllowed()
    {
        string[] allowedOrderBy = { "name", "-name", "price", "-price" };
        const string orderBy = "name";

        string result = OrderByValidator.Validate(allowedOrderBy, orderBy);

        result.Should().Be(orderBy);
    }

    [Fact]
    public void OrderByValidator_ThrowException_WhenOrderByIsNotAllowed()
    {
        string[] allowedOrderBy = { "name", "-name", "price", "-price" };
        const string orderBy = "id";

        Action action = () => OrderByValidator.Validate(allowedOrderBy, orderBy);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void PaginationOffset_CalculatesOffset_WhenValid()
    {
        const int page = 5;
        const int pageSize = 6;

        var result = PaginationOffset.Calculate(page, pageSize);

        result.Item1.Should().Be(25);
        result.Item2.Should().Be(5);
    }
}