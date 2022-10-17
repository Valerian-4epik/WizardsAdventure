using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets //задача ассетс провайдера загружить ресурсы
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at) //также мне нужно задать точку где создавать обьект
        {
            var prefab = Resources.Load<GameObject>(path); //указываем путь 
            return Object.Instantiate(prefab, at, Quaternion.identity); //а потом инстантиируем
        }
    }
}