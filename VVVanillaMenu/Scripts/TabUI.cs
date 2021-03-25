using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VVVanilla.Menu {
    public class TabUI : MonoBehaviour
    {
        List<GameObject> tabList;
        List<GameObject> contentList;
        [SerializeField]
        int indexNum = 0;


        // Start is called before the first frame update
        void Start()
        {
            _InitializeTabCount();
        }

        // Update is called once per frame
        void Update()
        {
            // 現在のフォーカスがTab内に含まれているかチェックが必要
            if(_CheckInnerChildObjectFromContentList()) 
            {
                if(Input.GetButtonDown("Horizontal")) 
                {
                    var lr = Input.GetAxis("Horizontal");
                    indexNum += lr > 0 ? 1 : -1;
                    if(indexNum >= tabList.Count) {
                        indexNum %= tabList.Count;
                    } else if( indexNum < 0) {
                        indexNum = tabList.Count - 1;
                    }
                    _ChangeTab(indexNum);
                }
            }
        }

        bool _CheckInnerChildObjectFromContentList() {
            var targetObj = EventSystem.current.currentSelectedGameObject;
            foreach(GameObject go in contentList) {
                if(_CheckInnerChildObject(go, targetObj)) return true;
            }
            return false;
        }

        bool _CheckInnerChildObject(GameObject searchObj, GameObject targetObj) {
            for (int j = 0; j < searchObj.transform.childCount; j++)
            {
                if (searchObj.transform.GetChild(j).gameObject == targetObj || _CheckInnerChildObject(searchObj.transform.GetChild(j).gameObject, targetObj)) {
                    return true;
                }
            }
            return false;
        }


        void _ChangeTab(int num)
        {
            // TODO フォーカスを当てるUIを設定する
            for(int i = 0 ; i < tabList.Count; i++) {
                // tab はアクティブなものだけ高さを高くするようにする
                tabList[i].GetComponent<TabSticky>().activeFlag = i == num;
                // content は必要なものだけ表示するようにする
                contentList[i].SetActive(i == num);
                if( i == num ) contentList[i].GetComponent<TabContent>().SetInitialTarget();
            }
        }

        void _InitializeTabCount() {
            tabList = new List<GameObject>();
            contentList = new List<GameObject>();
            for(int i = 0 ; i < transform.Find("StikeyList").childCount; i++) {
                tabList.Add(transform.Find("StikeyList").GetChild(i).gameObject);
                tabList[i].GetComponent<TabSticky>().activeFlag = false;
            }
            for(int i = 0 ; i < transform.Find("ContentList").childCount; i++) {
                contentList.Add(transform.Find("ContentList").GetChild(i).gameObject);
                contentList[i].gameObject.SetActive(false);
            }
            _ChangeTab(indexNum);
        }
    }
}