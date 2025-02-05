#include "LiquidCrystal.h"

LiquidCrystal lcd(12, 11, 10, 9, 8, 7, 6, 5, 4, 3);
//LiquidCrystal lcd(12, 11, 6, 5, 4, 3);

const int trigPin = 2; 
const int echoPin = 13; 
long duration;
float distance;
float distanceToJump = 7.;

void setup(){
  lcd.begin(16, 2);

  Serial.begin(115200);
  // Wait for a serial connection
  while (!Serial.availableForWrite());

  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
}

void loop(){
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);

  duration = pulseIn(echoPin, HIGH);
  distance = duration * 0.034 / 2; // Convertir en cm

  if (distance < distanceToJump) {
    Serial.println("JUMP");
  }

  delay(200);
}

// Handles incoming messages
// Called by Arduino if any serial data has been received
void serialEvent()
{
  String message = Serial.readStringUntil('\n');
  if (message == "Nothing Unlocked Yet") {
    lcd.clear();
    lcd.print("No power-up yet");
  }
  else if (message == "Plateforms Unlocked") {
    lcd.clear();
    lcd.print("You have unlocke");
    lcd.setCursor(0, 1);
    lcd.print("d the Plateforms");
  }
  else if (message == "Jump Unlocked") {
    lcd.clear();
    lcd.print("You have unlocke");
    lcd.setCursor(0, 1);
    lcd.print("d the Jump");
  }
  else if (message == "Inventory Unlocked") {
    lcd.clear();
    lcd.print("You have unlocke");
    lcd.setCursor(0, 1);
    lcd.print("d the Inventory");
  }
  else if (message == "Walljump Unlocked") {
    lcd.clear();
    lcd.print("You have unlocke");
    lcd.setCursor(0, 1);
    lcd.print("d the WallJump");
  }
  else if (message == "Gun Unlocked") {
    lcd.clear();
    lcd.print("You have unlocke");
    lcd.setCursor(0, 1);
    lcd.print("d the Gun");
  }
}
