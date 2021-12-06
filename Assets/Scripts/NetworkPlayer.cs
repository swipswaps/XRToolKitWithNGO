using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : NetworkBehaviour
{
    public override void OnNetworkSpawn() => EnableOrDisableClientInput();

    private void EnableOrDisableClientInput()
    {
        var clientMoveProvider = GetComponent<NetworkMoveProvider>();
        var clientControllers = GetComponentsInChildren<ActionBasedController>();
        var clientTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();

        var clientCamera = GetComponentInChildren<Camera>();

        if (IsClient && !IsOwner)
        {
            clientCamera.enabled = false;
            clientMoveProvider.enableInputActions = false;
            clientTurnProvider.enableTurnAround = false;
            clientTurnProvider.enableTurnLeftRight = false;

            foreach (var input in clientControllers)
            {
                input.enableInputActions = false;
                input.enableInputTracking = false;
            }
        }
    }
}
