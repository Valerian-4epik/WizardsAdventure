using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory //задача фактори создавать и инициализировать обьекты
    {
        private readonly IAssets _assetses;

        public GameFactory(IAssets assetses) //IAssetProvider = это по факту зависимость, ассетпровиде предоставляет услуги
        {
            _assetses = assetses;
        }
        
        public GameObject CreateWizardsSpawner(GameObject at) => 
            _assetses.Instantiate(AssetPath.WizardsSpawnerPath, at.transform.position);

        public GameObject CreateShopInterface() => 
            _assetses.Instantiate(AssetPath.ShopInterface);

        public GameObject CreateArenaDisposer() => 
            _assetses.Instantiate(AssetPath.ArenaDisposer);
        
        public GameObject CreateCameraFollower() => 
            _assetses.Instantiate(AssetPath.CameraFollower);
        
        public GameObject CreateLevelFinishInterface() => 
            _assetses.Instantiate(AssetPath.LevelFinishInterface);
    }
}