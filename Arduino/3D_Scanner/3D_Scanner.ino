
#define sensor A0
#define messwerteAnzahl 10
int Messwert = 0;
int messwerte[messwerteAnzahl];
int counter = 0;
void setup() {               
  pinMode(8, OUTPUT);     //dir
  pinMode(9, OUTPUT);
  digitalWrite(8, LOW);   //dir
  digitalWrite(9, LOW);
  pinMode(10, OUTPUT);     
  pinMode(11, OUTPUT);     //dir
  digitalWrite(10, LOW);
  digitalWrite(11, LOW);   //dir 
  Serial.begin(9600);

}

void loop() {
  
  float volts = analogRead(sensor)*0.0048828125;  // value from sensor * (5/1024)
  int distance = 13*pow(volts, -1) * 10; // worked out from datasheet graph
  delay(100); // slow down serial port 
  //Serial.println(distance);
  if (distance <= 300 && distance >= 40){
    messwerte[counter++] = distance;
    Serial.println(distance);
    if (counter == messwerteAnzahl) {
      counter = 0;
      Serial.print(mittelwert());   // print the distance
      Serial.println(" mm");
      Serial.println("----------------");
    }
  }
}

int mittelwert() {
  float result = 0.0;
  for (int i = 0; i < messwerteAnzahl; i++) {
    //Serial.println(messwerte[i]);
    result += messwerte[i];
  }
  return (int) (result / messwerteAnzahl);
}
