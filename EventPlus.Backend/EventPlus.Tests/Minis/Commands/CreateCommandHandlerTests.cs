using EventPlus.Application.Minis.Commands.Create;
using EventPlus.Tests.Base;
using EventPlus.Tests.Configuration;
using EventPlus.Tests.Extensions;

namespace EventPlus.Tests.Minis.Commands;

public class CreateCommandHandlerTests() : Test("Create_Command_Tests_Database")
{
    [Fact]
    public async Task Should_Create_Command()
    {
        var commandName = "Test command";

        //Arrange
        var request = new CreateCommandRequest
        {
            Name = commandName
        };

        var serviceProvider = ServiceProviderConfiguration
            .GetServiceProviderMock();

        var handler = new CreateCommandHandler(serviceProvider);
        handler.MockDatabase(Database);

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.Equal(commandName, result.Name);
        Assert.NotNull(result);
        Assert.Null(result.Avatar);
        Assert.Null(result.Description);
    }
}