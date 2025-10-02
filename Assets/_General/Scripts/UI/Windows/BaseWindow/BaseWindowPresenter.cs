using System;
using _General.Scripts._VContainer;
using _General.Scripts.Enums;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace _General.Scripts.UI.Windows.BaseWindow
{
    public abstract class BaseWindowPresenter : MonoBehaviour
    {
        [Inject] protected WindowsManager WindowsManager { get; private set; }
        
        protected CompositeDisposable Disposables;
        
        public abstract BaseWindowModel Model { get; }
        public abstract BaseWindowView View { get; }

        public virtual void Initialize()
        {
            Dispose();
            InjectManager.Inject(this);
            Disposables = new CompositeDisposable();
            View.Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            Disposables?.Dispose();
            View.Dispose();
        }
    }
}