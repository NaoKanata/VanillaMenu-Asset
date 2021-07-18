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

    public class ToggleUI : MonoBehaviour
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        bool isUsingMonitoringData = false;
        [SerializeField]
        string setDataName = "sampleData";
        [SerializeField]
        UnityEventBool sendValueFunc;
        [SerializeField]
        delegate bool getValueFunc();

        [SerializeField]
        private bool selfData = false;

        // Start is called before the first frame update
        void Start()
        {
            // データベースから値を取得する
            // 親の MenuUI から取得するようにする
            CheckValue();
        }

        public bool GetValue() {
            return false;
        }

        private void OnEnable() {
            animator.SetBool("Flag", selfData);
        }

        public void ChangeValue()
        {
            // 自信のデータを更新
            selfData = !selfData;
            CheckValue();

        }

        public void CheckValue()
        {
            // subscribe した関数を呼び出し
            sendValueFunc.Invoke(selfData);

            // 変更時のアニメーションを実行
            animator.SetBool("Flag", selfData);
        }
        
    }
}
