using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Settings")]
    public float flyDuration = 3f;
    public bool attacking = false;

    [Space] [Header("References")] 
    public GameObject top;
    public GameObject left;
    public GameObject right;
    public GameObject aim;
    
    [Space][Header("DiveAttack")]
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 5.0f;
    public float amplitude = 2.0f;
    public GameObject explosion;
    public GameObject bomb;
    public float DiveCooldown = 5;
    private float _elapsedTime = 0;

    [Space] [Header("Tornado")] 
    public Transform tornadoGoal;
    public GameObject tornadoPrefab;
    public float tornadoCooldown = 4;
    public float elapsedTimeTornado = 0;

    [Space] [Header("Launch")] 
    public bool launch = false;
    public bool fire = false;
    public GameObject fireArea;

    public GameObject bombPrefab;
    public bool alreadyFly;


    private float t = 0.0f;
    

    public enum State
    {
        Nothing,
        ChargeJump,
        Attacking,
    }

    public State action = State.Nothing;
    
    public Coroutine flyCoroutine;

    private Movement _movementRef;
    private Rigidbody _rb;
    

    // Start is called before the first frame update
    void Start()
    {
        _movementRef = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody>();
        Debug.Log(action);
    }

    private void Update()
    {
        if (!attacking)
        {
            flyTrigger();

            if (Input.GetKeyDown(KeyCode.F))
            {
                launch = !launch;
                Debug.Log("LAUNCH MODE = " + launch);
            }
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                fire = !fire;
                Debug.Log("NAPALM MODE = " + fire);
            }
            

            if (Input.GetKeyDown(KeyCode.E) && aim.activeSelf && _elapsedTime >= DiveCooldown)
            {
                if (!launch)
                {
                    attacking = true;
                    action = State.Attacking;
                    startPoint = transform.position;
                    endPoint = aim.transform.position;
                }
                else if (launch)
                {
                    GameObject b = Instantiate(bombPrefab, right.transform.position, Quaternion.identity);

                    //b.GetComponent<BombBehavior>().startPoint = bomb.transform.position;
                    //b.GetComponent<BombBehavior>().endPoint = aim.transform.position;
                    if (fire)
                        b.GetComponent<BombBehavior>().fireArea = fireArea;
                    _elapsedTime = 0;
                }
            }

            
            //tornado
            if (elapsedTimeTornado > tornadoCooldown)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    GameObject tornado = Instantiate(tornadoPrefab, right.transform.position, Quaternion.identity);
                    tornado.GetComponent<TornadoBehavior>().startPoint = right.transform.position;
                    tornado.GetComponent<TornadoBehavior>().endPoint = tornadoGoal.transform.position;
                    tornado.GetComponent<TornadoBehavior>().right = 1;
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    GameObject tornado = Instantiate(tornadoPrefab, left.transform.position, Quaternion.identity);
                    tornado.GetComponent<TornadoBehavior>().startPoint = left.transform.position;
                    tornado.GetComponent<TornadoBehavior>().endPoint = tornadoGoal.transform.position;
                    tornado.GetComponent<TornadoBehavior>().right = -1;
                }
            }
            else
            {
                elapsedTimeTornado += Time.deltaTime;
            }
            //tornado
        }
    }

    private void FixedUpdate()
    {
        
        if (_elapsedTime < DiveCooldown)
        {
            _elapsedTime += Time.deltaTime;
            bomb.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _elapsedTime / DiveCooldown);
        }
        
        if (attacking)
        {
            t += Time.deltaTime * speed;
            if (t > 1.0f)
            {
                t = 1.0f;
            }
            
            Vector3 position = CalculateSineWavePoint(t, startPoint, endPoint, amplitude); 
            gameObject.transform.LookAt(Vector3.Lerp(startPoint, endPoint, 0.5f), gameObject.transform.up);
            transform.position = position;
            
            
            if (Mathf.Abs(transform.position.x - endPoint.x )< 0.4f && Mathf.Abs(transform.position.z - endPoint.z )< 0.4f)
            {
                attacking = false;
                action = State.Nothing;
                _elapsedTime = 0;
                bomb.transform.localScale = Vector3.zero;
                GameObject e = Instantiate(explosion, endPoint, Quaternion.identity);
                e.transform.localScale *= 5;
                t = 0;

                if (fire)
                {
                    Instantiate(fireArea, endPoint, Quaternion.identity);
                }
            }
        }
    }

    void flyTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Space) && flyCoroutine == null && !alreadyFly)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            flyCoroutine = StartCoroutine(Fly());
            alreadyFly = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && flyCoroutine != null)
        {
            StopCoroutine(flyCoroutine);
            flyCoroutine = null;
            _movementRef.ResetGravity();
        }
    }

    IEnumerator Fly()
    {
        float elapsedTime = 0f;
        while (elapsedTime < flyDuration)
        {
            elapsedTime += Time.deltaTime;
            Physics.gravity = new Vector3(0, -3f, 0);
            top.transform.eulerAngles += new Vector3(0, 10f, 0);
            yield return null;
        }
        _movementRef.ResetGravity();
        flyCoroutine = null;
    }
    
    //Calculate path for dive attack
    private Vector3 CalculateSineWavePoint(float t, Vector3 p0, Vector3 p1, float amplitude)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1.0f - t;

        // Calculate the height (Y) based on a sine wave.
        float height = amplitude * Mathf.Sin(t * Mathf.PI);

        // Interpolate the position along the X and Z axes linearly.
        float x = oneMinusT * p0.x + t * p1.x;
        float z = oneMinusT * p0.z + t * p1.z;
        
        return new Vector3(x, height, z);
    }
    
    
}
