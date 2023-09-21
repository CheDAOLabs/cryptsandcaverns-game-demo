using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using System.Runtime.InteropServices;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// This component, added on an empty object in your level will handle the generation of a unique and randomized tilemap
    /// </summary>
    public class CCLevelGenerator : CCTilemapGenerator
    {
        [FormerlySerializedAs("GenerateOnStart")]
        [Header("TopDown Engine Settings")]
        /// Whether or not this level should be generated automatically on Awake
        [Tooltip("Whether or not this level should be generated automatically on Awake")]
        public bool GenerateOnAwake = false;

        [Header("Bindings")]
        /// the Grid on which to work
        [Tooltip("the Grid on which to work")]
        public Grid TargetGrid;

        /// the tilemap containing the walls
        [Tooltip("the tilemap containing the walls")]
        public Tilemap ObstaclesTilemap;

        /// the tilemap containing the walls' shadows
        [Tooltip("the tilemap containing the walls' shadows")]
        public MMTilemapShadow WallsShadowTilemap;

        /// the level manager
        [Tooltip("the level manager")] public LevelManager TargetLevelManager;

        [Header("Spawn")]
        /// the object at which the player will spawn
        [Tooltip("the object at which the player will spawn")]
        public Transform InitialSpawn;

        /// the exit of the level
        [Tooltip("the exit of the level")] public Transform Exit;

        /// the minimum distance that should separate spawn and exit.
        [Tooltip("the minimum distance that should separate spawn and exit.")]
        public float MinDistanceFromSpawnToExit = 2f;

        protected const int _maxIterationsCount = 100;

        private List<GameObject> spawnedPrefabs;

        /// <summary>
        /// On awake we generate our level if needed
        /// </summary>
        protected virtual void Awake()
        {
            if (GenerateOnAwake)
            {
                Generate();
            }
        }

        [DllImport("__Internal")]
        private static extern void Hello();

        [DllImport("__Internal")]
        private static extern int AddNumbers(int x, int y);

        [DllImport("__Internal")]
        private static extern string GetGrid();

        /// <summary>
        /// Generates a new level
        /// </summary>
        public override void Generate()
        {
            Debug.LogError("CC Generate");


            // string grid_str = GetGrid();
            string grid_str = "{\"data\":[\"1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1\",\"1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1\",\"0,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1\",\"1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,0,1,1\",\"1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,1\",\"1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1\",\"1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,1,1,0,0,0,0,1,1\",\"1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,1,0,1,1\",\"1,1,1,0,0,0,1,1,1,1,1,1,1,0,0,1,1,1,1,0,1,0,0,1\",\"1,1,1,1,0,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0,1,1,0,1\",\"1,1,1,1,0,0,1,1,1,1,1,0,0,0,1,1,1,1,1,0,1,1,0,1\",\"1,1,1,0,0,1,1,1,0,0,0,0,1,0,1,1,1,1,1,0,0,1,0,1\",\"1,1,1,0,1,1,1,1,1,0,1,1,1,0,0,1,1,1,1,0,1,1,0,1\",\"1,1,1,0,1,1,1,1,1,0,1,1,1,1,0,0,1,1,1,0,0,0,0,1\",\"1,0,0,0,1,1,1,1,1,0,0,1,1,1,1,0,1,1,1,0,1,1,0,1\",\"1,0,1,1,1,1,1,1,1,1,0,1,1,1,0,0,0,0,1,0,1,0,0,0\",\"1,0,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,0,1,0,1,0,1,1\",\"1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0\",\"1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,0,0,0,0,0,1,1\",\"1,1,1,1,0,1,1,1,0,1,1,1,1,1,1,0,0,0,0,1,0,0,1,1\",\"1,1,0,0,0,1,1,1,0,1,1,1,1,1,1,0,0,1,0,0,0,0,1,1\",\"1,1,1,1,0,1,1,1,0,0,1,1,1,0,0,0,1,1,1,0,0,0,1,1\",\"1,1,1,1,0,1,1,1,0,0,1,1,0,0,0,1,1,1,1,0,0,0,1,1\",\"1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\"],\"size\":24,\"name\":\"Forest of the Golem\",\"id\":\"107\",\"entities\":[{\"x\":2,\"y\":20,\"entityType\":0},{\"x\":20,\"y\":17,\"entityType\":0},{\"x\":17,\"y\":16,\"entityType\":1},{\"x\":10,\"y\":11,\"entityType\":0},{\"x\":21,\"y\":8,\"entityType\":1},{\"x\":21,\"y\":7,\"entityType\":1},{\"x\":3,\"y\":3,\"entityType\":0},{\"x\":2,\"y\":1,\"entityType\":0}]}\n";
            Debug.LogError(grid_str);
            ArrayData arrayData = JsonUtility.FromJson<ArrayData>(grid_str);

            int width = arrayData.GetWidth();
            int height = arrayData.GetHeight();
            GridWidth.x = width;
            GridWidth.y = width;
            GridHeight.x = height;
            GridHeight.y = height;

            base.arrayData = arrayData;
            base.Generate();
            HandleWallsShadow();
            PlaceEntryAndExit(arrayData);
            ResizeLevelManager();

            List<Vector3Int> entities = arrayData.GetEntities();
            spawnedPrefabs = new List<GameObject>();
            for (var i = 0; i < entities.Count; i++)
            {
                Vector3Int randomCoordinate = entities[i];
                randomCoordinate += MMTilemapGridRenderer.ComputeOffset(width - 1, height - 1);
                Vector3 world_pos = ObstaclesTilemap.CellToWorld(randomCoordinate) + (TargetGrid.cellSize / 2);
                if (randomCoordinate.z == 1)
                {
                    GameObject spawnedPrefab = Instantiate(CoinPicker, world_pos, Quaternion.identity);
                    spawnedPrefabs.Add(spawnedPrefab);
                }

                if (randomCoordinate.z == 2)
                {
                    GameObject spawnedPrefab = Instantiate(Ninga, world_pos, Quaternion.identity);
                    spawnedPrefabs.Add(spawnedPrefab);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (GameObject spawnedPrefab in spawnedPrefabs)
            {
                Destroy(spawnedPrefab);
            }
        }

        /// <summary>
        /// Resizes the level manager's bounds to match the new level
        /// </summary>
        protected virtual void ResizeLevelManager()
        {
            BoxCollider boxCollider = TargetLevelManager.GetComponent<BoxCollider>();

            Bounds bounds = ObstaclesTilemap.localBounds;
            boxCollider.center = bounds.center;
            boxCollider.size = new Vector3(bounds.size.x, bounds.size.y, boxCollider.size.z);
        }


        public static Vector2 GetRandomPosition(int[,] define, Tilemap targetTilemap, Grid grid, int width, int height,
            bool shouldBeFilled = true, int maxIterations = 1000)
        {
            int iterationsCount = 0;
            Vector3Int randomCoordinate = Vector3Int.zero;

            while (iterationsCount < maxIterations)
            {
                randomCoordinate.x = UnityEngine.Random.Range(0, width);
                randomCoordinate.y = UnityEngine.Random.Range(0, height);
                //

                bool hasTile = targetTilemap.HasTile(randomCoordinate);
                if (hasTile == shouldBeFilled && define[randomCoordinate.x, randomCoordinate.y] == 0)
                {
                    randomCoordinate += MMTilemapGridRenderer.ComputeOffset(width - 1, height - 1);
                    return targetTilemap.CellToWorld(randomCoordinate) + (grid.cellSize / 2);
                }

                iterationsCount++;
            }

            return Vector2.zero;
        }

        /// <summary>
        /// Moves the spawn and exit to empty places
        /// </summary>
        protected virtual void PlaceEntryAndExit(ArrayData arrayData)
        {
            UnityEngine.Random.InitState(GlobalSeed);
            int width = UnityEngine.Random.Range(GridWidth.x, GridWidth.y);
            int height = UnityEngine.Random.Range(GridHeight.x, GridHeight.y);

            int[,] define = arrayData.GetGrid();
            Vector3 spawnPosition = GetRandomPosition(define, ObstaclesTilemap, TargetGrid, width, height, false,
                width * height * 2);
            Vector3 exitPosition = GetRandomPosition(define, ObstaclesTilemap, TargetGrid, width, height, false,
                width * height * 2);
            ;
            
            GameObject spawnedPrefab = Instantiate(Weapon, spawnPosition, Quaternion.identity);
            spawnedPrefabs.Add(spawnedPrefab);

            InitialSpawn.transform.position = spawnPosition;
            Exit.transform.position = exitPosition;
        }

        /// <summary>
        /// Copies the contents of the Walls layer to the WallsShadows layer to get nice shadows automatically
        /// </summary>
        protected virtual void HandleWallsShadow()
        {
            if (WallsShadowTilemap != null)
            {
                WallsShadowTilemap.UpdateShadows();
            }
        }
    }
}