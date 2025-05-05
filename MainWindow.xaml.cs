using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFThemingDemo.Services;
using WPFThemingDemo.Views;

namespace WPFThemingDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        // 确保主题服务已初始化
        var themeService = ThemeService.Instance;
        
        // 默认加载主页内容
        LoadMainContent();
    }
    
    /// <summary>
    /// 加载主页面内容
    /// </summary>
    private void LoadMainContent()
    {
        // 默认显示主题演示页面
        MainFrame.Navigate(new ThemesDemoPage());
    }
    
    /// <summary>
    /// 导航到资源系统示例页面
    /// </summary>
    private void ResourceDemoButton_Click(object sender, RoutedEventArgs e)
    {
        // 导航到资源示例页面
        MainFrame.Navigate(new ResourceDemoPage());
    }
}