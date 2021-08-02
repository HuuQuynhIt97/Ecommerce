using System.Linq;
using System.Threading.Tasks;
using API.Helpers.Utilities;
using AutoMapper;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api._Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public ReviewService(
            IReviewRepository reviewRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<int> CountView()
        {
            var data = await _reviewRepository.FindAll().ToListAsync();
            return data.Count();
        }

        public async Task<OperationResult> Review(ReviewDto model)
        {
            var data = _mapper.Map<Review>(model);
            try
            {
                _reviewRepository.Add(data);
                await _reviewRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Review product successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}