using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Windows
{
    public abstract class BaseWindowView : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected Button closeButton;
        
        public virtual Tween Show()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => gameObject.SetActive(true));
            sequence.Append(canvasGroup.DOFade(1f, 0.5f).From(0));
            return sequence;
        }

        public virtual Tween Hide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(canvasGroup.DOFade(0f, 0.5f).From(1));
            sequence.AppendCallback(() => gameObject.SetActive(false));
            return sequence;
        }
    }
}