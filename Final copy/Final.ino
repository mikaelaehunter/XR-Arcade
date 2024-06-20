#include <Keyboard.h>                               //Including the keyboard library
#include <Mouse.h>                                  //Including the mouse library

// Declaring button pin
const int up = 2;                                       //Declaring variables for the pins
const int down = 3;
const int left = 4;
const int right = 5;
const int esc = 6;
const int space = 7;
const int menu = 8;

// Variable to store the previous state of the button
bool previousButtonStateUp = HIGH;
bool previousButtonStateDown = HIGH;
bool previousButtonStateLeft = HIGH;
bool previousButtonStateRight = HIGH;
bool previousButtonStateEsc = HIGH;
bool previousButtonStateSpace = HIGH;
bool previousButtonStateMenu = HIGH;

void setup() {

  pinMode(up, INPUT_PULLUP);                        //Setting up the internal pull-ups resistors
  pinMode(down, INPUT_PULLUP);                        //and also setting the pins to inputs.
  pinMode(left, INPUT_PULLUP);
  pinMode(right, INPUT_PULLUP);
  pinMode(esc, INPUT_PULLUP);
  pinMode(space, INPUT_PULLUP);
  pinMode(menu, INPUT_PULLUP);
  Keyboard.begin();

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

void upArrow() {
  
  bool currentButtonStateUp = digitalRead(up);                        // Read the current state of the button

  if (previousButtonStateUp == HIGH && currentButtonStateUp == LOW) {     // Detect the falling edge (button press)
    Keyboard.press(KEY_UP_ARROW);     // Button press detected
  } 
 
  if (previousButtonStateUp == LOW && currentButtonStateUp == HIGH) {     // Detect the rising edge (button release)
    Keyboard.release(KEY_UP_ARROW);                                 // Button release detected
  }

  previousButtonStateUp = currentButtonStateUp;                           // Update the previous button state

}

void downArrow() {
  
  bool currentButtonStateDown = digitalRead(down);                        // Read the current state of the button

  if (previousButtonStateDown == HIGH && currentButtonStateDown == LOW) {     // Detect the falling edge (button press)
    Keyboard.press(KEY_DOWN_ARROW);     // Button press detected
  } 
 
  if (previousButtonStateDown == LOW && currentButtonStateDown == HIGH) {     // Detect the rising edge (button release)
    Keyboard.release(KEY_DOWN_ARROW);                                 // Button release detected
  }

  previousButtonStateDown = currentButtonStateDown;                           // Update the previous button state

}

void leftArrow() {
  
  bool currentButtonStateLeft = digitalRead(left);                        // Read the current state of the button

  if (previousButtonStateLeft == HIGH && currentButtonStateLeft == LOW) {     // Detect the falling edge (button press)
    Keyboard.press(KEY_LEFT_ARROW);     // Button press detected
  } 
 
  if (previousButtonStateLeft == LOW && currentButtonStateLeft == HIGH) {     // Detect the rising edge (button release)
    Keyboard.release(KEY_LEFT_ARROW);                                 // Button release detected
  }

  previousButtonStateLeft = currentButtonStateLeft;                           // Update the previous button state

}

void rightArrow() {
  
  bool currentButtonStateRight = digitalRead(right);                        // Read the current state of the button

  if (previousButtonStateRight == HIGH && currentButtonStateRight == LOW) {     // Detect the falling edge (button press)
    Keyboard.press(KEY_RIGHT_ARROW);     // Button press detected
  } 
 
  if (previousButtonStateRight == LOW && currentButtonStateRight == HIGH) {     // Detect the rising edge (button release)
    Keyboard.release(KEY_RIGHT_ARROW);                                 // Button release detected
  }

  previousButtonStateRight = currentButtonStateRight;                           // Update the previous button state

}

void escapeKey() {
  
  bool currentButtonStateEsc = digitalRead(esc);                        // Read the current state of the button

  if (previousButtonStateEsc == HIGH && currentButtonStateEsc == LOW) {     // Detect the falling edge (button press)
    Keyboard.press(KEY_ESC);     // Button press detected
  } 
 
  if (previousButtonStateEsc == LOW && currentButtonStateEsc == HIGH) {     // Detect the rising edge (button release)
    Keyboard.release(KEY_ESC);                                 // Button release detected
  }

  previousButtonStateEsc = currentButtonStateEsc;                           // Update the previous button state

}

void spaceBar() {
  
  bool currentButtonStateSpace = digitalRead(space);                        // Read the current state of the button

  if (previousButtonStateSpace == HIGH && currentButtonStateSpace == LOW) {     // Detect the falling edge (button press)
    Keyboard.press(' ');     // Button press detected
  } 
 
  if (previousButtonStateSpace == LOW && currentButtonStateSpace == HIGH) {     // Detect the rising edge (button release)
    Keyboard.release(' ');                                 // Button release detected
  }

  previousButtonStateSpace = currentButtonStateSpace;                           // Update the previous button state

}

void menuKey() {
  
  bool currentButtonStateMenu = digitalRead(menu);                        // Read the current state of the button

  if (previousButtonStateMenu == HIGH && currentButtonStateMenu == LOW) {     // Detect the falling edge (button press)
    Keyboard.press(KEY_MENU);     // Button press detected
  } 
 
  if (previousButtonStateMenu == LOW && currentButtonStateMenu == HIGH) {     // Detect the rising edge (button release)
    Keyboard.release(KEY_MENU);                                 // Button release detected
  }

  previousButtonStateMenu = currentButtonStateMenu;                           // Update the previous button state

}

