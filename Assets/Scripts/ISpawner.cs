using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
public interface ISpawner
{
    void InitAfterInstantiate(AsyncOperationHandle<GameObject> obj);
}