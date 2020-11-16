using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserObjects
{
    // this the userobjects class which stores the state of the users.
    private int hitPoints;
    private string _uname;
    private string _color;
    public UserObjects(string uname,string color){
        _uname = uname;
        _color = color;
        hitPoints = 0;
    }

    public string getname(){
        return _uname;
    }
    public string getColor(){
        return _color;
    }
    public int getPoints(){
        return hitPoints;
    }
    public void updateHitPoints(int value){
        hitPoints += value;
    }
}
