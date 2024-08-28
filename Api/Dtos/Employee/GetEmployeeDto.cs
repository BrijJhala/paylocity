import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class EmployeeDTO {
    private int id;
    private String firstName;
    private String lastName;
    private double salary;  // Changed to double to match Java's standard type
    private LocalDate dateOfBirth;
    private List<DependentDTO> dependents = new ArrayList<>();

    // Getters and Setters
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public double getSalary() {
        return salary;
    }

    public void setSalary(double salary) {
        this.salary = salary;
    }

    public LocalDate getDateOfBirth() {
        return dateOfBirth;
    }

    public void setDateOfBirth(LocalDate dateOfBirth) {
        this.dateOfBirth = dateOfBirth;
    }

    public List<DependentDTO> getDependents() {
        return dependents;
    }

    public void setDependents(List<DependentDTO> dependents) {
        this.dependents = dependents;
    }

    // Additional methods if needed, e.g., addDependent
    public void addDependent(DependentDTO dependent) {
        this.dependents.add(dependent);
    }
}
