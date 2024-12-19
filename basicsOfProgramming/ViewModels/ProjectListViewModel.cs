using System.Collections.ObjectModel;
using basicsOfProgramming.Models.Projects;
using basicsOfProgramming.Services;
using basicsOfProgramming.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace basicsOfProgramming.ViewModels;

public partial class ProjectListViewModel : BaseViewModel
{
    private bool _isNavigating = false;
    public ObservableCollection<Project> Projects => ProjectStore.Instance.Projects;

    [ObservableProperty] private Project? _selectedProject;

    partial void OnSelectedProjectChanged(Project? value)
    {
        if (value == null || _isNavigating) return;
        _isNavigating = true;
        OnSelectProject(value);
    }

    public Command AddProjectCommand { get; }
    private Command<Project> SelectProjectCommand { get; }
    public Command SaveProjectsToFileCommand { get; }
    public Command LoadProjectsFromFileCommand { get; }

    public ProjectListViewModel()
    {
        Title = "Projects";
        AddProjectCommand = new Command(OnAddProject);
        SelectProjectCommand = new Command<Project>(OnSelectProject);
        SaveProjectsToFileCommand = new Command(OnSaveProjectsToFile);
        LoadProjectsFromFileCommand = new Command(OnLoadProjectsFromFile);
        foreach (var project in Projects)
        {
            SubscribeToProjectEvents(project);
        }
    }
    
    private void SubscribeToProjectEvents(Project project)
    {
        project.ProjectChanged += OnProjectChanged;
    }

    private void OnProjectChanged(object sender, ProjectEventArgs e)
    {
        Console.WriteLine($"UI update: {e.Message}, Task: {e.Task.Title}");
        OnPropertyChanged(nameof(Projects));
    }
    
    private async void OnSaveProjectsToFile()
    {
        try
        {
            ProjectStore.Instance.SaveProjectsToFile();
            await Shell.Current.DisplayAlert("Success", "Projects have been saved successfully.", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Error while saving projects: {ex.Message}", "OK");
        }
    }

    private async void OnLoadProjectsFromFile()
    {
        try
        {
            ProjectStore.Instance.LoadProjectsFromFile();
            await Shell.Current.DisplayAlert("Success", "Projects have been loaded successfully.", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Error while loading projects: {ex.Message}", "OK");
        }
    }

    private async void OnAddProject()
    {
        await Shell.Current.GoToAsync(nameof(ProjectDetailPage));
    }

    private async void OnSelectProject(Project project)
    {
        var navParam = new Dictionary<string, object> { { "Project", project } };
        await Shell.Current.GoToAsync(nameof(ProjectDetailPage), navParam);
        
        SelectedProject = null;
        _isNavigating = false;
    }
}