using System;
using System.Collections.Generic;
using _General.Scripts._VContainer;
using _General.Scripts.Interfaces;
using _General.Scripts.Registries;
using _Project.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.LevelEnvironment.Terrain
{
    public class TerrainController : MonoBehaviour, ISavableController, IClearData
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        
        [SerializeField] private TerrainModel _model;
        [SerializeField] private MeshFilter _meshFilter;
        
        private void Start()
        {
            InjectManager.Inject(this);
            _objectsRegistry.Register(this);
        }
        
        public ISavableModel GetSavableModel()
        {            
            if (_meshFilter != null && _meshFilter.mesh != null)
            {
                Mesh mesh = _meshFilter.mesh;

                _model.Vertices = new List<Vector3>(mesh.vertices);
                _model.Normals = new List<Vector3>(mesh.normals);
                _model.UVs = new List<Vector2>(mesh.uv);
                _model.Triangles = Array.ConvertAll(mesh.triangles, t => (ushort)t);
            }
            _model.Position = transform.position;
            _model.Rotation = transform.rotation;
            return _model;
        }
        
        public void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is TerrainModel terrainModel)
            {
                _model = terrainModel;
                if (_meshFilter != null && _model.Vertices != null && _model.UVs != null && _model.Triangles != null)
                {
                    Mesh mesh = new Mesh();
                    mesh.SetVertices(_model.Vertices);
                    mesh.SetUVs(0, _model.UVs);
                    mesh.SetTriangles(_model.Triangles, 0);
                    mesh.SetNormals(_model.Normals);
                    _meshFilter.mesh = mesh;
                }
            }
        }

        private void OnDestroy()
        {
            ClearData();
        }

        public void ClearData()
        {
            _objectsRegistry.Unregister(this);
        }
        
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}