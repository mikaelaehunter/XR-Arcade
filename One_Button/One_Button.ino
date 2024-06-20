#include "Keyboard.h"

//declaring button pins
const int down = 3; 

int buttonPushCounter = 0;
int buttonState = 0;
int lastButtonState = 0;

void setup() {
  //declare the buttons as input_pullup
  pinMode(down, INPUT_PULLUP);
  Serial.begin(9600);  
}

void loop() {

  edgeDetection();

}

void edgeDetection(){
  buttonState = digitalRead(down);
  if (buttonState != lastButtonState){
    if (buttonState == LOW){
      buttonPushCounter++;
      Serial.println("on");
      Serial.print("number of button pushes: ");
      Serial.println(buttonPushCounter);
    } else {
        Serial.println("off");
    }
    delay(50);
    }
  lastButtonState = buttonState;
  }
