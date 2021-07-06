using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// using VVVanilla.Status;

namespace VVVanilla.Menu {
    public class VolumeTipUI : MonoBehaviour
    {
        [SerializeField]
        TMPro.TMP_Text percentageText;

        // 遷移前に戻る時に使用する親オブジェクト
        [SerializeField]
        GameObject parentObject;

        [SerializeField]
        Image volumeImage;
        [SerializeField]
        string setDataName;
        [SerializeField]
        float value = 1.0f;

        int previousValue = -999;

        bool isFirstFocus = true;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            PreCheck();

            // int data = (int)StatusManager.instance.Get(setDataName);
            // フォーカスがこのオブジェクトの時のみ実行
            if(EventSystem.current.currentSelectedGameObject == gameObject)
            {
                MenuManager.instance.CurrentPlayerInput.SwitchCurrentActionMap("UI");
                if(!isFirstFocus && MenuManager.instance.CurrentPlayerInput.currentActionMap["Submit"].ReadValue<float>() > 0) {
                    // EventSystem.current.SetSelectedGameObject(parentObject);
                }
                else if(MenuManager.instance.CurrentPlayerInput.currentActionMap["Cancel"].ReadValue<int>() > 0) {
                    EventSystem.current.SetSelectedGameObject(parentObject);
                    // StatusManager.instance.Set(setDataName, previousValue);
                }
                MenuManager.instance.CurrentPlayerInput.SwitchCurrentActionMap("Player");

                // float ud = Input.GetAxis("Horizontal");
                float ud = MenuManager.instance.CurrentPlayerInput.currentActionMap["Move"].ReadValue<Vector2>().y;
                if(ud > 0) {
                    // StatusManager.instance.Set(setDataName, data + 1);
                }
                else if(ud < 0) {
                    // StatusManager.instance.Set(setDataName, data - 1);
                }
                isFirstFocus = false;
            }
            else
            {
                previousValue = -999;
                isFirstFocus = true;
            }
            // ChangeVolume(data);
            // UpdateText(data);
        }

        void PreCheck()
        {
            // var data = StatusManager.instance.Get(setDataName);
            // if(data == null) {
            //     StatusManager.instance.Set(setDataName, 0);
            // }
            // if(previousValue == -999) {
            //     previousValue = (int)StatusManager.instance.Get(setDataName);
            // }
        }

        public void UpdateText(int data)
        {
            if(percentageText) {
                percentageText.text = $"{Mathf.Floor(data)} %";
            }
        }

        public void ChangeVolume(int data)
        {
            value = (float)data/100;
            var scale = volumeImage.GetComponent<RectTransform>().localScale;
            scale.y = scale.y * 0.9f + value * 0.1f;
            volumeImage.GetComponent<RectTransform>().localScale = scale;
        }
    }
}
