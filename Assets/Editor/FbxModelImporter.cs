using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;

public class FbxModelImporter : AssetPostprocessor
{
    private const string ModelNameArachnid = "pajenczak";
    private const string ModelNameBlob = "glut";
    private const string ModelNameHumanoid = "trmboglow";
    private const string ModelNamePlayer = "laska";

    public void OnPostprocessModel(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case ModelNameArachnid:
                //ProcessArachnid(gameObject);
                break;
            case ModelNameBlob:
                ProcessBlob(gameObject);
                break;
            case ModelNameHumanoid:
                //ProcessHumanoid(gameObject);
                break;
        }
    }

    private void ProcessArachnid(GameObject gameObject)
    {
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = 2f;
        rigidbody.drag = 2f;
        rigidbody.angularDrag = 0.1f;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        rigidbody.interpolation = RigidbodyInterpolation.None;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        var navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.radius = 2f;
        navMeshAgent.speed = 10f;
        navMeshAgent.acceleration = 10f;
        navMeshAgent.angularSpeed = 150f;
        navMeshAgent.stoppingDistance = 0;
        navMeshAgent.autoTraverseOffMeshLink = true;
        navMeshAgent.autoBraking = true;
        navMeshAgent.autoRepath = true;
        navMeshAgent.height = 1;
        navMeshAgent.baseOffset = 0.5f;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        navMeshAgent.avoidancePriority = 50;

        var collider = gameObject.AddComponent<CapsuleCollider>();
        collider.center = new Vector3(0, 2f, -0.5f);
        collider.radius = 1.5f;
        collider.height = 6f;

        gameObject.AddComponent<AudioSource>();

        gameObject.AddComponent<ArachnidBehavior>();

        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("enemy");
    }

    private void ProcessBlob(GameObject gameObject)
    {
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = 3f;
        rigidbody.drag = 2f;
        rigidbody.angularDrag = 0.2f;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        rigidbody.interpolation = RigidbodyInterpolation.None;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        var navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.radius = 1.6f;
        navMeshAgent.speed = 7f;
        navMeshAgent.acceleration = 7f;
        navMeshAgent.angularSpeed = 100f;
        navMeshAgent.stoppingDistance = 0;
        navMeshAgent.autoTraverseOffMeshLink = true;
        navMeshAgent.autoBraking = true;
        navMeshAgent.autoRepath = true;
        navMeshAgent.height = 1;
        navMeshAgent.baseOffset = 0.5f;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        navMeshAgent.avoidancePriority = 50;

        var collider = gameObject.AddComponent<BoxCollider>();
        collider.center = new Vector3(0, 2.5f, -0.1f);
        collider.size = new Vector3(2.9f, 5f, 3.9f);

        gameObject.AddComponent<AudioSource>();

        gameObject.AddComponent<GlutBehavior>();

        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("enemy");
    }

    private void ProcessHumanoid(GameObject gameObject)
    {
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = 1f;
        rigidbody.drag = 2f;
        rigidbody.angularDrag = 0.05f;
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        rigidbody.interpolation = RigidbodyInterpolation.None;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        var navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.radius = 0.5f;
        navMeshAgent.speed = 30f;
        navMeshAgent.acceleration = 10f;
        navMeshAgent.angularSpeed = 200f;
        navMeshAgent.stoppingDistance = 0;
        navMeshAgent.autoTraverseOffMeshLink = true;
        navMeshAgent.autoBraking = true;
        navMeshAgent.autoRepath = true;
        navMeshAgent.height = 2;
        navMeshAgent.baseOffset = 1f;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        navMeshAgent.avoidancePriority = 50;

        gameObject.AddComponent<AudioSource>();

        gameObject.AddComponent<HumanoidBehavior>();
    
        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("enemy");
    }
}
