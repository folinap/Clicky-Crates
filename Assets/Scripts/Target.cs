using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]private int _pointValue;
    [SerializeField]private ParticleSystem _explosionParticle;
    [SerializeField]private Rigidbody _targetRb;
    [SerializeField]private GameManager _gameManager;
    private float _minSpeed = 12f;
    private float _maxSpeed = 16f;
    private float _maxTorque = 10f;
    private float _xRange = 4f;
    private float _ySpawnPos = -2f;
    private int enemyLayer = 3;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    private void OnTriggerEnter(Collider other)
    {       
        Destroy(gameObject);
        if(gameObject.layer != enemyLayer)
        {
            _gameManager.UpdateLives(-1);            
        }    
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }

    public void DestroyTarget()
    {
        if (_gameManager.IsGameActive)
        {
            Destroy(gameObject);
            Instantiate(_explosionParticle, transform.position,
            _explosionParticle.transform.rotation);
            _gameManager.UpdateScore(_pointValue);
        }
    }


}
