using Moq;
using NUnit.Framework;
using AutoMapper;
using PropertyApp.Application.Dtos;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Tests.Unit.Application

{
  public class GetPropertiesUseCaseTests
  {
    private Mock<IPropertyRepository> _mockRepo = null!;
    private Mock<IMapper> _mockMapper = null!;
    private GetPropertiesUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
      _mockRepo = new Mock<IPropertyRepository>();
      _mockMapper = new Mock<IMapper>();
      _useCase = new GetPropertiesUseCase(_mockRepo.Object, _mockMapper.Object);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsPagedResult()
    {

      _mockRepo.Setup(r => r.FilterAsync(null, null, null, null, 1, 20))
               .ReturnsAsync((new List<Property> { new Property { Name = "Casa 1" } }, 1));

      _mockMapper.Setup(m => m.Map<IEnumerable<PropertyListDto>>(It.IsAny<IEnumerable<Property>>()))
                 .Returns(new List<PropertyListDto> { new PropertyListDto { Name = "Casa 1" } });


      var result = await _useCase.ExecuteAsync(null, null, null, null);


      Assert.That(result, Is.Not.Null);
      Assert.That(result.TotalCount, Is.EqualTo(1));
      Assert.That(result.Items.First().Name, Is.EqualTo("Casa 1"));

    }
  }
}
