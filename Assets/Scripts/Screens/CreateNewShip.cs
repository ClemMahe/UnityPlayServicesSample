using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateNewShip : MonoBehaviour
{

    public Button createShipButton;
    public InputField shipNameInputField;


    // Start is called before the first frame update
    void Start()
    {    
        createShipButton.onClick.AddListener(checkShipNameFieldAndStartSceneIfNotEmpty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isNameValid(string name){
        bool isValid = true;
        if(name.Length==0) isValid=false;
        return isValid;
    }

    void checkShipNameFieldAndStartSceneIfNotEmpty(){
        string enteredShipName = shipNameInputField.text;
        if(isNameValid(enteredShipName)){
            ApplicationModel.shipName = enteredShipName;
            SceneManager.LoadScene("DebugUserScreen",LoadSceneMode.Single);
        }else{
            shipNameInputField.placeholder.color = Color.red;
            shipNameInputField.placeholder.GetComponent<Text>().text = "Please enter a name";
        }
    }

}
