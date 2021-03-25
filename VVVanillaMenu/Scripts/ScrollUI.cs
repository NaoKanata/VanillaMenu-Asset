using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VVVanilla.Menu {
    public class ScrollUI : MonoBehaviour
    {
        [SerializeField]
        GameObject ContentObj;
        [SerializeField]
        Scrollbar verticalHandle;

        [SerializeField]
        float safeDistanceTop = 100;
        [SerializeField]
        float safeDistance = 100;
        float pos = 0.0f;
        float currentPos = 0.0f;
        float vh = 0.0f;
        float height = 0.0f;
        Vector2 pPos = Vector2.zero;

        [SerializeField]
        GameObject autoArrangeObj;
        [SerializeField]
        string[] autoList;

        // Start is called before the first frame update
        void Awake()
        {
            vh = GetComponent<RectTransform>().sizeDelta.y;
            AutoAssignObject();
            ForceChangeHeight();
            SetHeight();
        }

        void AutoAssignObject() 
        {
            float pos = 0;
            foreach(string s in autoList) {
                GameObject go = Instantiate(autoArrangeObj);
                go.transform.parent = ContentObj.transform;
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -pos);
                pos += safeDistance + 50;
            }
        }

        // Update is called once per frame
        void Update()
        {
            var currentContent = EventSystem.current.currentSelectedGameObject;
            if(CheckInnerChildObject(currentContent)) {
                var cPos = currentContent.GetComponent<RectTransform>().anchoredPosition;
                if(-pPos.y <= pos + vh - safeDistance && -cPos.y > pos + vh - safeDistance)
                {
                    pos = -cPos.y - vh + safeDistance;
                }
                if(-pPos.y >= pos && -cPos.y < pos)
                {
                    pos = -cPos.y - safeDistanceTop;
                }
                pPos = cPos;
            }
            CalcPosition();
        }

        bool CheckInnerChildObject(GameObject targetObj){
            for (int i = 0; i < ContentObj.transform.childCount;i++){
                if(ContentObj.transform.GetChild(i).gameObject == targetObj)
                    return true;
            }
            return false;
        }

        void CalcPosition()
        {
            var ar = height - vh;
            currentPos = currentPos * 0.7f + pos * 0.3f;
            if(Mathf.Abs(currentPos-pos) < 0.01f) {
                currentPos = pos;
                verticalHandle.value = 1 - pos / ar;
            }
            else
            {
                verticalHandle.value = 1 - currentPos / ar;
            }
        }

        void ForceChangeHeight() {
            float minPos = 999;
            for (int i = 0; i < ContentObj.transform.childCount;i++) 
            {
                var pos = ContentObj.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
                if(-pos.y < minPos) {
                    minPos = -pos.y;
                }
            }
            float diff = safeDistanceTop - minPos;
            for (int i = 0; i < ContentObj.transform.childCount; i++)
            {
                ContentObj.transform.GetChild(i).GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
                ContentObj.transform.GetChild(i).GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);

                var pos = ContentObj.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
                pos.y -= diff;
                ContentObj.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = pos;
            }
        }

        void SetHeight()
        {
            float maxPos = 0;
            for (int i = 0; i < ContentObj.transform.childCount;i++) 
            {
                var pos = ContentObj.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
                if(-pos.y > maxPos) {
                    maxPos = -pos.y;
                }
            }
            height = maxPos + safeDistance;
            ContentObj.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
        }
    }
}
