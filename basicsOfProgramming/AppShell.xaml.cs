using basicsOfProgramming.Views;

namespace basicsOfProgramming;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ProjectDetailPage), typeof(ProjectDetailPage));
    }
}