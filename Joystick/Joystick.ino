#include <Mouse.h>

int xPin = A0;  // Analog output of horizontal joystick pin
int yPin = A1;  // Analog output of vertical joystick pin

int yZero, xZero;  // Stores the initial value of each axis, usually around 512
int yValue, xValue;  // Stores current analog output of each axis
const int sensitivity = 100;  // Higher sensitivity value = slower mouse, should be <= about 500
int mouseClickFlag = 0;
int invertMouse = 1; //Value to invert joystick based on orientation

void setup()
{
  pinMode(xPin, INPUT);  // Set both analog pins as inputs
  pinMode(yPin, INPUT);
  delay(1000);  // short delay to let outputs settle
  yZero = analogRead(yPin);  // get the initial values
  xZero = analogRead(xPin);  // Joystick should be in neutral position when reading these
}

void loop()
{
  yValue = analogRead(yPin) - yZero;  // read vertical offset
  xValue = analogRead(xPin) - xZero;  // read horizontal offset

  if (yValue != 0)
    Mouse.move(0, (invertMouse * (yValue / sensitivity)), 0); // move mouse on y axis
  if (xValue != 0)
    Mouse.move((invertMouse * (xValue / sensitivity)), 0, 0); // move mouse on x axis
}