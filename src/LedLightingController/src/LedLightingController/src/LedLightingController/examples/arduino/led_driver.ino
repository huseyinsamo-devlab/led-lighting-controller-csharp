// Basit seri komutlu LED/RGB kontrol (UNO/Nano için)
// LED_PIN tek renk; RGB için ayrı pinler kullan.
// Tek renk örnek:
#define LED_PIN 9

// RGB kullanacaksan:
#define R_PIN 9
#define G_PIN 10
#define B_PIN 11
#define USE_RGB 1  // 0 yaparsan tek renk mod

void setup() {
  Serial.begin(115200);
  pinMode(LED_PIN, OUTPUT);
  pinMode(R_PIN, OUTPUT);
  pinMode(G_PIN, OUTPUT);
  pinMode(B_PIN, OUTPUT);
}

void setBright(byte pct) {
#if USE_RGB
  // parlaklığı kaba ölçekle (RGB’de hepsine uygular, basit demo)
  analogWrite(R_PIN, map(pct, 0, 100, 0, 255));
  analogWrite(G_PIN, map(pct, 0, 100, 0, 255));
  analogWrite(B_PIN, map(pct, 0, 100, 0, 255));
#else
  analogWrite(LED_PIN, map(pct, 0, 100, 0, 255));
#endif
}

void setColor(byte r, byte g, byte b) {
#if USE_RGB
  analogWrite(R_PIN, r);
  analogWrite(G_PIN, g);
  analogWrite(B_PIN, b);
#endif
}

void onLed() {
#if USE_RGB
  setBright(100);
#else
  digitalWrite(LED_PIN, HIGH);
#endif
}

void offLed() {
#if USE_RGB
  setBright(0);
#else
  digitalWrite(LED_PIN, LOW);
#endif
}

void loop() {
  if (Serial.available()) {
    String line = Serial.readStringUntil('\n');
    line.trim();
    line.toUpperCase();

    if (line == "ON") onLed();
    else if (line == "OFF") offLed();
    else if (line.startsWith("BRIGHT ")) {
      int pct = line.substring(7).toInt();
      pct = constrain(pct, 0, 100);
      setBright((byte)pct);
    } else if (line.startsWith("COLOR ")) {
      int c1 = line.indexOf(' ');
      int c2 = line.indexOf(',', c1 + 1);
      int c3 = line.indexOf(',', c2 + 1);
      byte r = (byte)line.substring(c1 + 1, c2).toInt();
      byte g = (byte)line.substring(c2 + 1, c3).toInt();
      byte b = (byte)line.substring(c3 + 1).toInt();
      setColor(r, g, b);
    }
  }
}
