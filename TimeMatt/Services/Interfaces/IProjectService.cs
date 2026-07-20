using TimeMatt.Models;

namespace TimeMatt.Services;

public interface IProjectService
{
    List<Project> GetAll();
    Project? GetById(int id);
    List<Project> GetByClientId(int clientId);
    List<ProjectFile> GetFilesByProjectId(int projectId);
    List<ProjectComment> GetCommentsByProjectId(int projectId);
}
