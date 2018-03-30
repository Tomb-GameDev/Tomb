using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
        public float sensitivity = 1f;
        private bool isMouseLocked;

        private void Start()
        {
            isMouseLocked = true;
            LockCursor(isMouseLocked);
        }

        void FixedUpdate ()
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * sensitivity);
            transform.position = Vector3.Lerp (transform.position, target.position, smoothing * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isMouseLocked = !isMouseLocked;
                LockCursor(isMouseLocked);
            }
        }

        private void LockCursor(bool flag)
        {
            Cursor.lockState = (flag) ? CursorLockMode.Locked:CursorLockMode.None;
            Cursor.visible = !flag;
        }
    }
}