using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VVVanilla.Menu
{
    public class MenuCard : MonoBehaviour
    {
        [SerializeField]
        GameObject firstTarget = null;

        // Start is called before the first frame update
        void Start()
        {
            EventSystem.current.SetSelectedGameObject(firstTarget);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetButtonDown("Cancel")) {
                Back();
            }
        }

        // public void SetInitialForcus(string childName)
        // {
        //     firstTarget = transform.Find(childName).gameObject;
        // }

        public void CreateMenu(string menuName)
        {
            MenuManager.instance.CreateMenu(menuName);
        }

        public void Back(string exitEvent = "")
        {
            MenuManager.instance.Back(exitEvent);
        }

        public void SetStatus(string dataName, object data) {
            // TODO UnityEvent に置き換える
            // StatusManager.instance.Set(dataName, data);
        }

        public void SetStatusBoolTrue(string dataName) {
            SetStatus(dataName, true);
        }
        public void SetStatusBoolFalse(string dataName) {
            SetStatus(dataName, false);
        }
        public void SetStatusBoolToggle(string dataName) {
            // var f = StatusManager.instance.Get(dataName);
            // SetStatus(dataName, !(bool)f);
        }
    }
}
