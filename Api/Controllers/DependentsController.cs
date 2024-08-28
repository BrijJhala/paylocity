using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{

    private final DependentService dependentService;

    public DependentController(DependentService dependentService) {
        this.dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")] 

     @GetMapping("/{id}")
    public ResponseEntity<ApiResponse<GetDependentDto>> Get(@PathVariable int id) {
        ApiResponse<GetDependentDto> response = new ApiResponse<>();
        try {
            DependentDTO dependent = dependentService.getDependentById(id);
            GetDependentDto dependentDto = mapToGetDependentDto(dependent);
            response.setData(dependentDto);
            response.setMessage("Dependent retrieved successfully");
            return ResponseEntity.ok(response);
        } catch (DependentNotFoundException e) {
            response.setSuccess(false);
            response.setError(e.getMessage());
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(response);
        } catch (Exception e) {
            response.setSuccess(false);
            response.setError("An error occurred while retrieving the dependent");
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(response);
        }
    }

    @GetMapping
    public ResponseEntity<ApiResponse<List<GetDependentDto>>> GetAll() {
        ApiResponse<List<GetDependentDto>> response = new ApiResponse<>();
        try {
            List<DependentDTO> dependents = dependentService.getAllDependents();
            List<GetDependentDto> dependentDtos = dependents.stream()
                    .map(this::mapToGetDependentDto)
                    .collect(Collectors.toList());
            response.setData(dependentDtos);
            response.setMessage("Dependents retrieved successfully");
            return ResponseEntity.ok(response);
        } catch (Exception e) {
            response.setSuccess(false);
            response.setError("An error occurred while retrieving dependents");
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(response);
        }
    }

     private GetDependentDto mapToGetDependentDto(DependentDTO dependent) {
        GetDependentDto dto = new GetDependentDto();
        dto.setId(dependent.getId());
        dto.setFirstName(dependent.getFirstName());
        dto.setLastName(dependent.getLastName());
        dto.setDateOfBirth(dependent.getDateOfBirth());
        dto.setRelationship(dependent.getRelationship());
        return dto;
    }


    
}
