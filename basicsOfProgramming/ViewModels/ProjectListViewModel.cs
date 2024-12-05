using System.Collections.ObjectModel;
using basicsOfProgramming.Models.Projects;
using basicsOfProgramming.Services;
using basicsOfProgramming.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace basicsOfProgramming.ViewModels;

public partial class ProjectListViewModel : BaseViewModel
{
    public ObservableCollection<Project> Projects => ProjectStore.Instance.Projects;

    [ObservableProperty] private Project? _selectedProject;

    partial void OnSelectedProjectChanged(Project value)
    {
        SelectProjectCommand.Execute(value);
        SelectedProject = null;
    }

    public Command AddProjectCommand { get; }
    private Command<Project> SelectProjectCommand { get; }

    public ProjectListViewModel()
    {
        Title = "Projects";
        AddProjectCommand = new Command(OnAddProject);
        SelectProjectCommand = new Command<Project>(OnSelectProject);
    }

    private async void OnAddProject()
    {
        // Navigate to ProjectDetailPage to add a new project
        await Shell.Current.GoToAsync(nameof(ProjectDetailPage));
    }

    private async void OnSelectProject(Project project)
    {
        // Передать выбранный проект на страницу деталей
        var navParam = new Dictionary<string, object> { { "Project", project } };
        await Shell.Current.GoToAsync(nameof(ProjectDetailPage), navParam);
    }
}