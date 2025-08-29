using _General.Scripts.Enums;
using DG.Tweening;
using UnityEngine;

namespace _General.Scripts.UI.Windows
{
    public abstract class BaseWindowView : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        protected abstract BaseWindowViewModel BaseViewModel { get; }
        
        public virtual Tween Show()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => BaseViewModel.IsPaused.Value = true);
            sequence.AppendCallback(() => gameObject.SetActive(true));
            sequence.Append(_canvasGroup.DOFade(1f, 0.5f).From(0));
            return sequence;
        }

        public virtual Tween Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(0f, 0.5f).From(1));
            sequence.AppendCallback(() => gameObject.SetActive(false));
            sequence.AppendCallback(() => BaseViewModel.IsPaused.Value = false);
            return sequence;
        }
        
        public WindowType WindowType => BaseViewModel.WindowType.Value;
    }
}