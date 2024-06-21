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

//declaring joystick pins
const int xPin = A0;  // Analog output of horizontal joystick pin
const int yPin = A1;  // Analog output of vertical joystick pin

//declaring joystick variables
int yZero, xZero;  // Stores the initial value of each axis, usually around 512
int yValue, xValue;  // Stores current analog output of each axis
const int sensitivity = 100;  // Higher sensitivity value = slower mouse, should be <= about 500
int mouseClickFlag = 0;
int invertMouse = 1; //Value to invert joystick based on orientation

// Variable to store the previous state of the button
bool previousButtonStateUp = HIGH;
bool previousButtonStateDown = HIGH;
bool previousButtonStateLeft = HIGH;
bool previousButtonStateRight = HIGH;
bool previousButtonStateEsc = HIGH;
bool previousButtonStateSpace = HIGH;
bool previousButtonStateMenu = HIGH;

void setup() {
  //buttons
  pinMode(up, INPUT_PULLUP);                        //Setting up the internal pull-ups resistors
  pinMode(down, INPUT_PULLUP);                        //and also setting the pins to inputs.
  pinMode(left, INPUT_PULLUP);
  pinMode(right, INPUT_PULLUP);
  pinMode(esc, INPUT_PULLUP);
  pinMode(space, INPUT_PULLUP);
  pinMode(menu, INPUT_PULLUP);
  Keyboard.begin();

  //joystick
  pinMode(xPin, INPUT);  // Set both analog pins as inputs
  pinMode(yPin, INPUT);
  delay(1000);  // short delay to let outputs settle
  yZero = analogRead(yPin);  // get the initial values
  xZero = analogRead(xPin);  // Joystick should be in neutral position when reading these
}

void loop() {

  edgeDetection(up, previousButtonStateUp, KEY_UP_ARROW);
  edgeDetection(down, previousButtonStateDown, KEY_DOWN_ARROW);
  edgeDetection(left, previousButtonStateLeft, KEY_LEFT_ARROW);
  edgeDetection(right, previousButtonStateRight, KEY_RIGHT_ARROW);
  edgeDetection(esc, previousButtonStateEsc, KEY_ESC);
  edgeDetection(space, previousButtonStateSpace, ' ');
  edgeDetection(menu, previousButtonStateMenu, KEY_MENU);
  joystick();

}

void edgeDetection(int buttonPin, bool &previousButtonState, uint8_t key){
  bool currentButtonState = digitalRead(buttonPin); // Read the current state of the button
  if (previousButtonState == HIGH && currentButtonState == LOW){ // Detect the falling edge (button press)
    Keyboard.press(key);  // Button press detected
  }
  if (previousButtonState == LOW && currentButtonState == HIGH) {
    Keyboard.release(key); // Button release detected
  }
  previousButtonState = currentButtonState;
}

void joystick(){
  yValue = analogRead(yPin) - yZero;  // read vertical offset
  xValue = analogRead(xPin) - xZero;  // read horizontal offset

  if (yValue != 0)
    Mouse.move(0, (invertMouse * (yValue / sensitivity)), 0); // move mouse on y axis
  if (xValue != 0)
    Mouse.move((invertMouse * (xValue / sensitivity)), 0, 0); // move mouse on x axis
}
