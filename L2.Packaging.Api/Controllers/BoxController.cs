using L2.Packaging.UseCase.UseCase.Packaging;
using Microsoft.AspNetCore.Mvc;

namespace L2.Packaging.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxController : Controller
    {
        public BoxController(IPackagingUseCase packagingUseCase)
        {
            _packagingUseCase = packagingUseCase;
        }

        private readonly IPackagingUseCase _packagingUseCase;

        [HttpPost]
        public List<OrderPackagingResponse> PackagingOrderBox([FromBody] List<OrderPackagingRequest> orderPackagingRequest)
            => _packagingUseCase.Handle(orderPackagingRequest);
    }
}
