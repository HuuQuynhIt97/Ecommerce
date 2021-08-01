using System.Linq;
using System.Threading.Tasks;
using API.Helpers.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using ecommerce_api.Models;

namespace ecommerce_api._Services.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public WishlistService(
            IWishlistRepository wishlistRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(WishlistDto model)
        {
            var data = _mapper.Map<Wishlist>(model);
            try
            {
                _wishlistRepository.Add(data);
                await _wishlistRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Add product wishlist successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int productId, int userId)
        {
            var data = _wishlistRepository.FindAll(x => x.ID == id && x.Product_ID == productId && x.User_ID == userId)
                                          .FirstOrDefault();
            if (data != null)
            {
                try
                {
                    _wishlistRepository.Remove(data);
                    await _wishlistRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Remove product wishlist successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Remove product wishlist failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PageListUtility<WishlistDto>> GetDataWithPagination(PaginationParams param, bool isPaging = true)
        {
            var data =_wishlistRepository.FindAll().ProjectTo<WishlistDto>(_configuration)
                                                   .OrderByDescending(x => x.Created_at);
            return await PageListUtility<WishlistDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }
    }
}