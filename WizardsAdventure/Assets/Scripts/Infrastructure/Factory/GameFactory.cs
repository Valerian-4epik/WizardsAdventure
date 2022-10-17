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
        
        public GameObject CreateHeroesSpawner(GameObject at) => 
            _assetses.Instantiate(AssetPath.HeroesSpawnerPath, at.transform.position);
    }
}