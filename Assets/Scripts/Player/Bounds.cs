using System;
using UnityEngine;

namespace Player
{
    public class Bounds : MonoBehaviour
    {
        public static Bounds Instance;
        public Transform vectorForward;
        public Transform vectorBack;
        public Transform vectorLeft;
        public Transform vectorRight;

        private void Awake() => Instance = this;

        private void LateUpdate()
        {
            Vector3 viewPos = transform.position;
            viewPos.z = Mathf.Clamp(viewPos.z, vectorBack.transform.position.z, vectorForward.transform.position.z);
            viewPos.x = Mathf.Clamp(viewPos.x, vectorLeft.transform.position.x, vectorRight.transform.position.x);
            transform.position = viewPos;
        }
    }
}