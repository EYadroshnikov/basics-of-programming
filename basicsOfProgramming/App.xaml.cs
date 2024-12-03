using basicsOfProgramming.Views;

namespace basicsOfProgramming;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new ProjectsPage();
    }
}