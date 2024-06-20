#include "Keyboard.h"

// Declaring button pin
const int up = 2;

// Variable to store the previous state of the button
bool previousButtonState = HIGH;

void setup() {
  // Declare the button as input_pullup
  pinMode(up, INPUT_PULLUP);
  Keyboard.begin();
}

void loop() {
  edgeDetection();
}

void edgeDetection() {
  // Read the current state of the button
  bool currentButtonState = digitalRead(up);

  // Detect the falling edge (button press)
  if (previousButtonState == HIGH && currentButtonState == LOW) {
    // Button press detected
    //downArrow();
    Keyboard.press(KEY_UP_ARROW);
  } 

  // Detect the rising edge (button release)
  if (previousButtonState == LOW && currentButtonState == HIGH) {
    // Button release detected
    Keyboard.release(KEY_UP_ARROW);
  }

  // Update the previous button state
  previousButtonState = currentButtonState;

}
