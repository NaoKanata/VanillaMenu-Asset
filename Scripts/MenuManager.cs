using System.Collections.Generic;
using UnityEngine;

using VVVanilla.Core;
using VVVanilla.Core.Constants;
using UnityEngine.EventSystems;

namespace VVVanilla.Menu
{
    public struct MenuCardParam
    {
        public string childName;
        public GameObject targetObject;
    }
	public class MenuManager : MonoBehaviourWithSelfInstanceBase<MenuManager>
	{
		// 初期の画面
		[SerializeField]
        GameObject initialMenu;
		[SerializeField]
        Animator TipsBarUIAnim;
        [SerializeField]
        bool isViewTipsBarUI = true;

        GameObject _currentMenuCard = null;

        List<MenuCardParam> _stackMenuList; // NOTE Prefab の GameObject を保持すること


        // Start is called before the first frame update
        void Start()
		{
            _stackMenuList = new List<MenuCardParam>();

            // initialMenu が設定されていたら割り当てる
            if (initialMenu != null)
            {
                MenuCardParam menuCardParam;
                menuCardParam.targetObject = initialMenu;
                menuCardParam.childName = "";
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
                menuCardParam.childName = EventSystem.current.currentSelectedGameObject.name;
                _stackMenuList[_stackMenuList.Count - 1] = menuCardParam;
            }
            if(!isViewTipsBarUI&&TipsBarUIAnim)
                TipsBarUIAnim.SetBool("view", false);
        }

		// -----------------------------
		// priate methods
		// -----------------------------

        void _Create(MenuCardParam menuCardParam)
        {
            // TODO gameStatus を変更する処理を追加する
            _currentMenuCard = Instantiate(menuCardParam.targetObject);
            // if(menuCardParam.childName != "")
            //     _currentMenuCard.GetComponent<MenuCard>().SetInitialForcus(menuCardParam.childName);
        }

        void _Close()
        {
            // TODO 実際には Interactable = false にして、Closed アニメーションを実行させて、MenuCard 上で Destory するようにする
            Destroy(_currentMenuCard);
        }


        // -----------------------------
        // public methods
        // -----------------------------

        // Menu を生成する
        public void CreateMenu(string menuName)
        {
            _Close();
            MenuCardParam menuCardParam;
            var menuString = menuName.Split('/');
            menuCardParam.targetObject = Resources.Load<GameObject>($"{CMenu.DIR_MENU_CARD}/" + menuString[0]);

            // 初期フォーカスを指定していればそのコンテンツにフォーカスを当てる
            if(menuString.Length==2)
                menuCardParam.childName = menuString[1];
            else
                menuCardParam.childName = "";

            _stackMenuList.Add(menuCardParam);
            _Create(_stackMenuList[_stackMenuList.Count-1]);
            if(isViewTipsBarUI&&TipsBarUIAnim)
                TipsBarUIAnim.SetBool("view", true);
        }

        // TODO UnityEvent を使用するように置き換える
        // exitEvent にイベント名を追加すると、終了時にイベントを実行することができる
        public void Back(string exitEvent = "")
        {
            _Close();
            int num = _stackMenuList.Count;
            if (num >= 2)
            {
                MenuCardParam target = _stackMenuList[num - 2];
                _stackMenuList.RemoveAt(num - 1);
                _Create(target);
            }
            else
            {
                _stackMenuList.RemoveAt(0);
                if(exitEvent != "") 
                {
                    // TODO UnityEvent に置き換える
                    // EventManager.instance.CreateEvent(exitEvent);
                }
                if(isViewTipsBarUI&&TipsBarUIAnim)
                    TipsBarUIAnim.SetBool("view", false);
            }
        }
    }
}
