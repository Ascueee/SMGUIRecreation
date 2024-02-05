using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManage : MonoBehaviour
{
    //handles the states 
    [Header("Menu States")]
    [SerializeField] bool stateOne_;
    [SerializeField] bool stateTwo_;
    [SerializeField] bool stateThree_;
    [SerializeField] bool stateFour_;
    [SerializeField] bool stateFive_;
    [SerializeField] bool stateSix_;

    [Header("UI Components")]
    [SerializeField] Image logo_;
    [SerializeField] Text entrenceText_;
    [SerializeField] Vector3 textScale_;
    [SerializeField] float logoColorChangeTime_,logoAlphaChangeTime;

    [Header("Planet Components")]
    [SerializeField] GameObject[] individualPlanets_;
    [SerializeField] Vector3[] underMenuPositions_;
    [SerializeField] Vector3[] inMenuPositions_;
    
    [Header("Camera Components")]
    [SerializeField] GameObject menuCamera_;
    [SerializeField] Transform zoomPosOne_;
    [SerializeField] float zoomIntime_;

    [Header("State Two Components")]
    [SerializeField] GameObject menuPlanets_;
    [SerializeField] Transform planetMovePosition;
    [SerializeField] Image pleaseSelectAFile_;
    [SerializeField] Text pleaseSelectAFileText_;
    [SerializeField] float menuPlanetsLerpTime;

    [Header("State Three Components")]
    [SerializeField] int planetSelection_;
    [SerializeField] Transform planetMovePositionStateThree;
    [SerializeField] float menuStateThreeLerpTime;

    [Header("State Four Components")]
    [SerializeField] float countDown;
    float countDownTimer;
    [SerializeField] Image stateFourUIImage;
    [SerializeField] Vector3 imageFourUIOgPos;
    [SerializeField] Vector3 imageFourExitPos;
    [SerializeField] float menuStateFourLerpTime;

    [Header("State Five Components")]
    [SerializeField] Image selectSaveIconUI;
    [SerializeField] float menuStateFiveLerpTime;
    bool selectedYes;
    int increment;




    // Start is called before the first frame update
    void Start()
    {
        stateOne_ = true;
        planetSelection_ = -1;
        pleaseSelectAFile_.enabled = true;
        pleaseSelectAFileText_.enabled = true;

        countDownTimer = countDown;
        //GetUnderMenuPlanetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        PlanetSelectionCheck();
        HandleStates();
    }

    //this is the main method that handles all the states
    void HandleStates()
    {
        LeanTween.init(1000);

        //state one handles the logo change
        if (stateOne_)
        {
            StateOne();

        }

        //zoom on the camera to the planets coming up
        if (stateTwo_)
        {

            StateTwo();


            stateTwo_ = false;
        }

        if (stateThree_)
        {
            for (int i = 0; i < individualPlanets_.Length; i++)
            {

                individualPlanets_[i].GetComponent<PlanetProperties>().SetStateTwo(false);
            }



            StateThree();
        }

        if (stateFour_ == true && stateFive_ == false)
        {
            StateFour();
            stateFour_ = false;

        }

        //state five handles the button for stateFour selection
        if (stateFive_ == true && stateSix_ == false)
        {

            StateFive();

        }

        if(stateSix_ == true)
        {
            stateSix_ = false;
            stateFive_ = false;
        }


    }

    //State Functions

    //the intro screen logo
    void StateOne()
    {
        

        if (Input.anyKeyDown)
        {
            //this changes the UIs Color 
            logo_.color = Color.red;

            //this changes the alpha of the color
            LeanTween.value(logo_.gameObject, logo_.color.a, 0f, logoColorChangeTime_).setOnUpdate(ChangeLogoAlpha);
            LeanTween.scale(entrenceText_.gameObject, textScale_, logoColorChangeTime_);
            LeanTween.value(entrenceText_.gameObject, entrenceText_.color.a, 0f, logoColorChangeTime_).setOnUpdate(ChangeTextAlpha);

        }

    }

    //brings the planet up to the camera
    void StateTwo()
    {
        pleaseSelectAFile_.enabled = true;
        pleaseSelectAFileText_.enabled = true;

        print(planetSelection_);
        countDownTimer -= Time.deltaTime;


        //zooms in the camera to get a closer look at the planets
        LeanTween.move(menuCamera_, zoomPosOne_, zoomIntime_).setEaseInOutSine();

        
        //lerps all the individual planets to a inMenuPosition to
        LeanTween.move(individualPlanets_[0], inMenuPositions_[0], menuPlanetsLerpTime).setEaseInOutSine();
        LeanTween.move(individualPlanets_[1], inMenuPositions_[1], menuPlanetsLerpTime).setEaseInOutSine();
        LeanTween.move(individualPlanets_[2], inMenuPositions_[2], menuPlanetsLerpTime).setEaseInOutSine();
        LeanTween.move(individualPlanets_[3], inMenuPositions_[3], menuPlanetsLerpTime).setEaseInOutSine();
        LeanTween.move(individualPlanets_[4], inMenuPositions_[4], menuPlanetsLerpTime).setEaseInOutSine();
        LeanTween.move(individualPlanets_[5], inMenuPositions_[5], menuPlanetsLerpTime).setEaseInOutSine();


        LeanTween.value(pleaseSelectAFile_.gameObject, pleaseSelectAFile_.color.a, 1f , 1f).setOnUpdate(ChangeSelectFileAlpha);
        LeanTween.value(pleaseSelectAFileText_.gameObject, pleaseSelectAFileText_.color.a, 1f, 1f).setOnUpdate(ChangeSelectFileTextAlpha);

        for (int i = 0; i < individualPlanets_.Length;i++)
        {

            individualPlanets_[i].GetComponent<PlanetProperties>().SetStateTwo(true);
        }


    }


    //selecting one of the planets from the planet rotation screen
    void StateThree()
    {


        //this switch statement 
        switch (planetSelection_)
        {
            case 0:
                LeanTween.move(individualPlanets_[0], planetMovePositionStateThree, menuStateThreeLerpTime).setEaseInOutSine();
                MovePlanetsDown();
                break;
            case 1:
                LeanTween.move(individualPlanets_[1], planetMovePositionStateThree, menuStateThreeLerpTime).setEaseInOutSine();
                MovePlanetsDown();
                break;
            case 2:
                LeanTween.move(individualPlanets_[2], planetMovePositionStateThree, menuStateThreeLerpTime).setEaseInOutSine();
                MovePlanetsDown();
                break;
            case 3:
                LeanTween.move(individualPlanets_[3], planetMovePositionStateThree, menuStateThreeLerpTime).setEaseInOutSine();
                MovePlanetsDown();
                break;
            case 4:
                LeanTween.move(individualPlanets_[4], planetMovePositionStateThree, menuStateThreeLerpTime).setEaseInOutSine();
                MovePlanetsDown();
                break;
            case 5:
                LeanTween.move(individualPlanets_[5], planetMovePositionStateThree, menuStateThreeLerpTime).setEaseInOutSine();
                MovePlanetsDown();
                break;

        }
        //sets the planetselection_ int to 50 to not coincide with other planetselection cases so it does not go into a infinite loop
        planetSelection_ = 50;

        pleaseSelectAFile_.enabled = false;
        pleaseSelectAFileText_.enabled = false;



        //sets the different states 
        stateThree_ = false;
        stateFour_ = true;


    }


    //brings in the UI for selecting the planet
    void StateFour()
    {
        //gives a countdown to give the player some time to process the transition
        countDownTimer -= Time.deltaTime;

        //checks if the time is done
        if(countDownTimer <= 0)
        {
            //lerps the confirmation UI to the selected planet
            LeanTween.move(stateFourUIImage.rectTransform, planetMovePositionStateThree.transform.position, menuStateFourLerpTime);

            //switches state
            stateFour_ = false;
            //resets the countdown timer for future transition
            countDownTimer = countDown;
        }
    }

    void StateFive()
    {
        if(selectedYes == true)
        {
            
        }
        stateFive_ = false;
        stateSix_ = true;
    }


    //this method is called when the "NO" button is pressed on the confirmation UI
    public void SelectionOfNo()
    {
        //the confirmation UI lerps back to its original position
        LeanTween.move(stateFourUIImage.rectTransform, imageFourUIOgPos, menuStateFourLerpTime).setEaseInOutSine();

        //resets the planet selection to -2
        planetSelection_ = -2;

        //switches the state back to stateTwo
        stateTwo_ = true;
        stateFive_ = false;

    }

    //this method is called when the 
    public void SelectionOfYes()
    {
        SetStateFive();
    }

    void SetStateFive()
    {
        selectedYes = true;
        stateFive_ = true;
 
    }


    //Moves all the other planets that were not selected in state three
    void MovePlanetsDown()
    {
        
        for(int i = 0; i < individualPlanets_.Length; i++)
        {
            //checks if the selectedplanet is not chosen which then indicates that the planet can move down
            if(i != planetSelection_)
            {
                LeanTween.move(individualPlanets_[i], underMenuPositions_[i], menuStateThreeLerpTime).setEaseInOutSine();
            }
        }
    }

    //this is used to go to statethree and back to state two if the player choices to do so
    void PlanetSelectionCheck()
    {
        if (planetSelection_ > -1)
        {
            stateThree_ = true;

        }
        if(planetSelection_ == -2)
        {
            stateTwo_ = true;
            planetSelection_ = -1;
        }
    }

    //LEAN TWEEN FUNCTIONS FOR STATE ONE
    void ChangeLogoAlpha(float a)
    {
        //this sets a new vector with the lerped alpha channel
        var alphaChange = new Vector4(logo_.color.r, logo_.color.g, logo_.color.b, a);

        logo_.color = alphaChange;

        //checks if the logo has faded out and if so then changes moves on to the next state
        if(logo_.color.a == 0f)
        {
            stateTwo_ = true;
            stateOne_ = false;
        }

    }

    //LEAN TWEEN FUNTIONS FOR STATE TWO
    void ChangeSelectFileAlpha(float a)
    {
        //this sets a new vector with the lerped alpha channel
        var alphaChange = new Vector4(pleaseSelectAFile_.color.r, pleaseSelectAFile_.color.g, pleaseSelectAFile_.color.b, a);

        pleaseSelectAFile_.color = alphaChange;

    }

    void ChangeSelectFileTextAlpha(float a)
    {
        //this sets a new vector with the lerped alpha channel
        var alphaChange = new Vector4(pleaseSelectAFileText_.color.r, pleaseSelectAFileText_.color.g, pleaseSelectAFileText_.color.b, a);

        pleaseSelectAFileText_.color = alphaChange;

    }

    void ChangeTextAlpha(float a)
    {
        //this sets a new vector with the lerped alpha channel
        var alphaChange = new Vector4(entrenceText_.color.r, entrenceText_.color.g, entrenceText_.color.b, a);

        entrenceText_.color = alphaChange;

    }

    //LEAN TWEEN FUNCTIONS FOR STATE TWO

    //getter and setter methods
    public void SetPlanetSelection_(int planetIndex)
    {
        planetSelection_ = planetIndex;
    }
}
