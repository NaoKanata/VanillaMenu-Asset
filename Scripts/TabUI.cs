using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VVVanilla.Menu {
    public class TabUI : MonoBehaviour
    {
        List<GameObject> _tabList;
        List<GameObject> _contentList;
        [SerializeField]
        int _indexNum = 0;

        int _pressDownCount = 0;

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
                // if(Input.GetButtonDown("Horizontal")) 
                if(MenuManager.instance.CurrentPlayerInput.currentActionMap["Move"].ReadValue<Vector2>().x != 0)
                {
                    _pressDownCount++;
                    if(_pressDownCount == 1) {
                        // var lr = Input.GetAxis("Horizontal");
                        var lr = MenuManager.instance.CurrentPlayerInput.currentActionMap["Move"].ReadValue<Vector2>().x;
                        _indexNum += lr > 0 ? 1 : -1;
                        if(_indexNum >= _tabList.Count) {
                            _indexNum %= _tabList.Count;
                        } else if( _indexNum < 0) {
                            _indexNum = _tabList.Count - 1;
                        }
                        _ChangeTab(_indexNum);
                    }
                }
                else {
                    _pressDownCount = 0;
                }
            }
        }

        bool _CheckInnerChildObjectFromContentList() {
            var targetObj = EventSystem.current.currentSelectedGameObject;
            foreach(GameObject go in _contentList) {
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
            for(int i = 0 ; i < _tabList.Count; i++) {
                // tab はアクティブなものだけ高さを高くするようにする
                _tabList[i].GetComponent<TabSticky>().activeFlag = i == num;
                // content は必要なものだけ表示するようにする
                _contentList[i].SetActive(i == num);
                if( i == num ) _contentList[i].GetComponent<TabContent>().SetInitialTarget();
            }
        }

        void _InitializeTabCount() {
            _tabList = new List<GameObject>();
            _contentList = new List<GameObject>();
            for(int i = 0 ; i < transform.Find("StikeyList").childCount; i++) {
                _tabList.Add(transform.Find("StikeyList").GetChild(i).gameObject);
                _tabList[i].GetComponent<TabSticky>().activeFlag = false;
            }
            for(int i = 0 ; i < transform.Find("ContentList").childCount; i++) {
                _contentList.Add(transform.Find("ContentList").GetChild(i).gameObject);
                _contentList[i].gameObject.SetActive(false);
            }
            _ChangeTab(_indexNum);
        }
    }
}