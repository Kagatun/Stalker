using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sputnik : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _groundLayers;

    private Rigidbody _rigidbody;
    private float _speed = 3f;
    private float _stopDistance = 9f;
    private float _slopeMax = 90f;
    private float _sphereCastRadius = 0.6f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
            Move();
    }

    private void Move()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        float distance = (_target.position - transform.position).sqrMagnitude;

        if (Physics.SphereCast(transform.position, _sphereCastRadius, Vector3.down, out RaycastHit hit, _sphereCastRadius, _groundLayers))
        {
            float surfaceAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (surfaceAngle < _slopeMax && distance > _stopDistance)
            {
                Vector3 targetVelocity = direction * _speed;
                targetVelocity.y = 0;
                _rigidbody.velocity = targetVelocity;
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }
}