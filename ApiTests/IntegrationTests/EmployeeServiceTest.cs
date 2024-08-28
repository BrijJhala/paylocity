import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

import java.time.LocalDate;

public class EmployeeServiceTest {

    @Mock
    private EmployeeDAO employeeDAO;

    @Mock
    private DependentDAO dependentDAO;

    @InjectMocks
    private EmployeeService employeeService;

    @BeforeEach
    public void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    public void testCreateEmployee() {
        EmployeeDTO mockEmployee = new EmployeeDTO();
        mockEmployee.setId(1);
        mockEmployee.setFirstName("John");
        mockEmployee.setLastName("Doe");

        when(employeeDAO.createEmployee(anyString())).thenReturn(mockEmployee);

        EmployeeDTO employee = employeeService.createEmployee("John Doe");

        assertNotNull(employee);
        assertEquals("John", employee.getFirstName());
        verify(employeeDAO, times(1)).createEmployee(anyString());
    }

    @Test
    public void testAddDependent() {
        EmployeeDTO mockEmployee = new EmployeeDTO();
        mockEmployee.setId(1);
        mockEmployee.setFirstName("John");

        DependentDTO mockDependent = new DependentDTO();
        mockDependent.setId(1);
        mockDependent.setFirstName("Jane");

        when(employeeDAO.getEmployeeById(anyInt())).thenReturn(mockEmployee);
        when(dependentDAO.addDependent(anyInt(), anyString(), anyString(), any(LocalDate.class), any(Relationship.class)))
                .thenReturn(mockDependent);

        DependentDTO dependent = employeeService.addDependent(1, "Jane", "Doe", LocalDate.of(2010, 1, 1), Relationship.Child);

        assertNotNull(dependent);
        assertEquals("Jane", dependent.getFirstName());
        verify(dependentDAO, times(1)).addDependent(anyInt(), anyString(), anyString(), any(LocalDate.class), any(Relationship.class));
    }

    @Test
    public void testCalculateCosts() {
        EmployeeDTO mockEmployee = new EmployeeDTO();
        mockEmployee.setId(1);
        mockEmployee.setFirstName("John");

        when(employeeDAO.getEmployeeById(anyInt())).thenReturn(mockEmployee);

        EmployeeDTO employee = employeeService.calculateCosts(1);

        assertNotNull(employee);
        verify(employeeDAO, times(1)).getEmployeeById(anyInt());
        verify(employeeDAO, times(1)).updateEmployee(any(EmployeeDTO.class));
    }

    @Test
    public void testEmployeeNotFound() {
        when(employeeDAO.getEmployeeById(anyInt())).thenThrow(new EmployeeNotFoundException(999));

        assertThrows(EmployeeNotFoundException.class, () -> {
            employeeService.getEmployeeById(999);
        });
    }
}
