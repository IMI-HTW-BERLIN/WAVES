using UnityEngine;
using UnityEngine.InputSystem;
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
        [SerializeField] private bool ignoreKeyboardAndMouse;

        private int _numberOfPlayers;

        private const float InputDelay = 0.5f;
        private float _inputGuardTimer;


        private void OnEnable()
        {
            InputUser.listenForUnpairedDeviceActivity = 1;
            InputUser.onUnpairedDeviceUsed += PairNewUser;
        }

        private void OnDisable() => InputUser.onUnpairedDeviceUsed -= PairNewUser;

        private void PairNewUser(InputControl controls, InputEventPtr eventPtr)
        {
            //Prevent two inputs to trigger at the same time e.g. mouse movement and controller
            if (Time.time < _inputGuardTimer + InputDelay) return;

            if (ignoreKeyboardAndMouse &&
                (controls.device == Keyboard.current.device ||
                 controls.device == Mouse.current.device)) return;

            PlayerInput.Instantiate(players[_numberOfPlayers], _numberOfPlayers, pairWithDevice: controls.device);
            _numberOfPlayers++;
            _inputGuardTimer = Time.time;
        }
    }
}