using Boo.Lang;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager inputManager;

        private readonly List<InputDevice> _devices = new List<InputDevice>();

        public int numberOfDevices;
//        private void Start()
//        {
//            InputUser.listenForUnpairedDeviceActivity = 1;
//            InputUser.onUnpairedDeviceUsed += UnpairedDeviceUsed;
//
//            inputManager.onPlayerJoined += PlayerJoined;
//            inputManager.onPlayerLeft += PlayerLeft;
//        }
//
//        private void OnDisable()
//        {
//            inputManager.onPlayerJoined -= PlayerJoined;
//            inputManager.onPlayerLeft -= PlayerLeft;
//            InputUser.onUnpairedDeviceUsed -= UnpairedDeviceUsed;
//        }

        private void UnpairedDeviceUsed(InputControl inputControl, InputEventPtr inputEventPtr)
        {
            if (_devices.Contains(inputControl.device))
            {
                //Debug.Log("Device already in use");
                return;
            }

            Debug.Log("Unpaired Device");
            PlayerInput.Instantiate(inputManager.playerPrefab, inputManager.playerCount,
                pairWithDevice: inputControl.device);
            if (inputControl.device == Keyboard.current.device)
                _devices.Add(Mouse.current.device);
            if (inputControl.device == Mouse.current.device)
                _devices.Add(Keyboard.current.device);

            _devices.Add(inputControl.device);
            numberOfDevices = _devices.Count;
        }

        private void PlayerJoined(PlayerInput playerInput)
        {
            Debug.Log("Player joined");
        }

        private void PlayerLeft(PlayerInput playerInput)
        {
            Debug.Log("Player left");
        }
    }
}