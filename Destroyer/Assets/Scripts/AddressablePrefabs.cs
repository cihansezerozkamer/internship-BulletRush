using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class AddressablePrefabs : MonoBehaviour
{
    [SerializeField]
    AssetReferenceGameObject _Player;
    GameObject _PlayerObject;
    private void Start()
    {
        Addressables.InstantiateAsync(_Player).Completed += AddressablePrefabs_Completed;
    }

    private void AddressablePrefabs_Completed(AsyncOperationHandle<GameObject> obj)
    {
        _PlayerObject = obj.Result;
    }
}
