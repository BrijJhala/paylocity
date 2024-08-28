import java.util.HashMap;
import java.util.Map;

public class DependentDAO {
    private Map<Integer, DependentDTO> dependents = new HashMap<>(); // here it will be DB operations
    private int currentId = 1;

    public DependentDTO addDependent(int employeeId, String firstName, String lastName, LocalDate dateOfBirth, Relationship relationship) {
        DependentDTO dependent = new DependentDTO();
        dependent.setId(currentId++);
        dependent.setFirstName(firstName);
        dependent.setLastName(lastName);
        dependent.setDateOfBirth(dateOfBirth);
        dependent.setRelationship(relationship);
        dependents.put(dependent.getId(), dependent);
        return dependent;
    }

    public DependentDTO getDependentById(int id) {
        DependentDTO dependent = dependents.get(id);
        if (dependent == null) {
            throw new DependentNotFoundException(id);
        }
        return dependent;
    }

    public Map<Integer, DependentDTO> getDependentsByEmployeeId(int employeeId) {
        Map<Integer, DependentDTO> employeeDependents = new HashMap<>();
        for (DependentDTO dep : dependents.values()) {
            if (dep.getId() == employeeId) {
                employeeDependents.put(dep.getId(), dep);
            }
        }
        return employeeDependents;
    }
}
