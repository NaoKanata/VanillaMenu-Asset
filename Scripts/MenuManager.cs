using System.Collections.Generic;
using UnityEngine;

using VVVanilla.Core;
using VVVanilla.Core.Constants;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace VVVanilla.Menu
{
    public struct MenuCardParam
    {
        public string childName;  // UI Object
        public GameObject targetObject; // MenuCard Object
    }
	public class MenuManager : MonoBehaviourWithSelfInstanceBase<MenuManager>
	{
        // 初期の画面
        [SerializeField]
        GameObject initialMenu;

        [SerializeField]
        bool _isViewTipsBarUI = true;
        bool _isPressedCancel = false;

        GameObject _currentMenuCard = null;
        PlayerInput _currentPlayerInput = null;

        List<MenuCardParam> _stackMenuList; // NOTE Prefab の GameObject を保持すること

        // 値を渡すイベント
        [SerializeField]
        public UnityEvent<string, string> SetStatusEvent;


        public void OnChangeStatus(string key, string value) 
        {
            // 登録したキーと値によって反映させる値を切り替える
            if(_currentMenuCard != null) {
                foreach(IUITips go in _currentMenuCard.GetComponentsInChildren<IUITips>())
                {
                    if(go.GetTargetKey() == key) {
                        go.SetValue(value);
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Start()
		{
            _stackMenuList = new List<MenuCardParam>();
            _currentPlayerInput = GetComponent<PlayerInput>();

            // initialMenu が設定されていたら割り当てる
            if (initialMenu != null)
            {
                MenuCardParam menuCardParam;
                menuCardParam.targetObject = initialMenu;
                menuCardParam.childName = null;
                _stackMenuList.Add(menuCardParam);
                _Create(menuCardParam);
            }
		}

		// Update is called once per frame
		void Update()
        {
            if (_stackMenuList.Count > 0 && EventSystem.current.currentSelectedGameObject != null)
            {
                MenuCardParam menuCardParam = _stackMenuList[_stackMenuList.Count - 1];
                menuCardParam.childName = GetTargetNameWithConstruction(EventSystem.current.currentSelectedGameObject);
                _stackMenuList[_stackMenuList.Count - 1] = menuCardParam;
                ShowStackMenuList();
            }

            // キャンセルボタンが押された場合
            if(!_isPressedCancel && CurrentPlayerInput.currentActionMap["Cancel"].ReadValue<float>() != 0)
            {
                Back();
            }
            _isPressedCancel = CurrentPlayerInput.currentActionMap["Cancel"].ReadValue<float>() != 0;
        }

        public PlayerInput CurrentPlayerInput {
            get { return _currentPlayerInput; }
        }

        /// <summary>
        /// UI 名を Hierarchy の構造を含めて文字列として返す
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        string GetTargetNameWithConstruction(GameObject targetName) {
            string str = targetName.name;
            while(targetName.transform.parent&&!targetName.transform.parent.GetComponent<MenuCard>()) {
                targetName = targetName.transform.parent.gameObject;
                str = targetName.name + "/" + str;
            }
            return str;
        }

        void ShowStackMenuList()
        {
            // string str = "";
            // for (int i = 0; i < _stackMenuList.Count;i++) {
            //     var param = _stackMenuList[i];
            //     str += $" - {param.targetObject},{param.childName}";
            // }
            // Debug.Log(str);
        }

		// -----------------------------
		// priate methods
		// -----------------------------

        void _Create(MenuCardParam menuCardParam)
        {
            _currentMenuCard = Instantiate(menuCardParam.targetObject);
        }

        void _Close()
        {
            _currentMenuCard.GetComponent<MenuCard>().Hide();
        }


        // -----------------------------
        // public methods
        // -----------------------------

        /// <summary>
        /// Menu を生成する
        /// </summary>
        /// <param name="menuName"> MenuCard名 </param>
        /// <param name="onBackEvent"> 閉じた際に実行させるイベント </param> // TODO 必要か否か要チェック
        public void CreateMenu(string menuName, UnityEvent onBackEvent = null)
        {
            _Close();

            // Create MenuCardParam
            MenuCardParam menuCardParam;
            menuCardParam.targetObject = Resources.Load<GameObject>($"{CMenu.DIR_MENU_CARD}/" + menuName); // TODO 任意のディレクトリに選択できるようにする
            menuCardParam.childName = null;

            // Create MenuCardParam based on new MenuCard
            _stackMenuList.Add(menuCardParam);
            _Create(_stackMenuList[_stackMenuList.Count - 1]);
        }

        /// <summary>
        /// 前の画面に戻る or 現在の Menu を閉じる
        /// </summary>
        public void Back()
        {
            _Close();
            int num = _stackMenuList.Count;
            if (num >= 2)
            {

                // Back to previous MenuCard
                MenuCardParam menuCardParam = _stackMenuList[num - 2];
                _stackMenuList.RemoveAt(num - 1);
                _Create(menuCardParam);

                // 遷移前の最後に選択していたUIに戻す
                // NOTE 重複した名前だと上手くいかないので注意
                if(menuCardParam.childName != "") {
                    _currentMenuCard.GetComponent<MenuCard>().SetFirstTargetFromName(menuCardParam.childName);
                }
            }
            else
            {
                _stackMenuList.RemoveAt(0);
            }
        }

        /// <summary>
        /// 全ての遷移履歴を消して閉じる
        /// </summary>
        public void CloseAll()
        {
            _Close();
            _stackMenuList.Clear();
        }
    }
}
