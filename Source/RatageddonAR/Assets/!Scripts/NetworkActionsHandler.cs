using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class NetworkActionsHandler : NetworkBehaviour
{
    [SerializeField] private ObjectSpawner _spawner;

    private void Awake()
    {
        //_spawner.objectSpawned += TryToSpawn;
    }

    public void TryToSpawn(GameObject obj)
    {
        if (!IsOwner) return;
        if (obj != null)
            if (obj.TryGetComponent<NetworkObject>(out NetworkObject net))
                SpawnServerRpc(net.PrefabIdHash);
    }

    [ServerRpc]
    private void SpawnServerRpc(uint hash)
    {
        var a = NetworkManager.NetworkConfig.Prefabs.Prefabs.FirstOrDefault(x => x.SourcePrefabGlobalObjectIdHash == hash);
        Debug.Log(a.Prefab.name);
        a.Prefab.GetComponent<NetworkObject>().Spawn();
        Debug.Log("Spawned");
    }
}
