using UnityEngine;
using System.Collections;

public class HumanoidBehavior : EnemyBehaviour 
{
	public override int damage{ get {return 10;}}
	public override int maxEnemyHP{ get {return 1;}}
}
