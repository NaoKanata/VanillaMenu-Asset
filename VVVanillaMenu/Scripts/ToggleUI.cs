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
    public class ToggleUI : MonoBehaviour, IMonitorUI<bool>
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        bool isUsingMonitoringData = false;
        [SerializeField]
        string setDataName = "sampleData";
        [SerializeField]
        UnityEvent getValueFunc;

        public void SetValue(bool value) {

        }
        public bool GetValue() {
            return false;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // UnityEvent に置き換える
            // if (StatusManager.instance.Get(setDataName) == null)
            // {
            //     StatusManager.instance.Set(setDataName, false);
            // }
            // bool f = (bool)StatusManager.instance.Get(setDataName);
            // animator.SetBool("Flag", f);
        }

        public void ChangeValue()
        {
            // bool f = (bool)StatusManager.instance.Get(dataName);
            // StatusManager.instance.Set(dataName, !f);
        }
        
    }
}
