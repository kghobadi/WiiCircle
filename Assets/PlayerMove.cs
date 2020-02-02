using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    NavMeshAgent nma;
    Animator ani;

    public GameObject tavern, jukeBox, penpen;

    public CinemachineVirtualCamera topCam, jbCam, penpenCam, sparrotCam;

    public PersonDance sparrot;
    public AudioSource jbAudio;
    public AudioClip jbClip1;
    public AudioClip countryRoad;

    public Transform sparrotDancePos;
    public GameObject sparrotSpotLight;

    public Animator[] dancers;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        ani = GetComponentInChildren<Animator>();
    }
    bool shouldDoJBCam, shouldDoPenpenCam, shouldDoSparrotCam;

    float endTim = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Collider c = hit.collider;
                if (c.gameObject == tavern && !shouldDoJBCam)
                    nma.SetDestination(hit.point);
                else if (c.gameObject == jukeBox)
                {
                    shouldDoJBCam = true;
                    nma.SetDestination(jukeBox.transform.position + jukeBox.transform.forward * 0.5f);
                }
                else if (c.gameObject == penpen)
                {
                    shouldDoPenpenCam = true;
                    nma.SetDestination(penpen.transform.position + penpen.transform.forward * 1.5f);
                }
                else if (c.gameObject == sparrot.gameObject)
                {
                    if (hasSetSong)
                    {
                        shouldDoSparrotCam = true;
                        nma.SetDestination(sparrotDancePos.position);

                    }
                }
            }
        }

        if (shouldDoSparrotCam)// && Vector3.Distance(transform.position, sparrotDancePos.position) < 0.7f)
        {
            topCam.gameObject.SetActive(false);
            jbCam.gameObject.SetActive(false);
            penpenCam.gameObject.SetActive(false);
            sparrotCam.gameObject.SetActive(true);

            transform.LookAt(sparrot.transform);

            shouldDoJBCam = false;
            shouldDoPenpenCam = false;


            if (!hasDoneCR)
                StartCoroutine(DoCountryRoad());
            else
            {
                if (endTim < 30)
                    endTim += Time.deltaTime;
                //DOTRansition
                else
                {
                    SceneUtils scene = FindObjectOfType<SceneUtils>();
                    scene.LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }


            sparrotSpotLight.SetActive(true);
        }
        else if (shouldDoJBCam)// && Vector3.Distance(transform.position, jukeBox.transform.position) < 0.7f)
        {
            topCam.gameObject.SetActive(false);
            jbCam.gameObject.SetActive(true);
            penpenCam.gameObject.SetActive(false);
            sparrotCam.gameObject.SetActive(false);

            if (Vector3.Distance(transform.position, jukeBox.transform.position) < 0.7f)
            {
                ani.SetBool("atJB", true);

                transform.LookAt(jukeBox.transform);
                if (!hasSetSong)
                    StartCoroutine(DoSong());
            }

            shouldDoPenpenCam = false;
        }
        else if (Vector3.Distance(transform.position, penpen.transform.position) < 1.7f)// && shouldDoPenpenCam)
        {
            topCam.gameObject.SetActive(false);
            jbCam.gameObject.SetActive(false);
            penpenCam.gameObject.SetActive(true);
            sparrotCam.gameObject.SetActive(false);

            shouldDoJBCam = false;
        }
        else
        {
            topCam.gameObject.SetActive(true);
            jbCam.gameObject.SetActive(false);
            penpenCam.gameObject.SetActive(false);
            sparrotCam.gameObject.SetActive(false);

            ani.SetBool("atJB", false);

            shouldDoJBCam = false;
            shouldDoPenpenCam = false;
        }

        ani.SetBool("walking", nma.velocity.magnitude > 0.01f);
    }

    bool hasSetSong;
    IEnumerator DoSong()
    {
        hasSetSong = true;
        yield return new WaitForSeconds(3.5f);
        jbAudio.clip = jbClip1;
        jbAudio.Play();
        ani.SetBool("dance", true);
        shouldDoJBCam = false;

        foreach (Animator a in dancers)
            a.SetBool("doIt", true);
    }
    bool hasDoneCR;
    IEnumerator DoCountryRoad()
    {
        hasDoneCR = true;
        yield return new WaitForSeconds(3.5f);
        sparrot.shouldDance = true;
        jbAudio.clip = countryRoad;
        jbAudio.Play();
        ani.SetBool("croad", true);
        jbCam.LookAt = sparrot.transform;
    }

    private void OnAnimatorMove()
    {
        nma.speed = (ani.deltaPosition / Time.deltaTime).magnitude;
        transform.rotation = ani.rootRotation;
    }
}
