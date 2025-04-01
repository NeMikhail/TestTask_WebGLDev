using MAEngine;
using UnityEngine;
using Zenject;

namespace GameCoreModule
{

    public class PoolsOperator : IAction, IInitialisation, ICleanUp
    {
        private PoolsContainer _poolsContainer;
        private GameEventBus _gameEventBus;

        [Inject]
        public void Construct(PoolsContainer poolsContainer, GameEventBus gameEventBus)
        {
            _poolsContainer = poolsContainer;
            _gameEventBus = gameEventBus;
        }

        public void Initialisation()
        {
            _poolsContainer.Initialize();
            _gameEventBus.OnSpawnObjectFromPool += SpawnObject;
            _gameEventBus.OnSpawnRotatedObjectFromPool += SpawnObject;
        }

        public void Cleanup()
        {
            _poolsContainer.CleanUp();
            _gameEventBus.OnSpawnObjectFromPool -= SpawnObject;
            _gameEventBus.OnSpawnRotatedObjectFromPool -= SpawnObject;
        }



        public void SpawnObject(PrefabID prefabID, Vector3 position,
            GameObjectSpawnCallback callback)
        {
            IPool pool = _poolsContainer.PoolsDict.GetValue(prefabID);
            GameObject go = pool.Pop(position);
            callback.SetObject(go, pool);

        }

        public void SpawnObject(PrefabID prefabID, Vector3 position, Quaternion rotation,
            GameObjectSpawnCallback callback)
        {
            IPool pool = _poolsContainer.PoolsDict.GetValue(prefabID);
            GameObject go = pool.Pop(position, rotation);
            callback.SetObject(go, pool);
        }

    }
}
