using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public interface ICoroutineRunner //этот интерфейс нужен для запуска любих корутин 
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}