import java.util.List;
import java.util.stream.Collectors;

public class DependentService {

    private final DependentDAO dependentDAO;

    public DependentService(DependentDAO dependentDAO) {
        this.dependentDAO = dependentDAO;
    }

    public DependentDTO getDependentById(int id) {
        return dependentDAO.getDependentById(id);
    }

    public List<DependentDTO> getAllDependents() {
        return dependentDAO.getAllDependents();
    }
}
