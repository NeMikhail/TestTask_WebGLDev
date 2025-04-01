using MAEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreModule
{
    public class GameEventBus
    {
        private Action<PrefabID> _onCreatePool;
        private Action<PrefabID, Vector3, GameObjectSpawnCallback> _onSpawnObjectFromPool;
        private Action<PrefabID, Vector3, Quaternion, GameObjectSpawnCallback> _onSpawnRotatedObjectFromPool;

        private Action<PrefabID, Vector3, Transform, GameObjectSpawnCallback> _onSpawnObject;
        private Action<PrefabID, Vector3, GameObjectSpawnCallback> _onSpawnObjectWithoutRoot;
        private Action<PrefabID, Vector3, Quaternion, Transform, GameObjectSpawnCallback> _onSpawnRotatedObject;

        private Action<GameState> _onStateChanged;
        private Action _onGameOver;

        public Action<PrefabID> OnCreatePool 
        { get => _onCreatePool; set => _onCreatePool = value; }
        public Action<PrefabID, Vector3, GameObjectSpawnCallback> OnSpawnObjectFromPool 
        { get => _onSpawnObjectFromPool; set => _onSpawnObjectFromPool = value; }
        public Action<PrefabID, Vector3, Quaternion, GameObjectSpawnCallback> OnSpawnRotatedObjectFromPool 
        { get => _onSpawnRotatedObjectFromPool; set => _onSpawnRotatedObjectFromPool = value; }
        public Action<PrefabID, Vector3, Transform, GameObjectSpawnCallback> OnSpawnObject 
        { get => _onSpawnObject; set => _onSpawnObject = value; }
        public Action<PrefabID, Vector3, GameObjectSpawnCallback> OnSpawnObjectWithoutRoot 
        { get => _onSpawnObjectWithoutRoot; set => _onSpawnObjectWithoutRoot = value; }
        public Action<PrefabID, Vector3, Quaternion, Transform, GameObjectSpawnCallback> OnSpawnRotatedObject 
        { get => _onSpawnRotatedObject; set => _onSpawnRotatedObject = value; }
        public Action<GameState> OnStateChanged 
        { get => _onStateChanged; set => _onStateChanged = value; }
        public Action OnGameOver 
        { get => _onGameOver; set => _onGameOver = value; }
    }
}
