using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITipConnector : MonoBehaviour
{
    UnityEvent<string, string> onStatusChange;
    /// <summary>
    /// 状態を送る
    /// </summary>
    public void SetStatus(string param) {
        string[] p = param.Split(',');
        onStatusChange.Invoke(p[0], p[1]);
    }
}
