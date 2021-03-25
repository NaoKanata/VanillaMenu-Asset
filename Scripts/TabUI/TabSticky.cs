using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VVVanilla.Menu {
    public class TabSticky : MonoBehaviour
    {
        [SerializeField]
        public bool activeFlag = false;
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            animator.SetBool("active", activeFlag);
        }
    }
}