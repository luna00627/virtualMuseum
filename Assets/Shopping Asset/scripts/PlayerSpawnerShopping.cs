using Fusion;
using UnityEngine;

public class PlayerSpawnerShopping : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, new Vector3(49.23f, 165.81f, 579.08f), Quaternion.identity, player);
        }
    }
}