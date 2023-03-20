
// Move these OUTSIDE the loop function
int recdata[10];
int bytes = 0;

void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  pinMode(2,OUTPUT);
  pinMode(3,OUTPUT);
  pinMode(4,OUTPUT);
  pinMode(5,OUTPUT);
  pinMode(6,OUTPUT);
  pinMode(7,OUTPUT);
  pinMode(8,OUTPUT);
  pinMode(9,OUTPUT);
  pinMode(10,OUTPUT);
  pinMode(11,OUTPUT);
  pinMode(12,OUTPUT);
  pinMode(13,OUTPUT);
}

// the loop routine runs over and over again forever:
void loop() {
  if(Serial.available())
  {     
    while(Serial.available())
    {
      recdata[bytes]=Serial.read();
      bytes++;
    }
    if (bytes >= 12)
    {
      checkdata(); //function which checks the received data
      bytes = 0;
    }
  }
}

void checkdata(){
  if (bytes == 12){
    if (recdata[0] == 1){
      digitalWrite(2, HIGH);
    }
    else{
      digitalWrite(2, LOW);
    }

    if (recdata[1] == 1){
      digitalWrite(3, HIGH);
    }
    else{
      digitalWrite(3, LOW);
    }

    if (recdata[2] == 1){
      digitalWrite(4, HIGH);
    }
    else{
      digitalWrite(4, LOW);
    }

    if (recdata[3] == 1){
      digitalWrite(5, HIGH);
    }
    else{
      digitalWrite(5, LOW);
    }

    if (recdata[4] == 1){
      digitalWrite(6, HIGH);
    }
    else{
      digitalWrite(6, LOW);
    }

    if (recdata[5] == 1){
      digitalWrite(7, HIGH);
    }
    else{
      digitalWrite(7, LOW);
    }

    if (recdata[6] == 1){
      digitalWrite(8, HIGH);
    }
    else{
      digitalWrite(8, LOW);
    }

    if (recdata[7] == 1){
      digitalWrite(9, HIGH);
    }
    else{
      digitalWrite(9, LOW);
    }

    if (recdata[8] == 1){
      digitalWrite(10, HIGH);
    }
    else{
      digitalWrite(10, LOW);
    }

    if (recdata[9] == 1){
      digitalWrite(11, HIGH);
    }
    else{
      digitalWrite(11, LOW);
    }

    if (recdata[10] == 1){
      digitalWrite(12, HIGH);
    }
    else{
      digitalWrite(12, LOW);
    }

    if (recdata[11] == 1){
      digitalWrite(13, HIGH);
    }
    else{
      digitalWrite(13, LOW);
    }
  }
}
