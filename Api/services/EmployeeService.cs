import java.time.LocalDate;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

public class EmployeeService {
    private EmployeeDAO employeeDAO;
    private DependentDAO dependentDAO;
    private static final double EMPLOYEE_BASE_COST = 1000.0;
    private static final double DEPENDENT_BASE_COST = 500.0;

    public EmployeeService(EmployeeDAO employeeDAO, DependentDAO dependentDAO) {
        this.employeeDAO = employeeDAO;
        this.dependentDAO = dependentDAO;
    }

    private final double employeeBaseCost;
    private final double dependentBaseCost;

    // Constructor injection
    public EmployeeService(EmployeeDAO employeeDAO, DependentDAO dependentDAO,
                           @Value("${employee.base.cost}") double employeeBaseCost,
                           @Value("${dependent.base.cost}") double dependentBaseCost) {
        this.employeeDAO = employeeDAO;
        this.dependentDAO = dependentDAO;
        this.employeeBaseCost = employeeBaseCost;
        this.dependentBaseCost = dependentBaseCost;
    }

    public EmployeeDTO createEmployee(string name, string firstName, string lastName, double salary, string dateOfBirth) {
        
        EmployeeDTO employee = new EmployeeDTO();
        employee.setFirstName(firstName);
        employee.setLastName(lastName);
        employee.setSalary(salary);
        LocalDate dob = LocalDate.parse(dateOfBirth); 
        employee.setDateOfBirth(dob);
        return employeeDAO.createEmployee(employee);
    }

    public DependentDTO addDependent(int employeeId, String firstName, String lastName, LocalDate dateOfBirth, Relationship relationship) {
        EmployeeDTO employee = employeeDAO.getEmployeeById(employeeId);
        DependentDTO dependent = dependentDAO.addDependent(employeeId, firstName, lastName, dateOfBirth, relationship);
        employee.addDependent(dependent);
        employeeDAO.updateEmployee(employee);
        return dependent;
    }

    public EmployeeDTO calculateCosts(int employeeId) {
        EmployeeDTO employee = employeeDAO.getEmployeeById(employeeId);

        double employeeCost = EMPLOYEE_BASE_COST * (employee.getName().startsWith("A") ? 0.9 : 1.0);
        double dependentCost = 0;

        for (DependentDTO dep : employee.getDependents()) {
            dependentCost += DEPENDENT_BASE_COST * (dep.getFirstName().startsWith("A") ? 0.9 : 1.0);
        }

        double annualCost = employeeCost + dependentCost;
        double paycheckDeduction = annualCost / 26;

        employee.setAnnualCost(annualCost);
        employee.setPaycheckDeduction(paycheckDeduction);
        employeeDAO.updateEmployee(employee);

        return employee;
    }

    public EmployeeDTO getEmployeeById(int id) {
        return employeeDAO.getEmployeeById(id);
    }
}
