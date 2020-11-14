using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CellTile {
	// tower base stats
	public float searchInterval = 1.0f;	// how often the tower will search for enemies
	public float searchRange = 5.0f;   // how far the tower will search for enemies
	public float switchRange = 2.5f; // if the current enemy is further than this, search for a closer enemy
	public float cooldown = 1.0f;	// shot cooldown, seconds
	public float damage = 1.0f; // shot damage
	public float bulletVelocity = 5.0f;
	public float trackingSpeed = 5.0f;
	protected bool upgradable = false;
	protected int level = 1;
	protected int maxLevel = 5;

	// prefabs and objects
	public Bullet bullet;
	// public GameController controller;
	public List<GameObject> enemies;    // list of existing enemies

	// target tracking
	private Transform barrel;
	private GameObject target;  // enemy currently being targeted
	private Vector3 lookPos;
	private float angle;
	private Quaternion qTo;

	public ToolTipController tooltip;


	public virtual void Start() {
		target = null; // no initial target
		enemies = new List<GameObject>();
		barrel = transform.GetChild(0).GetChild(0);
		StartCoroutine("DoSearch"); // search coroutine starts on creation, loops forever
		StartCoroutine("DoShoot"); // shoot coroutine starts on creation, loops forever
		var bounds = GetComponent<BoxCollider2D>().bounds; //update the graph
		// Expand the bounds along the Z axis
		bounds.Expand(Vector3.forward * 1000);
		var guo = new GraphUpdateObject(bounds);
		//Debug.Log("update");
		// change some settings on the object
		AstarPath.active.UpdateGraphs(guo);
		tooltip = ToolTipController.instance;
	}

	public virtual void Update() {
		if (target) {
			print("Rotating");
			lookPos = target.transform.position - transform.position;
			angle = (Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg) - 90.0f;
			qTo = Quaternion.AngleAxis(angle, Vector3.forward);
			barrel.rotation = Quaternion.Slerp(barrel.rotation, qTo, trackingSpeed * Time.deltaTime);
		}
	}

	// shoot at an enemy
	public virtual bool ShootAt(Vector3 targetPos) {
		Vector3 bulletPos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0.0f);
		Bullet shotBullet = Instantiate(bullet, bulletPos, Quaternion.identity);
		Vector3 direction = (targetPos - transform.position).normalized;
		shotBullet.Setup(direction, bulletVelocity);
		return true;
	}

	// set the current target enemy
	public virtual void SetTarget(GameObject e) {
		target = e;
	}

	// checks for enemies within searchRange
	public virtual void ProximityCheck() {
		// if the tower has a target, check if it's still in range, and shoot if it is
		if (target) {
			if (Vector3.Distance(transform.position, target.transform.position) > switchRange) {
				target = null;
				ProximityCheck();
			}
		} else {
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, searchRange);
			// set new target to closest enemy
			GameObject closest = null;
			float closestDistance = 999f;
			foreach (Collider2D hitCollider in hitColliders) {
				if (hitCollider.tag.Equals("Enemy")) {
					GameObject current = hitCollider.gameObject;
					float distance = Vector3.Distance(transform.position, current.transform.position);
					if (distance < closestDistance) {
						closest = current;
						closestDistance = distance;
					}
				}
			}
			SetTarget(closest);
		}
	}

	/* returns a string of information about the tower to be used for tooltips */
	public override string GetInfo() {
		string info =	"<b>Tower<b> \n" +
						"Targeting Speed: " + this.searchInterval.ToString("n2") + "s\n" +
						"Targeting Range: " + this.searchRange.ToString("n2") + "\n" +
						"Cooldown: " + this.cooldown.ToString("n2") + "s\n" +
						"Damage: " + this.damage.ToString("n2") + "\n" +
						"Bullet Velocity: " + this.bulletVelocity.ToString("n2");
		return info;
	}

	public bool IsUpgradable() {
		return this.upgradable;
	}

	public int GetLevel() {
		return this.level;
	}

	public int GetMaxLevel() {
		return this.maxLevel;
	}

	public bool IsMaxLevel() {
		return (this.level == this.maxLevel);
	}
	public virtual void IncreaseLevel() {}

	// enemy search coroutine
	public virtual IEnumerator DoSearch() { 
		for (;;) {	// loop forever on searchInterval
			ProximityCheck();
			yield return new WaitForSeconds(searchInterval);
		}	
	}

	// tower shoot coroutine
	public virtual IEnumerator DoShoot() {
		for (;;) { // loop forever on cooldown
			bool shot = false;
			while (!shot) {
				if (target) {
					shot = ShootAt(target.transform.position);
				}
				yield return null;
			}
			yield return new WaitForSeconds(cooldown);
		}
	}
}
