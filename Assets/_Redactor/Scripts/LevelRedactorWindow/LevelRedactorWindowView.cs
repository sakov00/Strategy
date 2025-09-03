using _General.Scripts.UI.Windows;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Redactor.Scripts.LevelRedactorWindow
{
    public class LevelRedactorWindowView : BaseWindowView
    {
        [Header("Presenter")]
        [SerializeField] private LevelRedactorWindowViewModel _viewModel;
        
        [Header("Dev")]
        [SerializeField] private Button _saveLevelButton;
        [SerializeField] private Button _loadLevelButton;
        [SerializeField] private TMP_InputField _selectLevelInputField;
        
        protected override BaseWindowViewModel BaseViewModel => _viewModel;

        private void Start()
        {
            _saveLevelButton.gameObject.SetActive(true);
            _loadLevelButton.gameObject.SetActive(true);
            _selectLevelInputField.gameObject.SetActive(true);
            _saveLevelButton.onClick.AddListener(() => _viewModel.SaveLevelCommand.Execute(int.Parse(_selectLevelInputField.text)));
            _loadLevelButton.onClick.AddListener(() => _viewModel.LoadLevelCommand.Execute(int.Parse(_selectLevelInputField.text)));
        }
        
        public override Tween Show()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => gameObject.SetActive(true));
            sequence.Append(_canvasGroup.DOFade(1f, 0.5f).From(0));
            return sequence;
        }

        public override Tween Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(0f, 0.5f).From(1));
            sequence.AppendCallback(() => gameObject.SetActive(false));
            return sequence;
        }
    }
}