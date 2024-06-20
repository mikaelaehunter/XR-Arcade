#include <Keyboard.h>                               //Including the keyboard library
#include <Mouse.h>                                  //Including the mouse library

int up = 2;                                       //Declaring variables for the pins
int down = 3;
int left = 4;
int right = 5;
int esc = 6;
int space = 7;
int menu = 8;

int buttonStateUp = 0;
int buttonStateDown = 0;
int buttonStateLeft = 0;
int buttonStateRight = 0;
int buttonStateEsc = 0;
int buttonStateSpace = 0;
int buttonStateMenu = 0;

unsigned long lastDebounceTimeUp = 0;
unsigned long lastDebounceTimeDown = 0;
unsigned long lastDebounceTimeLeft = 0;
unsigned long lastDebounceTimeRight = 0;
unsigned long lastDebounceTimeEsc = 0;
unsigned long lastDebounceTimeSpace = 0;
unsigned long lastDebounceTimeMenu = 0;

unsigned long debounceDelay = 50; //debounce time in milliseconds

void setup() {

pinMode(up, INPUT_PULLUP);                        //Setting up the internal pull-ups resistors
pinMode(down, INPUT_PULLUP);                        //and also setting the pins to inputs.
pinMode(left, INPUT_PULLUP);
pinMode(right, INPUT_PULLUP);
pinMode(esc, INPUT_PULLUP);
pinMode(space, INPUT_PULLUP);
pinMode(menu, INPUT_PULLUP);

}

void loop() {

  upArrow();
  downArrow();
  leftArrow();
  rightArrow();
  escapeKey();
  spaceBar();
  menuKey();

}

void upArrow(){
  int reading = digitalRead(up);

  if (reading != buttonStateUp){
    lastDebounceTimeUp = millis();
  }

  if ((millis() - lastDebounceTimeUp) > debounceDelay){
    if (reading == LOW){
      Keyboard.press(KEY_UP_ARROW);
    } else {
        Keyboard.release(KEY_UP_ARROW);
      }
    }
    buttonStateUp = reading;
  }

void downArrow(){
  int reading = digitalRead(down);

  if (reading != buttonStateDown){
    lastDebounceTimeDown = millis();
  }

  if ((millis() - lastDebounceTimeDown) > debounceDelay){
    if (reading == LOW){
      Keyboard.press(KEY_DOWN_ARROW);
    } else {
        Keyboard.release(KEY_DOWN_ARROW);
      }
    }
    buttonStateDown = reading;
}

void leftArrow(){
  int reading = digitalRead(left);

  if (reading != buttonStateLeft){
    lastDebounceTimeLeft = millis();
  }

  if ((millis() - lastDebounceTimeLeft) > debounceDelay){
    if (reading == LOW){
      Keyboard.press(KEY_LEFT_ARROW);
    } else {
        Keyboard.release(KEY_LEFT_ARROW);
      }
    }
    buttonStateLeft = reading;
}

void rightArrow(){
  int reading = digitalRead(right);

  if (reading != buttonStateRight){
    lastDebounceTimeRight = millis();
  }

  if ((millis() - lastDebounceTimeRight) > debounceDelay){
    if (reading == LOW){
      Keyboard.press(KEY_RIGHT_ARROW);
    } else {
        Keyboard.release(KEY_RIGHT_ARROW);
      }
    }
    buttonStateRight = reading;
}

void escapeKey(){
  int reading = digitalRead(esc);

  if (reading != buttonStateEsc){
    lastDebounceTimeEsc = millis();
  }

  if ((millis() - lastDebounceTimeEsc) > debounceDelay){
    if (reading == LOW){
      Keyboard.press(KEY_ESC);
    } else {
        Keyboard.release(KEY_ESC);
      }
    }
    buttonStateEsc = reading;
}

void spaceBar(){
  int reading = digitalRead(space);

  if (reading != buttonStateSpace){
    lastDebounceTimeSpace = millis();
  }

  if ((millis() - lastDebounceTimeSpace) > debounceDelay){
    if (reading == LOW){
      Keyboard.press(' ');
    } else {
        Keyboard.release(' ');
      }
    }
    buttonStateSpace = reading;
}

void menuKey(){
  int reading = digitalRead(menu);

  if (reading != buttonStateMenu){
    lastDebounceTimeMenu = millis();
  }

  if ((millis() - lastDebounceTimeMenu) > debounceDelay){
    if (reading == LOW){
      Keyboard.press(KEY_MENU);
    } else {
        Keyboard.release(KEY_MENU);
      }
    }
    buttonStateMenu = reading; 
}
