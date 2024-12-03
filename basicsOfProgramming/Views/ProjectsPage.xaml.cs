using basicsOfProgramming.ViewModels;

namespace basicsOfProgramming.Views;

public partial class ProjectsPage : ContentPage
{
    public ProjectsPage()
    {
        InitializeComponent();

        // Устанавливаем DataContext на экземпляр ProjectViewModel
        BindingContext = new ProjectViewModel();
    }
}