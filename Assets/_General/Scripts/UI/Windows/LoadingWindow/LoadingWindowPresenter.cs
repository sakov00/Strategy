using _General.Scripts.Enums;
using _General.Scripts.UI.Windows.BaseWindow;
using DG.Tweening;
using UnityEngine;

namespace _General.Scripts.UI.Windows.LoadingWindow
{
    public class LoadingWindowPresenter : BaseWindowPresenter
    {
        [SerializeField] private BaseWindowModel _model;
        [SerializeField] private LoadingWindowView _view;
        
        public override BaseWindowModel Model => _model;
        public override BaseWindowView View => _view;
    }
}