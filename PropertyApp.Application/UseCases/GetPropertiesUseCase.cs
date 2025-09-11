using AutoMapper;
using PropertyApp.Application.Dtos;

public class GetPropertiesUseCase
{
    private readonly IPropertyRepository _propertyRepo;
    private readonly IMapper _mapper;

    public GetPropertiesUseCase(IPropertyRepository propertyRepo, IMapper mapper)
    {
        _propertyRepo = propertyRepo;
        _mapper = mapper;
    }

    public async Task<PagedResult<PropertyListDto>> ExecuteAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 20)
    {
        var (items, totalCount) = await _propertyRepo.FilterAsync(name, address, minPrice, maxPrice, page, pageSize);


        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return new PagedResult<PropertyListDto>
        {
            Items = _mapper.Map<IEnumerable<PropertyListDto>>(items),
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize
        };
    }
}
