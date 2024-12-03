using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using basicsOfProgramming.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using basicsOfProgramming.Models.Projects;

namespace basicsOfProgramming.ViewModels;

public partial class ProjectViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Project> _projects;

    [ObservableProperty]
    private Project? _selectedProject = null;

    [ObservableProperty]
    private string _projectName = string.Empty;

    [ObservableProperty]
    private string _projectDescription = string.Empty;

    [ObservableProperty]
    private DateTime _projectStartDate = DateTime.Now;

    [ObservableProperty]
    private DateTime _projectEndDate = DateTime.Now.AddMonths(1);

    [ObservableProperty]
    private ProjectPriority _projectPriority = ProjectPriority.Medium;
    
    public List<ProjectPriority> ProjectPriorities { get; }
    
    public bool CanAddProject => 
        !string.IsNullOrWhiteSpace(ProjectName) &&
        ProjectStartDate < ProjectEndDate;
    
    public ProjectViewModel()
    {
        Projects = new ObservableCollection<Project>();
        ProjectPriorities = Enum.GetValues(typeof(ProjectPriority)).Cast<ProjectPriority>().ToList();
        
        PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(ProjectName) || 
                e.PropertyName == nameof(ProjectDescription) || 
                e.PropertyName == nameof(ProjectStartDate) || 
                e.PropertyName == nameof(ProjectEndDate))
            {
                OnPropertyChanged(nameof(CanAddProject));
            }
        };
    }

    [RelayCommand]
    public void AddProject()
    {
        var newProject = new Project(ProjectName, ProjectDescription, ProjectStartDate, ProjectEndDate, ProjectPriority);
        
        Projects.Add(newProject);

        ProjectName = string.Empty;
        ProjectDescription = string.Empty;
        ProjectStartDate = DateTime.Now;
        ProjectEndDate = DateTime.Now.AddMonths(1);
        ProjectPriority = ProjectPriority.Medium;
    }
}