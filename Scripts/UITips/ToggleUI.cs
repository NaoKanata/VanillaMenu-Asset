using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

using VVVanilla.Core.Constants;
// using VVVanilla.Status;

namespace VVVanilla.Menu
{
    [Serializable]
    public class UnityEventBool : UnityEvent<bool> {}

    public class ToggleUI : MonoBehaviour, IUITips
    {
        [SerializeField]
        string targetKey;
        [SerializeField]
        Animator animator;

        bool _currentStatus = false;

        public string GetTargetKey() {
            return targetKey;
        }

        public void Set() {
            _currentStatus = !_currentStatus;
            MenuManager.instance.SetStatusEvent.Invoke(targetKey,_currentStatus.ToString());
        }

        public void SetValue(string value) {
            _currentStatus = bool.Parse(value);
            animator.SetBool("Flag", _currentStatus);
        }

    }
}
