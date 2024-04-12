using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Data;
using Product.DTO.Category;
using Product.Model;
using Product.Repository;
using Product.Repository.IRepository;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryRepository, IMapper  mapper, ILogger<CategoryController> logger) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            if(categories == null)
            {
                
                return NotFound();
            }
            return Ok(categoriesDto);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryRepository.Get(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            if(category == null)
            {
                _logger.LogError($"Error while try to get record id:{id}");
                return NotFound();
            }
            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCategoryDto>> Create([FromBody]CreateCategoryDto categoryDto)
        {
            var result =  _categoryRepository.IsRecordExist(x=>x.CategoryName==categoryDto.CategoryName);

            if (result)
            {
                return Conflict("Category already exist");
            }

            //Category category = new Category();
            //category.CategoryName = categoryDto.CategoryName;
            //category.CategoryCode = categoryDto.CategoryCode;

            var category = _mapper.Map<Category>(categoryDto);

            await _categoryRepository.Create(category);
            return CreatedAtAction("GetById",new {id = category.CategoryId},category);
        }

        

        [HttpPut]
        public async Task<ActionResult<UpdateCategoryDto>> Update([FromBody]UpdateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.Update(category);
            
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var category = await _categoryRepository.Get(id);
            await _categoryRepository.Delete(category);
            return Ok();
        }
    }
}
