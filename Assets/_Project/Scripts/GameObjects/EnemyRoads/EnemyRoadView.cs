using System;
using System.Collections.Generic;
using System.Linq;
using _General.Scripts._VContainer;
using _General.Scripts.AllAppData;
using _Project.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using VContainer;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameObjects.EnemyRoads
{
    public class EnemyRoadView : MonoBehaviour
    {
        [Inject] private AppData _appData;
        
        [SerializeField] private SplineAnimate _animatePrefab = new();
        [SerializeField] private List<TextMeshPro> _enemyIcons;
        [SerializeField] private float _distanceBetweenIcons = 4;
        
        public void Initialize(SplineContainer splineContainer)
        {
            InjectManager.Inject(this);
            var roadLength = splineContainer.Spline.GetLength();
            var countAnimateObjects = roadLength / _distanceBetweenIcons;
            for (int i = 0; i < countAnimateObjects; i++)
            {
                var splineAnimate = Instantiate(_animatePrefab, transform);
                splineAnimate.Container = splineContainer;
                splineAnimate.StartOffset = i / countAnimateObjects;
                splineAnimate.Play();
            }
        }
        
        public void RefreshInfoRound(SplineContainer splineContainer, List<EnemyGroup> roundEnemyList)
        {
            var enemyValues = Enum.GetValues(typeof(CharacterType))
                .Cast<CharacterType>()
                .Where(e => e.ToString().Contains("Enemy"))
                .ToArray();
            for (int i = 0; i < enemyValues.Length; i++)
            {
                var countEnemy = roundEnemyList[_appData.LevelData.CurrentRound].enemies.Count(x => x.enemyType == enemyValues[i]);
                if (countEnemy > 0)
                {
                    _enemyIcons[i].gameObject.SetActive(true);
                    _enemyIcons[i].text = countEnemy.ToString();
                }
                else
                    _enemyIcons[i].gameObject.SetActive(false); 
            }
            
            var wayLenght = splineContainer.Spline.GetLength();
            var activeIcons = _enemyIcons.Where(x => x.gameObject.activeInHierarchy).ToList();
            for (int i = 0; i < activeIcons.Count; i++)
            {
                var percentIcon = (wayLenght - (i + 1) * _distanceBetweenIcons) / wayLenght;
                activeIcons[i].transform.position = splineContainer.EvaluatePosition(percentIcon);
                activeIcons[i].transform.position += new Vector3(0, 0.1f, 0);
            }
        }
    }
}