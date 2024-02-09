using Bogus;
using FluentAssertions;
using Moq;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Enums;
using RocketseatAuction.API.UseCases.Auctions.GetCurrent;
using Xunit;

namespace UseCases.Test.Auctions.GetCurrent;
public class GetCurrentAuctionUseCaseTest
{
    [Fact]
    public void Success()
    {
        //ARRANGE
        var fakeAuction = new Faker<Auction>()
            .RuleFor(a => a.Id, f => f.Random.Number(1,700))
            .RuleFor(a => a.Name, f => f.Lorem.Word())       
            .RuleFor(a => a.Starts, f => f.Date.Past())       
            .RuleFor(a => a.Ends, f => f.Date.Future())
            .RuleFor(a => a.Items, (f, a) => new List<Item>
            {
                new Item
                {
                    Id = f.Random.Number(1,700),
                    Name = f.Commerce.ProductName(),
                    Brand = f.Commerce.Department(),
                    BasePrice = f.Random.Decimal(50, 1000),
                    Condition = f.PickRandom<Condition>(),
                    AuctionId = a.Id
                }
            }).Generate();
        var mock = new Mock<IAuctionRepository>();
        mock.Setup(i => i.GetCurrent()).Returns(fakeAuction);

        var useCase = new GetCurrentAuctionUseCase(mock.Object);

        //ACT
        var auction = useCase.Execute();

        //ASSERT
        auction.Should().NotBeNull();
        auction!.Id.Should().Be(fakeAuction.Id);
    }
}
