using _General.Scripts.AllAppData;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEditor.Build.Reporting;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Abstract
{
    public abstract class BuildController : ObjectController
    {
        [Inject] protected AppData AppData;
        [Inject] protected BuildPool BuildPool;
        
        private Vector3 _originalScale;
    }
}