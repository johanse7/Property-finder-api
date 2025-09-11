using AutoMapper;
using PropertyApp.Application.Dtos;

public class GetPropertyDetailsUseCase
{
    private readonly IPropertyRepository _propertyRepo;
    private readonly IMapper _mapper;
    public GetPropertyDetailsUseCase(IPropertyRepository propertyRepo, IMapper mapper)
    {
        _propertyRepo = propertyRepo;
        _mapper = mapper;
    }

    public async Task<PropertyDetailDto?> ExecuteAsync(string id)
    {
        var property = await _propertyRepo.GetByIdAsync(id);
        if (property == null) return null;
        return _mapper.Map<PropertyDetailDto>(property);
    }
}
