#include "Mouse.h" // include mouse library

// set pin numbers for switch, joystick axes, and LED:
const int switchPin = 9;    // switch to turn on and off mouse control
const int xAxis = A0;       // joystick X axis
const int yAxis = A1;       // joystick Y axis
const int ledPin = 10;       // Mouse control LED

// parameters for reading the joystick:
int range = 12;             // output range of X or Y movement
int responseDelay = 5;      // response delay of the mouse, in ms
int threshold = range / 4;  // resting threshold
int center = range / 2;     // resting position value

bool mouseIsActive = false;  // whether or not to control the mouse
int lastSwitchState = LOW;   // previous switch state

void setup() {
  pinMode(switchPin, INPUT);  // the switch pin
  pinMode(ledPin, OUTPUT);    // the LED pin
  
  Mouse.begin();              // take control of the mouse:
}

void loop() {
  
  int switchState = digitalRead(switchPin);             // read the switch:
  
  if (switchState != lastSwitchState) {                 // if it's changed and it's high, toggle the mouse state:
    if (switchState == HIGH) {
      mouseIsActive = !mouseIsActive;
      digitalWrite(ledPin, mouseIsActive);              // turn on LED to indicate mouse state:
    }
  }

  lastSwitchState = switchState;                        // save switch state for next comparison:

  int xReading = readAxis(A0);                          // read and scale the two axes:
  int yReading = readAxis(A1);

  if (mouseIsActive) {                                  // if the mouse control state is active, move the mouse:
    Mouse.move(xReading, yReading, 0);
  }

  delay(responseDelay);
}

int readAxis(int thisAxis) {                              //reads an axis (0 or 1 for x or y) and scales the analog input range to a range from 0 to <range>
  
  int reading = analogRead(thisAxis);                     // read the analog input:
  reading = map(reading, 0, 1023, 0, range);              // map the reading from the analog input range to the output range:
  int distance = reading - center;                        // if the output reading is outside from the rest position threshold, use it:

  if (abs(distance) < threshold) {
    distance = 0;
  }

  return distance;                                        // return the distance for this axis:
}
