using _Project.Scripts.SO;
using _Project.Scripts.Windows.Presenters;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Windows
{
    public class WindowsManager
    {
        [Inject] private IObjectResolver resolver;
        [Inject] private WindowsConfig windowsConfig;
        
        public void OpenWindow<T>() where T : BaseWindowPresenter
        {
            var window = resolver.Resolve<T>();
        }
    }
}