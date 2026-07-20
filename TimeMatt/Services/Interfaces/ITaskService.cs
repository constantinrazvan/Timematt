using TimeMatt.Models;

namespace TimeMatt.Services;

public interface ITaskService
{
    List<ProjectTask> GetAll();
    ProjectTask? GetById(int id);
    List<ProjectTask> GetByProjectId(int projectId);
    List<ProjectTask> GetPending();
    int GetProgressForProject(int projectId, int fallbackPercent);
}
