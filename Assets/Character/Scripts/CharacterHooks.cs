using UnityEngine;

namespace Character
{
	public class CharacterHooks : MonoBehaviour 
	{
	    public MeshRenderer MeshRenderer;
	    public Collider Collider;
	    public Rigidbody Rigidbody;
	    public Animator Animator;
	    public Transform Camera;

        public void Awake()
        {
            gameObject.transform.position.Set(0, 5000f, 500f);
            gameObject.tag = "Player";
        }
	}
}