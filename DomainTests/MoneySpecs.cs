using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;

namespace DomainTests;

public class MoneySpecs
{
    static T A<T>(Func<T, T>? customization = null)
    {
        var t = new Fixture().Create<T>();
        if (null != customization)
            t = customization(t);
        return t;
    }

    Money aValidMoney() =>
        A<Money>(with => new Money(Math.Abs(with.Value)));

    // class MoneyDto
    // {
    //     public decimal Amount { get; set; }
    //     public string Currency1 { get; set; }
    //     public string Currency2 { get; set; }
    //     public string Currency3 { get; set; }
    //     public string Currency4 { get; set; }
    //     public string Currency5 { get; set; }
    //     public string Currency6 { get; set; }
    //     public string Currency7 { get; set; }
    //     public string Currency8 { get; set; }
    // }

    void x()
    {
        // var money = A<MoneyDto>.But(with => new MoneyDto
        // {
        //     Amount = Math.Abs(with.Amount)
        // });
    }

    [Theory, AutoData]
    public void Money_initialize_properly(decimal amount)
        => new Money(Math.Abs(amount))
            .Should().BeEquivalentTo(new { Value = Math.Abs(amount) });

    [Theory, AutoData]
    public void Money_cannot_be_negative(decimal amount)
        => new Action(() => //Arrange
                new Money(-Math.Abs(amount)) //Act
        ).Should().Throw<Exception>(); //Assert

    [Theory, AutoData]
    public void Supports_subtraction(uint five)
    {
        //Arrange
        var smallerNumber = aValidMoney();
        var biggerNumber = new Money(smallerNumber.Value + five);

        //Act
        (biggerNumber - smallerNumber)

            //Assert
            .Value.Should().Be(five);
    }


    [Theory, AutoData]
    public void
        Less_money_cannot_be_subtracted_from_more_money(uint five)
    {
        //Arrange
        var smallerNumber = aValidMoney();
        var biggerNumber = new Money(smallerNumber.Value + five);

        //Act
        var expected = () => smallerNumber - biggerNumber;

        //Assert
        expected.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoData]
    public void Supports_add(decimal amount1, decimal amount2)
    {
        var amountOfMoney1 = new Money(amount1);
        var amountOfMoney2 = new Money(amount2);

        (amountOfMoney1 + amountOfMoney2).Value.Should()
            .Be(amount1 + amount2);
    }

    [Theory, AutoData]
    public void Supports_small_check_operation_when_is_correct(
        decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (smallerMoney < biggerMoney).Should().BeTrue();
    }

    [Theory, AutoData]
    public void Supports_small_check_operation_when_is_not_correct(
        decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (biggerMoney < smallerMoney).Should().BeFalse();
    }

    [Theory, AutoData]
    public void Supports_grater_check_operation_when_is_correct(
        decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (biggerMoney > smallerMoney).Should().BeTrue();
    }

    [Theory, AutoData]
    public void Supports_grater_check_operation_when_is_not_correct(
        decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (smallerMoney > biggerMoney).Should().BeFalse();
    }

    [Theory, AutoData]
    public void
        Supports_grater_or_equal_check_operation_when_is_not_correct(
            decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (smallerMoney >= biggerMoney).Should().BeFalse();
    }

    [Theory, AutoData]
    public void Supports_grater_or_equal_check_operation_when_is_correct(
        decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (biggerMoney >= smallerMoney).Should().BeTrue();
    }

    [Theory, AutoData]
    public void
        Supports_small_or_equal_check_operation_when_is_not_correct(
            decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (biggerMoney <= smallerMoney).Should().BeFalse();
    }

    [Theory, AutoData]
    public void Supports_small_or_equal_check_operation_when_is_correct(
        decimal value)
    {
        value = Math.Abs(value);
        var smallerMoney = new Money(value / 2);
        var biggerMoney = new Money(value);

        (smallerMoney <= biggerMoney).Should().BeTrue();
    }

    [Theory, AutoData]
    public void Money_initialize_with_value_properly(decimal amount)
    {
        Money money = amount;

        money.Value.Should().Be(amount);
    }

    [Theory, AutoData]
    public void cannot_initialize_money_with_negative_value_(
        decimal amount)
    {
        amount = -Math.Abs(amount);
        var expected = () =>
        {
            Money money = amount;
        };

        expected.Should().Throw<InvalidOperationException>();
    }
}