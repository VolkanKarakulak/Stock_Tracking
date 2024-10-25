﻿using Data.Repositories.GenericRepositories;
using Data.UnitOfWorks;
using Service.Exceptions.NotFoundExeptions;
using Service.DTOs.PaginationDto;
using Service.DTOs.ResponseDto;
using Service.Mapping;
using System.Linq.Expressions;



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

        public async Task<bool> DeleteAsync(int id)
        {
            
            var entity = _repository.Delete(id);

            if (entity)
            {
                await _unitOfWork.CommitAsync();
                return true;
            }
            throw new DataNotFoundException();
        }

        //public async Task DeleteRangeAsync(IEnumerable<int> entityIds)
        //{
        //    foreach (var entityId in entityIds) 
        //    {
        //        _repository.Delete(entityId);
        //    }
            
        //    await _unitOfWork.CommitAsync();
        //}

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

            if (!paged.Item3.Any())
            {
                throw new PageNotFoundException();
            }
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
            // "Id" property'sine ulaşma
            var idProperty = entity.GetType().GetProperty("Id");

            if (idProperty != null)
            {
                // "Id" property'sinin değerini alıyoruz
                var entityId = (int)idProperty.GetValue(entity);

                // isEntityExist kontrolü
                var isEntityExist = await _repository.IsEntityUpdateableAsync(entityId);

                if (isEntityExist)
                {
                    // Eğer entity güncellenebilir durumdaysa update işlemini yap
                    _repository.Update(entity);
                    await _unitOfWork.CommitAsync();
                }
            }
            else
            {
                // Eğer "Id" property yoksa uygun bir hata fırlatabilirsiniz.
                throw new DataNotFoundException();
            }

        }
        
    }
}
