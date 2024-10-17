using AutoMapper.Internal.Mappers;
using Data.Interceptors;
using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.PaginationDto;
using Service.DTOs.ProductDtos;
using Service.DTOs.ResponseDto;
using Service.Mapping;
using System.Linq.Expressions;
using System.Runtime.Serialization;


namespace Service.Services.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<T> CreateAsync(T entity)
        {
            
            await _repository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            await _repository.CreateRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task DeleteAsync(int id)
        {
            var result = _repository.Delete(id);
            if (result)
            {
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _repository.DeleteRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.ToList();
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<PagedResponseDto<IEnumerable<T>>> GetPagedAsync(PaginationDto paginationDto)
        {
           var paged = await _repository.GetPagedAsync(paginationDto.PageNumber, paginationDto.PageSize);

            //if (!paged.Item3.Any())
            //{
            //    throw new PageNotFoundException();
            //}
           var mappedItems = ObjectMapper.Mapper.Map<IEnumerable<T>>(paged.Item3);
           

           var pagedResponse = new PagedResponseDto<IEnumerable<T>>
           {
               PagedDto = mappedItems,
               TotalPages = paged.Item1,
               PageNumber = paginationDto.PageNumber,
               TotalCount = paged.Item2
           };

          return pagedResponse;
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }
        
    }
}
