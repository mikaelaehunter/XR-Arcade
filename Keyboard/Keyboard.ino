#include <Keyboard.h>                               //Including the keyboard library
#include <Mouse.h>                                  //Including the mouse library

int up = 2;                                       //Declaring variables for the pins
int down = 3;
int left = 4;
int right = 5;
int esc = 6;
int space = 7;
int menu = 8;

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

  if (digitalRead(up) == LOW)                     //Checking if the first switch has been pressed
  {
    Keyboard.press(KEY_UP_ARROW);
    delay(100);  // Hold the key for a short duration
    Keyboard.release(KEY_UP_ARROW);
    delay(100);  // Debounce delay
  }

  if (digitalRead(down) == LOW)                     //Checking if the second switch has been pressed
  {
    Keyboard.press(KEY_DOWN_ARROW);
    delay(100);  // Hold the key for a short duration
    Keyboard.release(KEY_DOWN_ARROW);
    delay(100);  // Debounce delay
  }

  if (digitalRead(left) == LOW)                     //Checking if the third switch has been pressed
  {
    Keyboard.press(KEY_LEFT_ARROW);
    delay(100);  // Hold the key for a short duration
    Keyboard.release(KEY_LEFT_ARROW);
    delay(100);  // Debounce delay
  }

  if (digitalRead(right) == LOW)                     //Checking if the fourth switch has been pressed
  {
    Keyboard.press(KEY_RIGHT_ARROW);
    delay(100);  // Hold the key for a short duration
    Keyboard.release(KEY_RIGHT_ARROW);
    delay(100);  // Debounce delay
  }

  if (digitalRead(esc) == LOW)                     //Checking if the fifth switch has been pressed
  {
    Keyboard.press(KEY_ESC);
    delay(100);  // Hold the key for a short duration
    Keyboard.release(KEY_ESC);
    delay(100);  // Debounce delay                                    //Sending a string and a return
  }

  if (digitalRead(space) == LOW)
  {
    Keyboard.press(' ');
    delay(100);  // Hold the key for a short duration
    Keyboard.release(' ');
    delay(100);  // Debounce delay
  }

  if (digitalRead(menu) == LOW)
  {
    Keyboard.press(KEY_MENU);
    delay(100);  // Hold the key for a short duration
    Keyboard.release(KEY_MENU);
    delay(100);  // Debounce delay
  }
  
  delay(10); //debounce delay

}