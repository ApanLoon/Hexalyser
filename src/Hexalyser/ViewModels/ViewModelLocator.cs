using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Hexalyser.Models;

namespace Hexalyser.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IModelService, DesignModelService>();
            }
            else
            {
                SimpleIoc.Default.Register<IModelService, ModelService>();
            }

            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        public MainWindowViewModel MainWindowViewModel => SimpleIoc.Default.GetInstance<MainWindowViewModel>();
    }
}