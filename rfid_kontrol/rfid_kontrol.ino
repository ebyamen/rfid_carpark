#include <MFRC522.h>
#include <SPI.h>

int rst_pin9=9;                    //Giris ve cıkıs pinleri tanımlandı
int ss_pin10=10;
int rst_pin5=5;
int ss_pin6=6;
int rst_pin3=3;
int ss_pin7=7;

MFRC522 giris1(ss_pin10,rst_pin9); //Giris ve cıkıs pinleri tanımlandı
MFRC522 cikis(ss_pin6,rst_pin5);



void setup ()
{
  Serial.begin(9600);
  SPI.begin();
  giris1.PCD_Init();
  cikis.PCD_Init();
}

void loop()
{
  if (giris1.PICC_IsNewCardPresent())
  {
    if (giris1.PICC_ReadCardSerial())
    {

      Serial.print(giris1.uid.uidByte[0]);
      Serial.print(giris1.uid.uidByte[1]);
      Serial.print(giris1.uid.uidByte[2]);
      Serial.println(giris1.uid.uidByte[3]);
    }
    giris1.PICC_HaltA();
  }

if (cikis.PICC_IsNewCardPresent())
  {
    if (cikis.PICC_ReadCardSerial())
    {

      Serial.print(cikis.uid.uidByte[0]);
      Serial.print(cikis.uid.uidByte[1]);
      Serial.print(cikis.uid.uidByte[2]);
      Serial.println(cikis.uid.uidByte[3]);
    }
    cikis.PICC_HaltA();
  }

  
}
