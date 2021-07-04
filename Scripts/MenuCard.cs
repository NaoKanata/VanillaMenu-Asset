using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace VVVanilla.Menu
{
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

        // TODO ここらへんどうするか問題
        // public void SetStatus(string dataName, object data) {
        //     // TODO UnityEvent に置き換える
        //     // StatusManager.instance.Set(dataName, data);
        // }

        // public void SetStatusBoolTrue(string dataName) {
        //     SetStatus(dataName, true);
        // }
        // public void SetStatusBoolFalse(string dataName) {
        //     SetStatus(dataName, false);
        // }
        // public void SetStatusBoolToggle(string dataName) {
        //     // var f = StatusManager.instance.Get(dataName);
        //     // SetStatus(dataName, !(bool)f);
        // }
    }
}
