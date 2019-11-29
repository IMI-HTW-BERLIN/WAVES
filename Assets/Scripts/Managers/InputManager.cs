using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace Managers
{
    /// <summary>
    /// This manager will instantiate the players. We use this manager instead of the <see cref="PlayerInputManager"/>
    /// due to some bugs that prevent multiple UI usage, caused by the PlayerControls duplication process.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [Header("Players")] [SerializeField] private GameObject[] players;

        [SerializeField] private int maxPlayers;

        private int _numberOfPlayers;


        private void OnEnable()
        {
            InputUser.listenForUnpairedDeviceActivity = 1;
            InputUser.onUnpairedDeviceUsed += PairNewUser;
        }

        private void OnDisable() => InputUser.onUnpairedDeviceUsed -= PairNewUser;

        private void PairNewUser(InputControl controls, InputEventPtr eventPtr)
        {
            if (_numberOfPlayers == maxPlayers) return;

            //Ignore Mouse-Movement
            if (!(controls is ButtonControl)) return;

            PlayerInput.Instantiate(players[_numberOfPlayers], _numberOfPlayers, pairWithDevice: controls.device);
            _numberOfPlayers++;
        }
    }
}