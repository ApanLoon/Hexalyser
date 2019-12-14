using GalaSoft.MvvmLight.Command;

namespace Hexalyser.ViewModels.Commands
{
    public class Command
    {
        public string Name { get; set; }
        public RelayCommand Execute { get; set; }
    }
}
