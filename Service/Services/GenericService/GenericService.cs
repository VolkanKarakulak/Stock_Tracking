using AutoMapper;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using Service.Exceptions.NotFoundExeptions;
using System.Linq.Expressions;

namespace Service.Services.GenericService
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TDto>(entity);
        }

        public async Task<IEnumerable<TDto>> CreateRangeAsync(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            await _repository.CreateRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var idProperty = entity.GetType().GetProperty("Id");

            if (idProperty == null)
            {
                throw new InvalidOperationException("Entity does not have an Id property.");
            }

            // "Id" property'sinin değerini alıyoruz ve null olup olmadığını kontrol ediyoruz
            var entityIdValue = idProperty.GetValue(entity);

            if (entityIdValue == null || !(entityIdValue is int entityId))
            {
                throw new DataNotFoundException();
            }

            var isEntityExist = await _repository.IsEntityUpdateableAsync(entityId);

            if (!isEntityExist)
            {
                throw new DataNotFoundException();
            }

            // Eğer entity güncellenebilir durumdaysa update işlemini yap
            await _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.DeleteAsync(id);

            if (entity)
            {
                await _unitOfWork.CommitAsync();
                return true;
            }
            throw new DataNotFoundException();
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression)
        {
            // Burada doğrudan IQueryable döndürüyoruz
            return _repository.GetBy(expression);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new DataNotFoundException();
            return _mapper.Map<TDto>(entity);
        }

        public async Task<PagedResponseDto<IEnumerable<TDto>>> GetPagedAsync(PaginationDto paginationDto)
        {
            var paged = await _repository.GetPagedAsync(paginationDto.PageNumber, paginationDto.PageSize);

            if (!paged.Item3.Any())
            {
                throw new PageNotFoundException();
            }

            var mappedItems = _mapper.Map<IEnumerable<TDto>>(paged.Item3);

            var pagedResponse = new PagedResponseDto<IEnumerable<TDto>>
            {
                PagedDto = mappedItems,
                TotalPages = paged.Item1,
                PageNumber = paginationDto.PageNumber,
                TotalCount = paged.Item2
            };

            return pagedResponse;
        }
    }
}

