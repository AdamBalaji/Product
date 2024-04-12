using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.DTO.Category;
using Product.DTO.SubCategory;
using Product.Model;
using Product.Repository.IRepository;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository _subcategoryRepository;
        private readonly IMapper _mapper;

        public SubCategoryController(ISubCategoryRepository SubCategoryRepository, IMapper mapper)
        {
            _subcategoryRepository = SubCategoryRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetAll()
        {
            var subcategories = await _subcategoryRepository.GetAll();
            var subcategoriesDto = _mapper.Map<List<SubCategoryDto>>(subcategories);

            if (subcategories == null)
            {
                return NotFound();
            }
            return Ok(subcategoriesDto);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubCategoryDto>> GetById(int id)
        {
            var subcategory = await _subcategoryRepository.Get(id);

            var subcategoryDto = _mapper.Map<SubCategoryDto>(subcategory);
            if (subcategory == null)
            {
                return NotFound();
            }
            return Ok(subcategoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateSubCategoryDto>> Create([FromBody] CreateSubCategoryDto subcategoryDto)
        {
            var result = _subcategoryRepository.IsRecordExist(X=>X.SubCategoryName == subcategoryDto.SubCategoryName);

            if (result)
            {
                return Conflict("SubCategory already exist");
            }

            //Category category = new Category();
            //category.CategoryName = categoryDto.CategoryName;
            //category.CategoryCode = categoryDto.CategoryCode;

            var subcategory = _mapper.Map<SubCategory>(subcategoryDto);

            await _subcategoryRepository.Create(subcategory);
            return CreatedAtAction("GetById", new { id = subcategory.SubCategoryId }, subcategory);
        }



        [HttpPut]
        public async Task<ActionResult<UpdateSubCategoryDto>> Update([FromBody] UpdateSubCategoryDto subcategoryDto)
        {
            var subcategory = _mapper.Map<SubCategory>(subcategoryDto);
            await _subcategoryRepository.Update(subcategory);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var subcategory = await _subcategoryRepository.Get(id);
            await _subcategoryRepository.Delete(subcategory);
            return Ok();
        }
    }
}

