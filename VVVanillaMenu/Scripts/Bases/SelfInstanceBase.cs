using UnityEngine;

namespace VVVanilla.Core
{
	// 自分自身の instance を取得するクラス
	// NOTE オブジェクトが一つしか存在しない仮定で使用すること
	public class MonoBehaviourWithSelfInstanceBase<Type> : MonoBehaviour
	{
		// 静的に instance を保存する
		static Type _instance;

		static public Type instance {
			get { return _instance; }
		}

		virtual protected void Awake()
		{
			// instance を自動的に割り当てる
			_instance = GetComponent<Type>();
#if UNITY_EDITOR
			Debug.Log($"[CORE] Get Instance {_instance.ToString()}");
#endif
		}
	}
}