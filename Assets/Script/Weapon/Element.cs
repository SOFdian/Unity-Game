using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    //element用来存储元素值之类的
    public string[] element;
    public float[] num;
    //在这里
    public float speed;
    public float interval;
    private void Awake() {
        calculatSpeed();
        calculatInterval();
    }
    public bool hasCard(string str){
        if(element==null){
            element = new string[1];
            num = new float[1];
        }
        //检查是否已经获得了这张卡
        for(int i = 0; i < element.Length; i++){
            if(element[i] == str){
                return true;
            }
        }
        return false;
    }
    public float getCardNum(string str){
        for(int i = 0; i < element.Length; i++){
            if(element[i] == str){
                return num[i];
            }
        }
        return 0;
    }
    public void getCard(string str,float num){
        if(element==null){
            element = new string[1];
            this.num = new float[1];
            element[0] = str;
            this.num[0] = num;
            return;
        }
        //添加到element中
        string[] tempElement = new string[element.Length+1];
        float[] tempNum = new float[this.num.Length+1];
        for(int i = 0; i < element.Length; i++){
            tempElement[i] = element[i];
            tempNum[i] = this.num[i];
        }
        tempElement[element.Length] = str;
        tempNum[this.num.Length] = num;
        element = tempElement;
        this.num = tempNum;
    }
    public void ungetCard(string str){
        if(element.Length==1){
            element = null;
            num = null;
            return;
        }
        //从element中移除
        string[] tempElement = new string[element.Length-1];
        float[] tempNum = new float[this.num.Length-1];
        int j = 0;

        for(int i = 0; i < element.Length; i++){
            if(element[i] != str){

                tempElement[j] = element[i];
                tempNum[j] = this.num[i];
                j++;
            }
        }
        element = tempElement;
        this.num = tempNum;
    }


    public void calculatSpeed(){
        //根据element计算speed
        float sum = 0;
        for(int i = 0; i < element.Length; i++){
            if(element[i] == "speed"){
                sum += num[i];
            }
        }
        speed = sum;
    }
    public void calculatInterval(){
        //根据element计算interval
        float sum = 0;
        for(int i = 0; i < element.Length; i++){
            if(element[i] == "interval"){
                sum += num[i];
            }
        }
        interval = sum;
    }

}
