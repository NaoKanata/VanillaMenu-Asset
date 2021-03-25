using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VVVanilla.Menu {
    public class TabContent : MonoBehaviour
    {
        [SerializeField]
        GameObject initialTargetObj;

        void Update()
        {
        }

        public void SetInitialTarget(){
            if(initialTargetObj) 
            {
                EventSystem.current.SetSelectedGameObject(initialTargetObj);
            }
            else
            {
                if(transform.GetChild(0).name == "ScrollUI") {
                    EventSystem.current.SetSelectedGameObject(transform.Find("ScrollUI/Viewport/Content").GetChild(0).gameObject);

                } else {
                    EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
                }
            }
        }
    }
}