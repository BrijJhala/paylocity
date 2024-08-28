import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class EmployeeDAOTest {

    private EmployeeDAO employeeDAO;

    @BeforeEach
    public void setUp() {
        employeeDAO = new EmployeeDAO();
    }

    @Test
    public void testCreateEmployee() {
        EmployeeDTO employee = employeeDAO.createEmployee("John Doe");
        assertNotNull(employee);
        assertEquals("John Doe", employee.getName());
    }

    @Test
    public void testGetEmployeeById() {
        EmployeeDTO employee = employeeDAO.createEmployee("Jane Doe");
        EmployeeDTO retrievedEmployee = employeeDAO.getEmployeeById(employee.getId());
        assertNotNull(retrievedEmployee);
        assertEquals(employee.getId(), retrievedEmployee.getId());
    }

    @Test
    public void testUpdateEmployee() {
        EmployeeDTO employee = employeeDAO.createEmployee("John Doe");
        employee.setFirstName("Johnny");
        employeeDAO.updateEmployee(employee);

        EmployeeDTO updatedEmployee = employeeDAO.getEmployeeById(employee.getId());
        assertEquals("Johnny", updatedEmployee.getFirstName());
    }

    @Test
    public void testEmployeeNotFound() {
        assertThrows(EmployeeNotFoundException.class, () -> {
            employeeDAO.getEmployeeById(999);
        });
    }
}
