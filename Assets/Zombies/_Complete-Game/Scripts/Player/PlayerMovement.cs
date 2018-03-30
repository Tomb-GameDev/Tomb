using UnityEngine;

namespace CompleteProject
{
    public class PlayerMovement : MonoBehaviour
    {
        private struct Direction
        {
            public bool isMoving;
            public Vector3 moveDir;
            public Quaternion rotation;
            
            public void Set(float x, float z)
            {
                isMoving = x != 0 | z != 0;
                moveDir = new Vector3(x, 0, z).normalized;
                rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            }
        }

        public float speed = 6f;            // The speed that the player will move at.
        public float smoothTurn = 0.5f;
        public Transform camTrans;

        private Animator anim;                      // Reference to the animator component.
        private Rigidbody rb;          // Reference to the player's rigidbody.
        private readonly Direction[,] direction = new Direction[3,3];

        void Awake ()
        {
            anim = GetComponent <Animator> ();
            rb = GetComponent <Rigidbody> ();
            for (int x = 0; x < 3; ++x)
                for (int z = 0; z < 3; ++z)
                    direction[x, z].Set(x-1, z-1);
        }

        void FixedUpdate ()
        {
            int leftRight = (int)Input.GetAxisRaw("Horizontal") + 1;
            int forBack = (int)Input.GetAxisRaw("Vertical") + 1;
            
            Move (ref direction[leftRight, forBack]);
        }

        void Move(ref Direction dir)
        {
            anim.SetBool("IsWalking", dir.isMoving);
            if (dir.isMoving)
            { 
                rb.rotation = camTrans.rotation * dir.rotation;
                rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            }
            //rb.MovePosition(rb.position + transform.TransformDirection(dir.moveDir) * speed * Time.deltaTime); 
        }
    }
}