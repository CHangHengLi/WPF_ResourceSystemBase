# WPF 主题切换演示项目

这个项目演示了一个完整的WPF主题系统实现，支持动态切换亮色/暗色主题。

## 主要特性

1. **完整的资源层次结构**
   - Colors.xaml - 基础颜色定义
   - Themes\Light.xaml 和 Themes\Dark.xaml - 主题特定颜色
   - Brushes.xaml - 基于颜色的笔刷资源
   - Styles.xaml - 控件样式
   - Converters.xaml - 值转换器资源

2. **动态资源绑定**
   - 所有样式使用DynamicResource绑定资源，确保主题切换时能实时更新

3. **主题切换服务**
   - ThemeService实现单例模式，提供全局主题管理
   - 正确的资源加载顺序，避免资源覆盖问题
   - 视觉树刷新机制，确保UI组件正确更新

4. **MVVM架构**
   - 视图和逻辑分离，使用命令绑定
   - 基础视图模型实现属性变更通知

## 主题系统设计原则

1. **资源加载顺序至关重要**：
   ```
   Colors.xaml -> Theme\{Current}.xaml -> Brushes.xaml -> Styles.xaml
   ```
   
2. **使用DynamicResource而非StaticResource**：
   - 颜色值使用DynamicResource引用
   - 笔刷使用DynamicResource引用颜色
   - 样式使用DynamicResource引用笔刷
   
3. **主题切换机制**：
   - 清空并重新加载资源字典，确保顺序正确
   - 强制刷新视觉树，确保所有控件都应用新主题

## 运行项目

1. 打开解决方案文件 `WPFThemingDemo.sln`
2. 构建并运行项目
3. 点击界面右上角的主题切换按钮，体验亮色/暗色主题切换

## 项目结构

```
WPFThemingDemo/
├── Converters/                  # 值转换器
│   ├── BooleanToVisibilityConverter.cs
│   └── StringToBooleanConverter.cs
├── Resources/                   # 资源文件
│   ├── Colors.xaml              # 基础颜色定义
│   ├── Brushes.xaml             # 笔刷资源
│   ├── Styles.xaml              # 控件样式
│   ├── Converters.xaml          # 转换器资源
│   └── Themes/                  # 主题文件
│       ├── Light.xaml           # 亮色主题
│       └── Dark.xaml            # 暗色主题
├── Services/                    # 服务
│   └── ThemeService.cs          # 主题服务
├── ViewModels/                  # 视图模型
│   ├── MainViewModel.cs         # 主窗口视图模型
│   ├── RelayCommand.cs          # 命令实现
│   └── ViewModelBase.cs         # 基础视图模型
├── App.xaml                     # 应用程序定义
├── App.xaml.cs                  # 应用程序代码
├── MainWindow.xaml              # 主窗口UI
└── MainWindow.xaml.cs           # 主窗口代码
``` 