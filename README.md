# **Pelora \- Platformer de Universos Narrativos (Proyecto 4\)**

**Materia:** Diseño Interactivo / Desarrollo de Videojuegos

**Integrantes del Equipo:**

* Jairo Diaz  
* Juan Jose Losada

## **Descripción del Proyecto**

"Pelora" es un videojuego de plataformas en 2D (scroll lateral) desarrollado en Unity. El juego está basado en un universo narrativo propio, donde el jugador (un minero) debe sortear obstáculos, recolectar objetos de valor y enfrentarse a enemigos para escapar con vida a través de un portal final.

Cumple con todos los requisitos de diseño de niveles mediante **Tilemaps**, animaciones, físicas, y un flujo completo de Interfaz de Usuario (UI) con menús de inicio, pausa y pantallas de victoria/derrota.

## **🌟 3 Características Adicionales Implementadas (Innovación)**

Para enriquecer la experiencia de juego y resolver problemas técnicos comunes en el desarrollo 2D, investigamos e implementamos las siguientes tres mecánicas avanzadas:

### **1\. Sistema de "Sonido Fantasma" Espacial (AudioSource.PlayClipAtPoint)**

**El problema:** En Unity, si se le asigna un AudioSource a un enemigo o a una moneda, el sonido se corta abruptamente en el momento en que el objeto es destruido (Destroy(gameObject)).

**Nuestra solución:** Investigamos e implementamos el método AudioSource.PlayClipAtPoint. Esto nos permite instanciar un clip de audio en una coordenada específica del espacio 3D/2D justo antes de que el objeto desaparezca. De esta forma, el grito de derrota del Troll o el sonido de recolección de la moneda se reproducen completos y de forma independiente al ciclo de vida del GameObject, mejorando drásticamente el *Game Feel*.

### **2\. Inteligencia Artificial de Persecución y Combate con Rebote Físico**

**El problema:** Los enemigos de plataforma básicos suelen moverse estáticamente de izquierda a derecha.

**Nuestra solución:** Programamos un script de Jefe (BossController) que rastrea la posición del jugador mediante Tags. El enemigo (Troll) calcula la distancia en el eje X y ejecuta saltos parabólicos dinámicos hacia la posición actual del jugador.

Además, implementamos un sistema de combate clásico estilo *Mario Bros*, donde el jugador debe aterrizar exactamente sobre el enemigo (calculando que la altura Y del jugador sea mayor a la del enemigo). Al hacerlo, se le resta vida al jefe y se aplica una fuerza de rebote (rb.linearVelocity) al Rigidbody2D del jugador para impulsarlo hacia arriba.

### **3\. Interfaz de Usuario Adaptativa (Canvas Scaler) y TextMeshPro Flotante**

**El problema:** Al compilar el ejecutable, la interfaz estática se recortaba o deformaba en pantallas con resoluciones distintas a la del editor.

**Nuestra solución:** Investigamos el uso del Canvas Scaler en modo **"Scale With Screen Size"** y la correcta aplicación de **Anchors** combinados (Shift \+ Alt). Esto garantiza que el HUD y los Menús se escalen proporcionalmente en cualquier monitor. Adicionalmente, integramos **TextMeshPro (TMP)** no solo en la UI, sino en el espacio del mundo (World Space) para crear un indicador de vida flotante sobre la cabeza del Troll (ej. "2/2"), el cual se actualiza en tiempo real al recibir daño.

## **Controles del Juego**

* **Movimiento:** Teclas A y D (o Flechas Izquierda/Derecha).  
* **Salto:** Tecla Espacio (Soporta doble salto si se adquiere el ítem correspondiente).  
* **Interacción:** Tecla E (Recoger poderes y entregar moneda al Champion).  
* **Pausa:** Botón de Pausa en pantalla (o tecla Esc si está configurada).

## **Enlaces de Entrega**

* **Ejecutable (.zip):** Entregado a través de INTERACTIVA.
