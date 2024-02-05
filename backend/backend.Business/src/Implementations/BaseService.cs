using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Shared;
using backend.Business.src.Shared;

namespace backend.Business.src.Implementations
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto> : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    {
        private readonly IBaseRepo<T> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepo<T> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteOneById(Guid id)
        {
            var foundItem = await _baseRepository.GetOneById(id);
            if (foundItem is null)
            {
                return false;
            }
            await _baseRepository.DeleteOneById(foundItem);
            return true;
        }

        public virtual async Task<IEnumerable<TReadDto>> GetAll(QueryOptions queryOptions)
        {
            return _mapper.Map<IEnumerable<TReadDto>>(await _baseRepository.GetAll(queryOptions));
        }

        public virtual async Task<TReadDto> GetOneById(Guid id)
        {
            var foundItem = await _baseRepository.GetOneById(id);
            if(foundItem == null) 
            {
                throw CustomException.NotFoundException();
            }
            else 
            {
                return _mapper.Map<TReadDto>(foundItem);
            } 
        }

        public async Task<TReadDto> UpdateOneById(Guid id, TUpdateDto updated)
        {
            var foundItem = await _baseRepository.GetOneById(id);
            if (foundItem == null)
            {
                throw new Exception("Item not found");
            }

            _mapper.Map(updated, foundItem); 
            var updatedEntity = await _baseRepository.UpdateOneById(foundItem); 
            return _mapper.Map<TReadDto>(updatedEntity);
        }

        public virtual async Task<TReadDto> CreateOne(TCreateDto dto)
        {
            var entity = await _baseRepository.CreateOne(_mapper.Map<T>(dto));
            return _mapper.Map<TReadDto>(entity);
        }
    }
}