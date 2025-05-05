using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFThemingDemo.Models;

namespace WPFThemingDemo.ViewModels
{
    public class ThemesDemoViewModel : ViewModelBase
    {
        private ObservableCollection<string> _comboItems;
        private ObservableCollection<ComboItemModel> _comboItemModels;
        private ComboItemModel _selectedComboItem;

        public ThemesDemoViewModel()
        {
            // 初始化简单组合框数据
            ComboItems = new ObservableCollection<string>
            {
                "北京",
                "上海",
                "广州",
                "深圳",
                "杭州",
                "南京",
                "成都",
                "重庆",
                "武汉",
                "西安"
            };

            // 初始化复杂组合框数据
            ComboItemModels = new ObservableCollection<ComboItemModel>
            {
                new ComboItemModel(1, "苹果"),
                new ComboItemModel(2, "香蕉"),
                new ComboItemModel(3, "橙子"),
                new ComboItemModel(4, "葡萄"),
                new ComboItemModel(5, "西瓜"),
                new ComboItemModel(6, "芒果")
            };

            // 设置默认选中项
            SelectedComboItem = ComboItemModels[0];
        }

        public ObservableCollection<string> ComboItems
        {
            get => _comboItems;
            set => SetProperty(ref _comboItems, value);
        }

        public ObservableCollection<ComboItemModel> ComboItemModels
        {
            get => _comboItemModels;
            set => SetProperty(ref _comboItemModels, value);
        }

        public ComboItemModel SelectedComboItem
        {
            get => _selectedComboItem;
            set => SetProperty(ref _selectedComboItem, value);
        }
    }
} 