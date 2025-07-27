using FluentAssertions;
using NotBot.Services;

namespace NotBotTests.Services;

public class SecureCaptchaCodeGeneratorTests
{

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void Generate_Should_Return_String_With_Correct_Length(int length)
    {
        // Act
        var code = SecureCaptchaCodeGeenrator.Generate(length);

        // Assert
        code.Should().NotBeNull()
            .And.HaveLength(length);
    }

    [Fact]
    public void Generate_Should_Not_Return_Null_Or_Empty()
    {
        // act
        var code = SecureCaptchaCodeGeenrator.Generate(6);

        // assert
        code.Should().NotBeNullOrEmpty();
        code.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Generate_Should_Throw_If_Length_Is_Invalid(int invalidLength)
    {
        // Act
        var act = () => SecureCaptchaCodeGeenrator.Generate(invalidLength);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>()
           .WithMessage("*Length must be positive*");
    }

    [Fact]
    public void Generate_Should_Not_Return_Duplicate_Codes()
    {
        // Arrange
        var generatedCodes = new HashSet<string>();
        const int totalGenerations = 1000;
        const int codeLength = 8;

        // Act
        for (int i = 0; i < totalGenerations; i++)
        {
            var code = SecureCaptchaCodeGeenrator.Generate(codeLength);
            var added = generatedCodes.Add(code);

            // Assert each code is unique as it is generated
            added.Should().BeTrue($"duplicate code detected at iteration {i}: {code}");
        }

        // Assert overall count
        generatedCodes.Count.Should().Be(totalGenerations);
    }

}
