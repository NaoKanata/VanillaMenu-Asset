using UnityEngine;
using UnityEngine.EventSystems;

namespace VVVanilla.Menu
{
    public class MenuCardBase : MonoBehaviour {
        /// <summary>
        /// 最初にフォーカスを当てる項目
        /// </summary>
        [SerializeField]
        GameObject firstTarget = null;

        Animator _animator;
        bool _isClose = false;
        bool _isOpen = false;
        bool _isPreClose = false;
        bool _isPreOpen = false;
        bool _openedFlag = false;

        protected virtual void Start() {
            _animator = GetComponent<Animator>();
            View();
        }

        protected virtual void Update() {
            if(_animator) {
                if(_animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) {
                    if(_isClose&&_isPreClose)
                    {
                        // MenuCard が消えたら削除する
                        _isClose = false;
                        Destroy(this.gameObject);
                    }
                    if(_isOpen&&_isPreOpen)
                    {
                        // MenuCard が開いたら最初のフォーカスに当てる
                        _isOpen = false;
                        EventSystem.current.SetSelectedGameObject(firstTarget);
                    }
                }
            }
            _isPreOpen = _isOpen;
            _isPreClose = _isClose;
        }

        public void SetFirstTargetFromName(string targetName) {
            firstTarget = transform.Find(targetName).gameObject;
        }

        public virtual void Hide() {
            EventSystem.current.SetSelectedGameObject(null);
            _isClose = true;
            if(_animator) {
                _animator.SetTrigger("Hide");
            } else {
                // Animator が未割当の場合は、すぐに削除する
                _isClose = false;
                Destroy(this.gameObject);
            }
        }
        protected virtual void View() {
            _isOpen = true;
            if(_animator) {
                _animator.SetTrigger("View");
            } else {
                // Animator が未割当の場合は、すぐに最初のフォーカスに当てる
                EventSystem.current.SetSelectedGameObject(firstTarget);
                _isOpen = false;
            }
        }
    }
}
