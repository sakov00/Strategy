using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace _Project.Scripts.UI.Elements
{
    public class SplineAnimateStartPos : MonoBehaviour
    {
        [SerializeField] private List<SplineAnimate> _splineAnimates;

        private void Awake()
        {
            for (int i = 0; i < _splineAnimates.Count; i++)
            {
                var splineAnimate = _splineAnimates[i];
                splineAnimate.StartOffset = i / (float)_splineAnimates.Count;
            }
        }
    }
}