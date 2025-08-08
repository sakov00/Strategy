using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace _Project.Scripts.UI.Elements
{
    [RequireComponent(typeof(SplineContainer))]
    public class SplineStartPos : MonoBehaviour
    {
        [SerializeField] private SplineContainer _splineContainer;
        
        [SerializeField] private List<SplineAnimate> _splineAnimates;
        [SerializeField] private List<Transform> _enemyIcons;
        [SerializeField] private float _distanceBetweenIcons = 4;

        private void OnValidate()
        {
            _splineContainer ??= GetComponent<SplineContainer>();
        }

        private void Awake()
        {
            for (int i = 0; i < _splineAnimates.Count; i++)
            {
                var splineAnimate = _splineAnimates[i];
                splineAnimate.StartOffset = i / (float)_splineAnimates.Count;
            }
            
            var wayLenght = _splineContainer.Spline.GetLength();
            for (int i = 0; i < _enemyIcons.Count; i++)
            {
                var percentIcon = (wayLenght - (i + 1) * _distanceBetweenIcons) / wayLenght;
                _enemyIcons[i].position = _splineContainer.EvaluatePosition(percentIcon);
                _enemyIcons[i].position += new Vector3(0, 0.1f, 0);
            }
        }
    }
}