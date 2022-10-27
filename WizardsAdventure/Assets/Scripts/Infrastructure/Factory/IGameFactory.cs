using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateWizardsSpawner(GameObject at);
        GameObject CreateShopInterface();
        GameObject CreateArenaDisposer();
    }
}