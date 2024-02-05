using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlanetProperties : MonoBehaviour
{
    [SerializeField] MenuManage menuManager_;
    [SerializeField] Image icon;
    [SerializeField] float rotateSpeed;
    [SerializeField] int numOfPlanet;
    [SerializeField] float countDown;
    float countDownTimer;
    bool inStateTwo = false;

    // Start is called before the first frame update
    void Start()
    {
        icon.color = Color.white;
        icon.enabled = false;
        countDownTimer = countDown;
    }

    // Update is called once per frame
    void Update()
    {
        LeanTween.init();
        PlanetIcons();
        RotatePlanet();


    }

    void PlanetIcons()
    {
        if (inStateTwo)
        {
            countDownTimer -= Time.deltaTime;

            if(countDownTimer <= 0)
            {
                icon.enabled = true;
                countDownTimer = countDown;

            }
        }

        if(inStateTwo == false)
        {
            icon.enabled = false;
        }
    }

    void RotatePlanet()
    {
        
        transform.Rotate(0f,0f, rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void OnMouseDown()
    {

        //set the planet selection
        menuManager_.SetPlanetSelection_(numOfPlanet);
    }

    private void OnMouseEnter()
    {
        icon.color = Color.blue;
        rotateSpeed = 50; //speeds up the rotation of the planets
    }
    private void OnMouseExit()
    {
        icon.color = Color.white;
        rotateSpeed = 25;
    }

    public void SetStateTwo(bool setState)
    {
        inStateTwo = setState;
    }


}
