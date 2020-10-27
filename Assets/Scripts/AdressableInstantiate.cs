using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressableInstantiate : MonoBehaviour
{
    public void AdressableInst(AssetReference assetRef, Transform parent, Action<AsyncOperationHandle<GameObject>> action)
    {
        StartCoroutine(InstGObject(assetRef, parent, action));
    }

    private IEnumerator InstGObject(AssetReference assetRef, Transform parent, Action<AsyncOperationHandle<GameObject>> action)
    {
        assetRef.LoadAssetAsync<GameObject>();
        var actionHandler = assetRef.InstantiateAsync(parent);
        actionHandler.Completed += action;
        yield return actionHandler;
    }
}