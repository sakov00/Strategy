using System;
using System.Collections.Generic;
using System.Linq;
using _General.Scripts.AllAppData;
using _Project.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

namespace _Project.Scripts.GameObjects.EnemyRoads
{
    public class EnemyRoadView : MonoBehaviour
    {
        [SerializeField] private List<SplineAnimate> _splineAnimates;
        [SerializeField] private List<TextMeshPro> _enemyIcons;
        [SerializeField] private float _distanceBetweenIcons = 4;
        
        public void Initialize()
        {
            for (var i = 0; i < _splineAnimates.Count; i++)
            {
                var splineAnimate = _splineAnimates[i];
                splineAnimate.StartOffset = i / (float)_splineAnimates.Count;
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
                var countEnemy = roundEnemyList[AppData.LevelData.CurrentRound].enemies.Count(x => x.enemyType == enemyValues[i]);
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