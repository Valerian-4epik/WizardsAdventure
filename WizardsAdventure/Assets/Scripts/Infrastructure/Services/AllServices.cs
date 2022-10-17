namespace Infrastructure.Services
{
    public class AllServices//по факты ЭТО Сервис ЛОКАТОР
    {//что из себя это предствляет, сервис который как правило содержит в себе статику
        private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance = new AllServices()); //проверяем на null  

        public void RegisterSingle<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService //для каждого дженерика сгенерируется собственный класс
        {
            public static TService ServiceInstance;
        }
    //сервис локатор решает задачу получения места под реализацию по запросу некоторого интерфейса
    }
}