using _General.Scripts.Registries;
using _General.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Redactor.Scripts.LevelRedactorWindow
{
    public class LevelRedactorWindowViewModel : BaseWindowViewModel
    {
        [Inject] private RedactorManager _redactorManager;
        [Inject] private ObjectsRegistry _objectsRegistry;
     
        [SerializeField] private LevelRedactorWindowModel _model;
        protected override BaseWindowModel BaseModel => _model;
        
        public ReactiveCommand<int> SaveLevelCommand { get; } = new();
        public ReactiveCommand<int> LoadLevelCommand { get; } = new();

        protected override void Awake()
        {
            base.Awake();
            
            SaveLevelCommand.Subscribe(levelIndex => _redactorManager.SaveLevel(levelIndex)).AddTo(this);
            LoadLevelCommand.Subscribe(levelIndex => _redactorManager.StartLevel(levelIndex).Forget()).AddTo(this);
        }
    }
}