using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Suika.Scripts.Input
{
    public class InputController : MonoBehaviour
    {
        public bool IsActive { get; set; } = true;
        public ReactiveCommand<Vector2> TouchBeganCommand { get; } = new();
        public ReactiveCommand<Vector2> TouchMovedCommand { get; } = new();
        public ReactiveCommand<Vector2> TouchEndedCommand { get; } = new();
        private Pointer _activePointer;
        private List<RaycastResult> _raycastResults = new();

        private void Update()
        {
            if (!IsActive) return;
            HandleTouch();
        }

        private void HandleTouch()
        {
            if (_activePointer == null)
            {
                _activePointer = ResolveNewPointer();
                if (_activePointer == null) return;

                if (IsOverlapWithUI(_activePointer))
                {
                    _activePointer = null;
                    return;
                }
                
                TouchBeganCommand.Execute(_activePointer.position.ReadValue());
                return;
            }

            if (!_activePointer.added)
            {
                _activePointer = null;
                return;
            }

            Vector2 position = _activePointer.position.ReadValue();
            if (_activePointer.press.isPressed)
            {
                TouchMovedCommand.Execute(position);
            }
            else
            {
                TouchEndedCommand.Execute(position);
                _activePointer = null;
            }
        }

        private Pointer ResolveNewPointer()
        {
            if (Touchscreen.current != null && Touchscreen.current.press.wasPressedThisFrame)
                return Touchscreen.current;
            if (Mouse.current != null && Mouse.current.press.wasPressedThisFrame)
                return Mouse.current;
            return null;
        }

        private bool IsOverlapWithUI(Pointer pointer)
        {
            if (EventSystem.current == null || pointer == null)
                return false;
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = pointer.position.ReadValue()
            };
            _raycastResults.Clear();
            EventSystem.current.RaycastAll(eventData, _raycastResults);
            return _raycastResults.Count > 0;
        }

        private void OnDestroy()
        {
            TouchBeganCommand.Dispose();
            TouchMovedCommand.Dispose();
            TouchEndedCommand.Dispose();
        }
    }
}