using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserObjects
{
    // this the userobjects class which stores the state of the users.
    private int hitPoints;
    private string _uname;
    private string _color;
    private string _uuid;
    private int showing;
    public UserObjects(string uname,string uuid){
        _uname = uname;
        _uuid = uuid;
        hitPoints = 0;
        showing = 0;
    }
    public void setShowing(){
        showing= 1;
    }
    public int getShowing(){
        return showing;
    }
    public string getname(){
        return _uname;
    }
    public string getuuid(){
        return _uuid;
    }
    public void setUserName(string name){
        _uname = name;
    }
    public string getColor(){
        return _color;
    }
    public void setColor(string color){
        _color = color;
    }
    public int getPoints(){
        return hitPoints;
    }
    public void updateHitPoints(int value){
        hitPoints += value;
    }
}
