using EmployeeClient.Models;
using EmployeeClient.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace EmployeeClient
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeApiService _apiService;
        private ObservableCollection<Employee> _employees;
        public MainWindow()
        {
            InitializeComponent();
            _apiService = new EmployeeApiService();
            _employees = new ObservableCollection<Employee>();
            EmployeeDataGrid.ItemsSource = _employees;
            LoadEmployees();
        }
        /// <summary>
        /// 社員一覧を再読込する
        /// </summary>
        private async void LoadEmployees()
        {
            var employees = await _apiService.GetAllAsync();
            _employees.Clear();
            foreach (var emp in employees)
            {
                _employees.Add(emp);
            }
        }
        /// <summary>
        /// 追加ボタン
        /// </summary>
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee
            {
                Name = NameTextBox.Text,
                Department = DepartmentTextBox.Text,
                HireDate = HireDatePicker.SelectedDate ?? System.DateTime.Now
            };
            await _apiService.AddAsync(employee);
            LoadEmployees();
        }
        /// <summary>
        /// 更新ボタン
        /// </summary>
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is not Employee selected)
            {
                MessageBox.Show("更新する社員を選択してください");
                return;
            }
            selected.Name = NameTextBox.Text;
            selected.Department = DepartmentTextBox.Text;
            selected.HireDate = HireDatePicker.SelectedDate ?? selected.HireDate;
            await _apiService.UpdateAsync(selected);
            LoadEmployees();
        }
        /// <summary>
        /// 削除ボタン
        /// </summary>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is not Employee selected)
            {
                MessageBox.Show("削除する社員を選択してください");
                return;
            }
            await _apiService.DeleteAsync(selected.Id);
            LoadEmployees();
        }
        /// <summary>
        /// 再読込ボタン
        /// </summary>
        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
    }
}
