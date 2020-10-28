using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using Scripts.MapSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Scripts.UI
{
    public class GameSceneManager : Singleton<GameSceneManager>, ISpawner
    {
#pragma warning disable 0649
        [SerializeField] private AdressableInstantiate adressableInstantiate;
        [SerializeField] private PlayerController player;
        [SerializeField] private GameSceneUI gameSceneUI;
        [SerializeField] private Terrain terrain;
        [SerializeField, Range(10, 100)] private float randomCircleMinRadius;
        [SerializeField, Range(10, 100)] private float randomCircleMaxRadius;
        [SerializeField, Space] private int maxTargets;
        [SerializeField] private AssetReference targetPrefab;
#pragma warning restore 0649
        public GameSceneUI GameSceneUI => gameSceneUI;
        public PlayerController Player => player;
        public AdressableInstantiate AdressableInstantiate => adressableInstantiate ?? (adressableInstantiate = GetComponent<AdressableInstantiate>());

        private void Start()
        {
            GameSceneUI.Init(player.transform, player.MiniMapCamera, Spawn);

            Spawn();

        }

        public void Spawn()
        {
            var rndTargets = Random.Range(1, maxTargets + 1);
            for (int i = 0; i < rndTargets; i++)
            {
                AdressableInstantiate.AdressableInst(targetPrefab, transform, InitAfterInstantiate);
            }
        }

        public void InitAfterInstantiate(AsyncOperationHandle<GameObject> obj)
        {
            var target = obj.Result;
            target.GetComponent<MarkedObject>().Init();
        }

        public void ChangeObjectPosition(Transform targetTransform)
        {
            Vector3 randomPosition = MathUtilities.RandomPointInAnnulus(player.transform.position, randomCircleMinRadius, randomCircleMaxRadius);

            var minterrainX = terrain.terrainData.bounds.min.x + 50;
            var maxterrainX = terrain.terrainData.bounds.max.x - 50;

            var minterrainZ = terrain.terrainData.bounds.min.z + 50;
            var maxterrainZ = terrain.terrainData.bounds.max.z - 50;

            randomPosition.x = Mathf.Clamp(randomPosition.x, minterrainX, maxterrainX);
            randomPosition.y = 0.5f;
            randomPosition.z = Mathf.Clamp(randomPosition.z, minterrainZ, maxterrainZ);


            targetTransform.position = randomPosition;
        }
    }
}