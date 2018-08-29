using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PermutationsTestApp.Services;
using PermutationsTestApp.ViewModel;

namespace PermutationsTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermutationsController : ControllerBase
    {
        private readonly IPermutationsService _service;
        private readonly IMapper _mapper;

        public class ElementModel
        {
            public List<string> Values;
        }

        public PermutationsController(IPermutationsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ElementView>> Get()
        {
            return _mapper.Map<List<ElementView>>(await _service.GetAllAsync());
        }

        [HttpGet("{value}")]
        public async Task<ElementView> Get([FromRoute] string value)
        {
            var result = _mapper.Map<ElementView>(await _service.GetAsync(value));
            return result;
        }

        [HttpPost]
        public async Task<List<ElementView>> Post([FromBody] string[] values)
        {
            var result = new List<ElementView>();

            foreach (var value in values)
            {
                var element = await _service.CalculateAsync(value);

                var view = _mapper.Map<ElementView>(element);
                result.Add(view);
            }

            return result;
        }
    }
}
