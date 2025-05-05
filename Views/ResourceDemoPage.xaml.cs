using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFThemingDemo.Views
{
    /// <summary>
    /// ResourceDemoPage.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceDemoPage : Page
    {
        // 用于绑定到ListBox的运行时数据
        public ObservableCollection<string> RuntimeItems { get; } = new ObservableCollection<string>
        {
            "运行时数据项 1",
            "运行时数据项 2",
            "运行时数据项 3"
        };

        public ResourceDemoPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 返回按钮点击事件处理
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // 导航回主题演示页面
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new ThemesDemoPage());
            }
        }

        /// <summary>
        /// 尝试访问不同作用域的资源
        /// </summary>
        private void AccessResourceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = "";

                // 1. 访问元素级资源 (应该能成功)
                if (ScopeDemo.Resources.Contains("LocalBrush"))
                {
                    var brush = ScopeDemo.Resources["LocalBrush"] as SolidColorBrush;
                    result += $"元素级资源颜色: {brush?.Color.ToString()}\n";
                }
                else
                {
                    result += "找不到元素级资源 'LocalBrush'\n";
                }

                // 2. 访问页面级资源 (应该能成功)
                if (this.Resources.Contains("PageSpecificBrush"))
                {
                    var brush = this.Resources["PageSpecificBrush"] as SolidColorBrush;
                    result += $"页面级资源颜色: {brush?.Color.ToString()}\n";
                }
                else
                {
                    result += "找不到页面级资源 'PageSpecificBrush'\n";
                }

                // 3. 尝试直接访问局部作用域之外的资源 (应该失败)
                try
                {
                    var localBrush = this.Resources["LocalBrush"] as SolidColorBrush;
                    result += $"页面级中的LocalBrush: {localBrush?.Color.ToString()}\n";
                }
                catch (Exception ex)
                {
                    result += $"页面级中无法访问LocalBrush: {ex.Message}\n";
                }

                // 4. 访问应用程序级资源 (应该成功)
                if (Application.Current.Resources.Contains("PrimaryBrush"))
                {
                    var brush = Application.Current.Resources["PrimaryBrush"] as SolidColorBrush;
                    result += $"应用程序级资源颜色: {brush?.Color.ToString()}\n";
                }
                else
                {
                    result += "找不到应用程序级资源 'PrimaryBrush'\n";
                }

                // 显示结果
                ResourceResult.Text = result;
            }
            catch (Exception ex)
            {
                ResourceResult.Text = $"访问资源时出错: {ex.Message}";
            }
        }

        /// <summary>
        /// 在代码中创建资源
        /// </summary>
        private void CreateResourceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 创建随机颜色
                var random = new Random();
                byte r = (byte)random.Next(256);
                byte g = (byte)random.Next(256);
                byte b = (byte)random.Next(256);
                Color randomColor = Color.FromRgb(r, g, b);

                // 创建一个新的画刷资源
                SolidColorBrush dynamicBrush = new SolidColorBrush(randomColor);
                
                // 添加到页面资源字典中
                this.Resources["DynamicBrush"] = dynamicBrush;

                // 显示结果
                ResourceResult.Text = $"已创建动态资源 'DynamicBrush'\n颜色值: {randomColor}";
                
                // 显示资源创建成功的视觉反馈
                ResourceDisplay.Fill = dynamicBrush;
            }
            catch (Exception ex)
            {
                ResourceResult.Text = $"创建资源时出错: {ex.Message}";
            }
        }

        /// <summary>
        /// 使用代码创建的资源
        /// </summary>
        private void UseResourceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Resources.Contains("DynamicBrush"))
                {
                    // 从资源字典中获取画刷
                    var brush = this.Resources["DynamicBrush"] as SolidColorBrush;
                    
                    // 改变按钮的背景色
                    UseResourceButton.Background = brush;
                    
                    // 显示成功信息
                    ResourceResult.Text = $"成功使用动态资源 'DynamicBrush'\n颜色值: {brush?.Color.ToString()}";
                }
                else
                {
                    ResourceResult.Text = "找不到动态资源 'DynamicBrush'，请先点击'创建资源'按钮";
                }
            }
            catch (Exception ex)
            {
                ResourceResult.Text = $"使用资源时出错: {ex.Message}";
            }
        }
    }
} 