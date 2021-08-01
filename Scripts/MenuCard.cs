using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace VVVanilla.Menu
{
    // [System.Serializable]
    // public class MenuUIAccessRegister : Serialize.KeyAndValue<string, GameObject>{

    //     public MenuUIAccessRegister (string key, GameObject value) : base (key, value) {

    //     }
    // }

    public class MenuCard : MenuCardBase
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// MenuCard の生成
        /// </summary>
        /// <param name="menuName"></param>
        public void Create(string menuName)
        {
            MenuManager.instance.CreateMenu(menuName);
        }

        /// <summary>
        /// 現在の MenuCard を消して前の MenuCard に戻る
        /// </summary>
        /// <param name="menuName"></param>
        public void Back()
        {
            MenuManager.instance.Back();
        }
    }
}
