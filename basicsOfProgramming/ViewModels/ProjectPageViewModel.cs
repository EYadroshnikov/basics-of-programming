using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using basicsOfProgramming.Models.Projects;
using basicsOfProgramming.Models.Tasks;
using System.Collections.ObjectModel;
using basicsOfProgramming.Views;

namespace basicsOfProgramming.ViewModels;

public partial class ProjectPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Project> _projects = new();

    [ObservableProperty]
    private Project? _selectedProject;

    public ProjectPageViewModel()
    {
        Title = "Projects";
        LoadProjects();
    }

    private void LoadProjects()
    {
        Projects.Add(new Project("Sample Project", "Description", DateTime.Today, DateTime.Today.AddDays(30)));
    }

    [RelayCommand]
    private async Task SelectProject()
    {
        if (SelectedProject == null) return;

        var projectPage = new ProjectPage(new ProjectPageViewModel(SelectedProject));
        await Shell.Current.Navigation.PushAsync(projectPage);
    }
}