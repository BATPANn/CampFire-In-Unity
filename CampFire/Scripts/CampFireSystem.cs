using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;
using System.Collections;

public class CampFireSystem : MonoBehaviour
{

    public GameObject[] freeWoods;

    public GameObject[] PlacedWoods;

    public TextMeshProUGUI Subtext;
    public GameObject TalkPanel;

    private bool CanInteract = true;

    private bool HaveWood = false;

    private int TakenWood = 0;
    private int PlacedWoodCount = 0;

    private bool CanStartFire = false;


    public GameObject ActualFire;


    // Update is called once per frame
    void Update()
    {


        if(CanInteract == true)
        {


            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100f))
            {


                if (hit.collider.CompareTag("Wood"))
                {

                    Debug.Log("Hit wood");
                    if (Input.GetMouseButtonDown(0))
                    {

                        if (HaveWood == true)
                        {

                            // cant take
                            
                            StartCoroutine(HandisFullCO());

                        }
                        else if (HaveWood == false)
                        {

                            // take wood
                            TakeWood();

                        }


                    }


                }
                else if (hit.collider.CompareTag("CampFire"))
                {



                    if(Input.GetMouseButtonDown (0))
                    {



                        if(CanStartFire == false)
                        {


                            if (HaveWood == true)
                            {

                                // place wood 
                                PlaceWood();

                            }
                            else if (HaveWood == false)
                            {

                                // i dont have wood
                                StartCoroutine(CantTakeWoodCO());

                            }



                        }
                        else if(CanStartFire == true)
                        {

                            ActualFire.SetActive(true);
                            CanInteract = false;


                        }
                        

                    }

                    



                }



            }



        }

        
        
    }





    void TakeWood()
    {


        HaveWood = true;

        if(TakenWood == 0)
        {

            freeWoods[0].SetActive(false);
            TakenWood = 1;

        }
        else if(TakenWood == 1)
        {

            freeWoods[1].SetActive(false);
            TakenWood = 2;

        }
        else if (TakenWood == 2)
        {

            freeWoods[2].SetActive(false);
            TakenWood = 3;

        }



    }

    void PlaceWood()
    {


        HaveWood = false;

        if(PlacedWoodCount == 0)
        {

            PlacedWoods[0].SetActive(true);
            PlacedWoodCount = 1;
        }
        else if(PlacedWoodCount == 1)
        {

            PlacedWoods[1].SetActive(true);
            PlacedWoodCount = 2;
        }
        else if (PlacedWoodCount == 2)
        {

            PlacedWoods[2].SetActive(true);
            PlacedWoodCount = 3;

            CanStartFire = true;

        }


    }

    IEnumerator CantTakeWoodCO()
    {


        CanInteract = false;

        TalkPanel.SetActive(true);
        Subtext.text = "I don't have any wood to place";

        yield return new WaitForSeconds(1.5f);

        TalkPanel.SetActive(false);
        Subtext.text = "";

        CanInteract = true;



    }


    IEnumerator HandisFullCO()
    {


        CanInteract = false;

        TalkPanel.SetActive(true);
        Subtext.text = "My hand is full";

        yield return new WaitForSeconds(1.5f);

        TalkPanel.SetActive(false);
        Subtext.text = "";

        CanInteract = true;


    }


}
