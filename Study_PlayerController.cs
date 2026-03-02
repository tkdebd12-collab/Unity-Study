using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {   // MonoBehaviour: 이 코드를 유니티 캐릭터에 '부품(컴포넌트)'처럼 붙여서 쓸 수 있게 해주는 마법의 단어!

	//Public variables
	public float jumpForce = 500.0f; //Base force for a jump  // 점프하는 힘 (이게 높을수록 점프를 더 높게함)
	public float speed = 10.0f; //Speed of the player         // 달리는 속도 (숫자가 높을수록 속도가 점점 빨라짐)

	//MonoBehaviour object components
	//Rigidbody2D rb;                // 물리 엔진 (중력, 힘 담당)  근육과 중력' 게 있어야 캐릭터가 땅으로 떨어지기도 하고, 점프 힘을 받았을 때 위로 솟구칠 수도 있어요. "물리 법칙을 적용받겠다!"는 뜻
    //CircleCollider2D cc;  // 충돌 범위 (벽에 부딪히기 담당) (충돌체)      히트박스(몸체)'  릭터가 벽에 부딪히거나 바닥에 서 있으려면 '부딪히는 면'이 있어야겠죠? 이건 캐릭터를 동그란(Circle) 투명 보호막으로 감싸서 부딪힘을 감지하게 해 줘
    //SpriteRenderer sr;    // 캐릭터 그림 (화면에 보여주기 담당)   (화면 출력)    도화지와 붓'    캐릭터 그림을 화면에 실제로 '그려주는' 역할을 해요. 이게 없으면 캐릭터는 투명 인간이 됩니다.
	                                                                                                                                                                                                                                    
    //Rigidbody2D rb;        // [물리 엔진] 근육과 중력 담당. 이게 있어야 중력을 받고 점프를 함.
    //CircleCollider2D cc;   // [충돌체] 히트박스(몸체). 이게 있어야 바닥에 서고 벽에 부딪힘.
    //SpriteRenderer sr;     // [화면 출력] 도화지와 붓. 이게 없으면 캐릭터는 투명인간!

	void Awake(){
		//Get references to our components
		rb = GetComponent<Rigidbody2D>();
		cc = GetComponent<CircleCollider2D>();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveHorizontal(Input.GetAxis("Horizontal")); //Move/adjust our horizontal velocity based on our horizontal input
		if(Input.GetButtonDown("Jump")){ //If we pushed the jump button down this frame...
			Jump(); //Lets jump!
		}
	}

	void OnCollisionEnter2D(Collision2D coll) { //On the frame this object's Collider collides with another collider...
		if (coll.gameObject.name == "Speed Powerup"){  //Check if we've hit a speed powerup
			Debug.Log("Powerup picked up!"); //Lets let the console know we've hit a powerup
			speed *= 1.5f; //Increase our speed by 150%!
			Destroy(coll.gameObject); //Destroy the powerup
		}
	}

	void MoveHorizontal(float input){ //Takes a input from -1.0 to 1.0
		Vector2 moveVel = rb.velocity; //Get our current rigidbody's velocity
		moveVel.x = input * speed * Time.deltaTime; //Set the new x velocity to be the given input times our speed
		//Note the multiply by Time.deltaTime to compensate for game clock
		rb.velocity = moveVel; //Update our rigidbody's velocity
	}

	void Jump(){
		//Replace "true" with "IsGrounded()" if you want to stop the infinite jumps
		if(true){
			rb.AddForce(Vector2.up * jumpForce); //Add a upward force to our rigidbody
		}
	}

	/* Not to be presented, but this will return true if you are on the ground, false otherwise
	bool IsGrounded(){ //Returns true if we are on the ground, false otherwise
		float spriteRange = cc.radius*transform.localScale.y + 0.05f; //Get the point directly under the player
		float raycastRange = spriteRange + 0.05f; //How far should we do the linecast

		//Perform a linecast hit check for colliders
		RaycastHit2D hit = Physics2D.Linecast(transform.position - new Vector3(0, spriteRange, 0), transform.position - new Vector3(0, raycastRange, 0));
		
		//Debug.DrawLine(transform.position - new Vector3(0, spriteRange, 0), transform.position - new Vector3(0, raycastRange, 0)); //Show the linecast we're preforming

		if(hit.collider == null){ //If the raycast didn't hit anything
			return false;
		} else if(hit.collider.tag == "Platform"){ //If it hit a ground's collider
			return true;
		} else { //If it hit anything else
			return false;
		}		
	}
	*/
} 
