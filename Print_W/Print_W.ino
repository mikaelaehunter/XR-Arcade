#include <Keyboard.h>

void setup() {
  Keyboard.begin();
  delay(1000);
}

void loop() {
  Keyboard.press('w');
  delay(100);
  Keyboard.releaseAll();
  delay(1000); 
}
