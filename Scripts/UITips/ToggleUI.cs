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
        Animator animator;
        [SerializeField]
        bool isUsingMonitoringData = false;
        [SerializeField]
        UnityEventBool sendValueFunc;

        /// <summary>
        /// 値が変わったときのイベント、基本的には MenuCard と接続する
        /// </summary>
        [SerializeField]
        UnityEvent<string, object> OnChangeStatus;

        bool _currentStatus = false;

        public void ChangeValue(string key) {
            _currentStatus = !_currentStatus;
            OnChangeStatus.Invoke(key, _currentStatus);
        }

        public void SetValue(string value) {
            _currentStatus = bool.Parse(value);
            animator.SetBool("Flag", _currentStatus);
        }

    }
}
