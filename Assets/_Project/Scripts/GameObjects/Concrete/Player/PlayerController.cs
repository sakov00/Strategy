using _General.Scripts._GlobalLogic;
using _General.Scripts.AllAppData;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.ActionSystems;
using _Project.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Concrete.Player
{
    public class PlayerController : UnitController, ISelectable
    {
        [Inject] private AppData _appData;
        [Inject] private GameTimer _gameTimer;
        [field: SerializeField] public PlayerModel Model { get; private set; }
        [field: SerializeField] public PlayerView View { get; private set; }
        protected override UnitModel UnitModel => Model;
        protected override UnitView UnitView => View;
        
        private DamageSystem _damageSystem;
        private DetectionAim _detectionAim;
        private PlayerMovementSystem _playerMovementSystem;
        private RegenerationHpSystem _regenerationHpSystem;

        public override UniTask InitializeAsync()
        {
            base.InitializeAsync();
            
            Model.CurrentHealth = Model.MaxHealth;
            
            if(Model.CurrentTimeResurrection != 0)
                _gameTimer.OnEverySecond += TryReturnToGame;
            if(Model.CurrentTimeNoDamage != 0)
                _gameTimer.OnEverySecond += DisableNoDamage;
            
            _playerMovementSystem = new PlayerMovementSystem(Model, View, transform);
            _detectionAim = new DetectionAim(Model, transform);
            _damageSystem = new DamageSystem(Model, View, transform);
            _regenerationHpSystem = new RegenerationHpSystem(Model, View);
            View.Initialize();
            return default;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _detectionAim?.DetectAim();
            _damageSystem?.Attack();
        }

        private void Update()
        {
            _playerMovementSystem?.MoveTo(_appData.LevelData.MoveDirection);
        }

        public override ISavableModel GetSavableModel()
        {
            Model.SavePosition = transform.position;
            Model.SaveRotation = transform.rotation;
            return Model;
        }

        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is PlayerModel playerModel)
            {
                Model = playerModel;
            }
        }

        public override void Killed()
        {
            Dispose(false);
            _gameTimer.OnEverySecond += TryReturnToGame;
        }

        private void TryReturnToGame()
        {
            Model.CurrentTimeResurrection++;
            View.UpdateLoadBar(Model.CurrentTimeResurrection, Model.NeedTimeResurrection);
            if (Model.CurrentTimeResurrection == Model.NeedTimeResurrection)
            {
                _gameTimer.OnEverySecond -= TryReturnToGame;
                Model.CurrentTimeResurrection = 0;
                InitializeAsync();
                Model.IsNoDamageable = true;
                _gameTimer.OnEverySecond += DisableNoDamage;
            }
        }
        
        private void DisableNoDamage()
        {
            Model.CurrentTimeNoDamage++;
            if (Model.CurrentTimeNoDamage == Model.NeedTimeNoDamage)
            {
                _gameTimer.OnEverySecond -= DisableNoDamage; 
                Model.CurrentTimeNoDamage = 0;
                Model.IsNoDamageable = false;
            }
        }

        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            base.Dispose(returnToPool, clearFromRegistry);
            Model.CurrentTimeNoDamage = 0;
            Model.CurrentTimeResurrection = 0;
            _gameTimer.OnEverySecond -= DisableNoDamage;
            _gameTimer.OnEverySecond -= TryReturnToGame;
            _detectionAim = null;
            _playerMovementSystem = null;
            _regenerationHpSystem?.Dispose();
            _damageSystem?.Dispose();
        }
    }
}