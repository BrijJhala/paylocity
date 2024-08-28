import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

public class EmployeeControllerTest {

    @Mock
    private EmployeeService employeeService;

    @InjectMocks
    private EmployeeController employeeController;

    @BeforeEach
    public void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    public void testCreateEmployee() {
        EmployeeDTO mockEmployee = new EmployeeDTO();
        mockEmployee.setId(1);
        mockEmployee.setFirstName("John");

        when(employeeService.createEmployee(anyString())).thenReturn(mockEmployee);

        ResponseEntity<ApiResponse<EmployeeDTO>> response = employeeController.createEmployee("John Doe");

        assertEquals(HttpStatus.OK, response.getStatusCode());
        assertNotNull(response.getBody());
        assertEquals("John", response.getBody().getData().getFirstName());
    }

    @Test
    public void testAddDependent() {
        DependentDTO mockDependent = new DependentDTO();
        mockDependent.setId(1);
        mockDependent.setFirstName("Jane");

        when(employeeService.addDependent(anyInt(), anyString(), anyString(), any(LocalDate.class), any(Relationship.class)))
                .thenReturn(mockDependent);

        ResponseEntity<ApiResponse<DependentDTO>> response = employeeController.addDependent(1, "Jane", "Doe", "2010-01-01", Relationship.Child);

        assertEquals(HttpStatus.OK, response.getStatusCode());
        assertNotNull(response.getBody());
        assertEquals("Jane", response.getBody().getData().getFirstName());
    }

    @Test
    public void testGetCost() {
        EmployeeDTO mockEmployee = new EmployeeDTO();
        mockEmployee.setId(1);
        mockEmployee.setFirstName("John");

        when(employeeService.calculateCosts(anyInt())).thenReturn(mockEmployee);

        ResponseEntity<ApiResponse<EmployeeDTO>> response = employeeController.getCost(1);

        assertEquals(HttpStatus.OK, response.getStatusCode());
        assertNotNull(response.getBody());
        assertEquals("John", response.getBody().getData().getFirstName());
    }

    @Test
    public void testGetEmployeeById() {
        EmployeeDTO mockEmployee = new EmployeeDTO();
        mockEmployee.setId(1);
        mockEmployee.setFirstName("John");

        when(employeeService.getEmployeeById(anyInt())).thenReturn(mockEmployee);

        ResponseEntity<ApiResponse<EmployeeDTO>> response = employeeController.getEmployeeById(1);

        assertEquals(HttpStatus.OK, response.getStatusCode());
        assertNotNull(response.getBody());
        assertEquals("John", response.getBody().getData().getFirstName());
    }

    @Test
    public void testEmployeeNotFound() {
        when(employeeService.getEmployeeById(anyInt())).thenThrow(new EmployeeNotFoundException(999));

        ResponseEntity<ApiResponse<EmployeeDTO>> response = employeeController.getEmployeeById(999);

        assertEquals(HttpStatus.NOT_FOUND, response.getStatusCode());
        assertNotNull(response.getBody());
        assertFalse(response.getBody().isSuccess());
    }
}
