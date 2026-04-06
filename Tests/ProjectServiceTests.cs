using FreelanceManagementSystem.Services;
using FreelanceManagementSystem.Models;
using Xunit;

namespace FreelanceManagementSystem.Tests;

public class ProjectServiceTests
{
    [Fact]
    public void CreateProject_ValidData_ShouldCreateProject()
    {
        // Arrange
        var service = new ProjectService();

        // Act
        var project = service.CreateProject("Website Redesign", "client-123", "Modern website");

        // Assert
        Assert.NotNull(project);
        Assert.Equal("Website Redesign", project.Name);
        Assert.Equal("client-123", project.ClientId);
        Assert.Equal("Modern website", project.Description);
        Assert.Equal("In Progress", project.Status);
        Assert.NotEmpty(project.Id);
    }

    [Fact]
    public void CreateProject_EmptyName_ShouldThrowException()
    {
        // Arrange
        var service = new ProjectService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.CreateProject("", "client-123", "Description"));
    }

    [Fact]
    public void CreateProject_EmptyClientId_ShouldThrowException()
    {
        // Arrange
        var service = new ProjectService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.CreateProject("Project Name", "", "Description"));
    }

    [Fact]
    public void GetAllProjects_ShouldReturnAllProjects()
    {
        // Arrange
        var service = new ProjectService();
        service.CreateProject("Project 1", "client-1", "Desc 1");
        service.CreateProject("Project 2", "client-2", "Desc 2");

        // Act
        var projects = service.GetAllProjects();

        // Assert
        Assert.Equal(2, projects.Count);
    }

    [Fact]
    public void AddDeadlineToProject_ValidData_ShouldAddDeadline()
    {
        // Arrange
        var service = new ProjectService();
        var project = service.CreateProject("Project 1", "client-1", "Desc");
        var futureDate = DateTime.Now.AddDays(7);

        // Act
        service.AddDeadlineToProject(project.Id, futureDate, "Delivery deadline");

        // Assert
        var updatedProject = service.GetProjectById(project.Id);
        Assert.NotNull(updatedProject.Deadline);
        Assert.Equal(futureDate.Date, updatedProject.Deadline.DueDate.Date);
    }

    [Fact]
    public void AddDeadlineToProject_PastDate_ShouldThrowException()
    {
        // Arrange
        var service = new ProjectService();
        var project = service.CreateProject("Project 1", "client-1", "Desc");
        var pastDate = DateTime.Now.AddDays(-7);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.AddDeadlineToProject(project.Id, pastDate, "Deadline"));
    }

    [Fact]
    public void UpdateProjectStatus_ValidStatus_ShouldUpdateStatus()
    {
        // Arrange
        var service = new ProjectService();
        var project = service.CreateProject("Project 1", "client-1", "Desc");

        // Act
        service.UpdateProjectStatus(project.Id, "Completed");

        // Assert
        var updatedProject = service.GetProjectById(project.Id);
        Assert.Equal("Completed", updatedProject.Status);
    }

    [Fact]
    public void UpdateProjectStatus_InvalidStatus_ShouldThrowException()
    {
        // Arrange
        var service = new ProjectService();
        var project = service.CreateProject("Project 1", "client-1", "Desc");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.UpdateProjectStatus(project.Id, "Invalid Status"));
    }

    [Fact]
    public void GetProjectCount_ShouldReturnCorrectCount()
    {
        // Arrange
        var service = new ProjectService();
        service.CreateProject("Project 1", "client-1", "Desc 1");
        service.CreateProject("Project 2", "client-2", "Desc 2");
        service.CreateProject("Project 3", "client-3", "Desc 3");

        // Act
        var count = service.GetProjectCount();

        // Assert
        Assert.Equal(3, count);
    }
}
