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
  Keyboard.begin();
}

void loop() {

  //downArrow();
  edgeDetection();

}

void downArrow(){
  if (digitalRead(down) == LOW)                     //Checking if the first switch has been pressed
  {
    Keyboard.press(KEY_DOWN_ARROW);
    delay(100);
    Keyboard.release(KEY_DOWN_ARROW);
    delay(100);
  }
}

void edgeDetection(){
  buttonState = digitalRead(down);
  if (buttonState != lastButtonState){
    if (buttonState == LOW){
      Keyboard.press(KEY_DOWN_ARROW);
      delay(10);
      buttonPushCounter++;
      Keyboard.println("on");
      Keyboard.print("number of button pushes: ");
      Keyboard.println(buttonPushCounter);
    } else {
        Keyboard.release(KEY_DOWN_ARROW);
        delay(10);
        Keyboard.println("off");
    }
    delay(50);
    }
  lastButtonState = buttonState;
  }
