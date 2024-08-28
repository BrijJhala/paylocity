using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
/*
Its java implementation to demostrate how controller works.
**/
public class EmployeesController : ControllerBase
{
    private EmployeeService employeeService;

    @ApiOperation(value = "Create a new employee")
    @ApiResponses(value = {
            @ApiResponse(code = 200, message = "Successfully created employee", response = ApiResponse.class),
            @ApiResponse(code = 500, message = "Internal server error")
    })
    @PostMapping
    public ApiResponse<EmployeeDTO> createEmployee(@RequestParam string firstName,@RequestParam string lastName, @RequestParam double salary, string dateOfBirth) {
        ApiResponse<EmployeeDTO> response = new ApiResponse<>();
        try {
            EmployeeDTO employee = employeeService.createEmployee(firstName, lastName, salary, dateOfBirth);
            response.setData(employee);
            response.setMessage("Employee created successfully");
        } catch (Exception e) {
            response.setSuccess(false);
            response.setError("Failed to create employee");
        }
        return response;
    }


    @GetMapping("/{id}")
    @ApiOperation(value = "Get an employee by ID")
    @ApiResponses(value = {
            @ApiResponse(code = 200, message = "Successfully retrieved employee", response = ApiResponse.class),
            @ApiResponse(code = 404, message = "Employee not found"),
            @ApiResponse(code = 500, message = "Internal server error")
    })
   [SwaggerOperation(Summary = "Get employee by id")]

    public ApiResponse<EmployeeDTO> getEmployeeById(@PathVariable int id) {
        ApiResponse<EmployeeDTO> response = new ApiResponse<>();
        try {
            EmployeeDTO employee = employeeService.getEmployeeById(id);
            response.setData(employee);
            response.setMessage("Employee retrieved successfully");
        } catch (EmployeeNotFoundException e) {                                                                                                             
            response.setSuccess(false);
            response.setError(e.getMessage());
        } catch (Exception e) {
            response.setSuccess(false);
            response.setError("Failed to retrieve employee");
        }
        return response;
    }
    @ApiOperation(value = "Add a dependent to an employee")
    @ApiResponses(value = {
            @ApiResponse(code = 200, message = "Successfully added dependent", response = ApiResponse.class),
            @ApiResponse(code = 404, message = "Employee not found"),
            @ApiResponse(code = 500, message = "Internal server error")
    })
    @PostMapping("/{id}/dependents")
    public ApiResponse<DependentDTO> addDependent(
            @PathVariable int id,
            @RequestParam String firstName,
            @RequestParam String lastName,
            @RequestParam String dateOfBirth,  // This will be parsed to LocalDate
            @RequestParam Relationship relationship) {
        ApiResponse<DependentDTO> response = new ApiResponse<>();
        try {
            LocalDate dob = LocalDate.parse(dateOfBirth);  // Parsing the date from String
            DependentDTO dependent = employeeService.addDependent(id, firstName, lastName, dob, relationship);
            response.setData(dependent);
            response.setMessage("Dependent added successfully");
        } catch (EmployeeNotFoundException e) {
            response.setSuccess(false);
            response.setError(e.getMessage());
        } catch (Exception e) {
            response.setSuccess(false);
            response.setError("Failed to add dependent");
        }
        return response;
    }

    @ApiOperation(value = "Get the cost of employee benefits")
    @ApiResponses(value = {
            @ApiResponse(code = 200, message = "Successfully retrieved cost", response = ApiResponse.class),
            @ApiResponse(code = 404, message = "Employee not found"),
            @ApiResponse(code = 500, message = "Internal server error")
    })
    @GetMapping("/{id}/cost")
    public ApiResponse<EmployeeDTO> getCost(@PathVariable int id) {
        ApiResponse<EmployeeDTO> response = new ApiResponse<>();
        try {
            EmployeeDTO employee = employeeService.calculateCosts(id);
            response.setData(employee);
            response.setMessage("Cost retrieved successfully");
        } catch (EmployeeNotFoundException e) {
            response.setSuccess(false);
            response.setError(e.getMessage());
        } catch (Exception e) {
            response.setSuccess(false);
            response.setError("Failed to retrieve cost");
        }
        return response;
    }
    public EmployeeController(EmployeeService employeeService) {
        this.employeeService = employeeService;
    }
    
    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        var employees = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }
}
