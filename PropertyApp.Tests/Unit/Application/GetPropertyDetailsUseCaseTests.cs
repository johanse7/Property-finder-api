using Moq;
using NUnit.Framework;
using AutoMapper;
using PropertyApp.Application.Dtos;
using PropertyApp.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PropertyApp.Tests.Application
{
  public class GetPropertyDetailsUseCaseTests
  {
    private Mock<IPropertyRepository> _mockRepo = null!;
    private Mock<IMapper> _mockMapper = null!;
    private GetPropertyDetailsUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
      _mockRepo = new Mock<IPropertyRepository>();
      _mockMapper = new Mock<IMapper>();
      _useCase = new GetPropertyDetailsUseCase(_mockRepo.Object, _mockMapper.Object);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsPropertyDetailDto_WhenPropertyExists()
    {
      var propertyId = "123";
      var property = new Property
      {
        Id = propertyId,
        Name = "Casa Test",
        Address = "Calle  123",
        Price = 1000000
      };

      _mockRepo.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync(property);

      _mockMapper.Setup(m => m.Map<PropertyDetailDto>(property))
          .Returns(new PropertyDetailDto
          {
            Property = new PropertyDto { Id = propertyId, Name = "Casa Test", Address = "Calle Falsa 123", Price = 1000000 },
            Owner = new OwnerDto { Name = "Juan Pérez" },
            Images = new List<PropertyImageDto> { new PropertyImageDto { File = "img1.jpg" } },
            Traces = new List<PropertyTraceDto> { new PropertyTraceDto { Name = "Registro 1" } }
          });


      var result = await _useCase.ExecuteAsync(propertyId);


      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Property.Id, Is.EqualTo(propertyId));
      Assert.That(result.Property.Name, Is.EqualTo("Casa Test"));
      Assert.That(result.Owner, Is.Not.Null);
      Assert.That(result.Images, Has.Exactly(1).Items);
      Assert.That(result.Traces, Has.Exactly(1).Items);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsNull_WhenPropertyDoesNotExist()
    {

      var propertyId = "not-found";
      _mockRepo.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync((Property?)null);

      var result = await _useCase.ExecuteAsync(propertyId);

      Assert.That(result, Is.Null);
      _mockMapper.Verify(m => m.Map<PropertyDetailDto>(It.IsAny<Property>()), Times.Never);
    }
  }
}
