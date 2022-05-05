using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpState : MonoBehaviour
{
    public bool Shield = false;
    public int DoubleJumps = 0;

    [SerializeField] private GameObject ShieldSprite;
    [SerializeField] private GameObject DoubleJumpSprite;

    void Awake(){
        ShieldSprite.SetActive(false);
        DoubleJumpSprite.SetActive(false);
    }

    void Start(){
        ResetPowerUps();
        ShieldSprite.SetActive(false);
        DoubleJumpSprite.SetActive(false);
        // Debug.Log("Double Jump" + DoubleJumps);
    }

    void Update()
    {
        if(Shield){
            ShieldSprite.SetActive(true);
        }else{
            ShieldSprite.SetActive(false);
        }

        if(DoubleJumps > 0){
            DoubleJumpSprite.SetActive(true);
        }else{
            DoubleJumpSprite.SetActive(false);
        }
    }

    public void DestroyShield(){
        Shield = false;
    }

    public void HasDoubleJumped(){
        Debug.Log("Double Jump" + DoubleJumps);
        if(DoubleJumps > 0){
            DoubleJumps--;
        }
    }

    public void ResetPowerUps(){
        Shield = false;
        DoubleJumps = 0;
    }
}
