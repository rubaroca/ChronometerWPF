using ChronometerWPF.Models;
using ChronometerWPF.ViewModels;
using System.Windows;

namespace ChronometerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChronometerViewModel(new Chronometer());
        }
    }
}
