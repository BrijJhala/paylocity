import java.util.HashMap;
import java.util.Map;

// EmployeeDAO.java
public class EmployeeDAO {
    private Map<Integer, EmployeeDTO> employees = new HashMap<>();
    private int currentId = 1;

    public EmployeeDTO createEmployee(EmployeeDTO dto) {
        EmployeeDTO employee = new EmployeeDTO();        
        employees.put(employee.getId(), dto);
        return employeeDTO;
    }

    public EmployeeDTO getEmployeeById(int id) {
        EmployeeDTO employee = employees.get(id);
        if (employee == null) {
            throw new EmployeeNotFoundException(id);
        }
        return employee;
    }

    public void updateEmployee(EmployeeDTO employee) {
        employees.put(employee.getId(), employee);
    }
}


