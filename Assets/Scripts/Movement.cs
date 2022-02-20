using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip engineClip;
    [SerializeField] ParticleSystem MainThrusterParticular;
    [SerializeField] ParticleSystem RightThrusterParticular;
    [SerializeField] ParticleSystem LeftThrusterParticular;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
            StartThrusting();
        else
            StopThrusting();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineClip);
        }
        if (!MainThrusterParticular.isPlaying)
        {
            MainThrusterParticular.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        MainThrusterParticular.Stop();
    }
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // Left Rotate
            Rotate(rotationThrust, LeftThrusterParticular);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Right Rotate
            Rotate(-rotationThrust, RightThrusterParticular);
        } else
        {
            RightThrusterParticular.Stop();
            LeftThrusterParticular.Stop();
        }
    }

    private void Rotate(float rotationThrust, ParticleSystem particular)
    {
        ApplyRotation(rotationThrust);
        ParticulSides(particular);
    }

    private void ParticulSides(ParticleSystem particular)
    {
        if (!particular.isPlaying)
        {
            particular.Play();
        } 
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
