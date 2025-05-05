using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPFThemingDemo.Services
{
    public class ThemeService
    {
        private const string LightTheme = "Light";
        private const string DarkTheme = "Dark";
        
        public event EventHandler<string> ThemeChanged;
        
        public string CurrentTheme { get; private set; } = LightTheme;

        // 单例模式
        private static ThemeService _instance;
        public static ThemeService Instance => _instance ??= new ThemeService();

        private ThemeService()
        {
            // 私有构造函数
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        public void ToggleTheme()
        {
            string newTheme = CurrentTheme == LightTheme ? DarkTheme : LightTheme;
            ApplyTheme(newTheme);
        }

        /// <summary>
        /// 应用指定的主题
        /// </summary>
        /// <param name="themeName">主题名称（Light 或 Dark）</param>
        public void ApplyTheme(string themeName)
        {
            if (themeName != LightTheme && themeName != DarkTheme)
                throw new ArgumentException($"无效的主题名称: {themeName}。有效值为 {LightTheme} 或 {DarkTheme}");

            if (CurrentTheme == themeName)
                return;

            try
            {
                // 创建新的资源字典集合
                var newMergedDictionaries = new List<ResourceDictionary>();

                // 按照固定顺序添加资源字典，确保顺序一致
                // 先添加颜色定义
                newMergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri($"/WPFThemingDemo;component/Resources/Colors.xaml", UriKind.Relative)
                });

                // 再添加主题特定的颜色覆盖
                newMergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri($"/WPFThemingDemo;component/Resources/Themes/{themeName}.xaml", UriKind.Relative)
                });

                // 添加笔刷定义（依赖于颜色）
                newMergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri($"/WPFThemingDemo;component/Resources/Brushes.xaml", UriKind.Relative)
                });

                // 添加样式（依赖于笔刷）
                newMergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri($"/WPFThemingDemo;component/Resources/Styles.xaml", UriKind.Relative)
                });

                // 保存应用程序中没有Source的字典（内联资源）
                var app = Application.Current;
                var inlineResources = new List<ResourceDictionary>();
                foreach (var dict in app.Resources.MergedDictionaries)
                {
                    if (dict.Source == null)
                    {
                        inlineResources.Add(dict);
                    }
                }

                // 清空并重建资源字典集合
                app.Resources.MergedDictionaries.Clear();

                // 先添加所有有Source的资源字典（按正确顺序）
                foreach (var dict in newMergedDictionaries)
                {
                    app.Resources.MergedDictionaries.Add(dict);
                }

                // 再添加所有内联资源字典
                foreach (var dict in inlineResources)
                {
                    app.Resources.MergedDictionaries.Add(dict);
                }

                // 更新当前主题属性
                CurrentTheme = themeName;

                // 强制刷新所有窗口
                foreach (Window window in Application.Current.Windows)
                {
                    // 强制更新布局
                    window.UpdateLayout();

                    // 递归刷新所有元素
                    RefreshVisualTree(window);
                }

                // 触发主题改变事件
                ThemeChanged?.Invoke(this, themeName);

                Console.WriteLine($"主题已切换为: {themeName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"切换主题时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 递归刷新视觉树，确保主题资源更新
        /// </summary>
        private static void RefreshVisualTree(DependencyObject root)
        {
            // 触发资源重新应用
            if (root is FrameworkElement element)
            {
                // 强制重新应用样式
                if (element.Style != null)
                {
                    var style = element.Style;
                    element.Style = null;
                    element.Style = style;
                }
            }

            // 递归处理所有子元素
            int childCount = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(root, i);
                RefreshVisualTree(child);
            }
        }
    }
} 