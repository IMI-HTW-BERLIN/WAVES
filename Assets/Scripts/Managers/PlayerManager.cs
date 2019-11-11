using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager inputManager;

        private void Start()
        {
            inputManager.onPlayerJoined += PlayerJoined;
            inputManager.onPlayerLeft += PlayerLeft;

            InputUser.listenForUnpairedDeviceActivity = 1;
            InputUser.onUnpairedDeviceUsed += UnpairedDeviceUsed;
        }

        private void UnpairedDeviceUsed(InputControl inputControl, InputEventPtr inputEventPtr)
        {
            throw new NotImplementedException();
        }

        private void PlayerLeft(PlayerInput playerInput)
        {
            throw new NotImplementedException();
        }

        private void PlayerJoined(PlayerInput playerInput)
        {
            throw new NotImplementedException();
        }
    }
}