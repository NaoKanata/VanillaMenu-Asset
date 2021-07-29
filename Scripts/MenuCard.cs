using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace VVVanilla.Menu
{
    [System.Serializable]
    public class MenuUIAccessRegister : Serialize.KeyAndValue<string, GameObject>{

        public MenuUIAccessRegister (string key, GameObject value) : base (key, value) {

        }
    }

    public class MenuCard : MenuCardBase
    {
        [SerializeField]
        List<MenuUIAccessRegister> menuUIAccessRegisters;
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
        /// Status が更新されたらそれに合わせて更新する
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AffectValueFromReceivedData(string key, string value) {
            Debug.Log($"Received Data: key={key}, value={value}");
            foreach(MenuUIAccessRegister param in menuUIAccessRegisters) {
                if(param.Key == key) {
                    // UITip の値変更処理（例えば、ToggleUI なら true/false の値から UI の見え方を変える処理）
                    // UITip.ChangeValue(value) 的な
                }
            }
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
