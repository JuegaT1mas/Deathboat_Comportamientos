# <p align="center">JuegaT1mas</div>
## Deathboat

## Descripción del Juego

*Resuelve los puzzles mientras escapas del monstruo*

*Deathboat* es un juego 3D de Survival Horror y Puzzles. Desarrollado por el equipo de JuegaT1+ para PC y móvil gracias a Unity WebGL, en Deathboat se explora un crucero en el que el jugador tratará de huir de un monstruo que lo acecha.

## Integrantes

* Iván Gómez Ortega: Artista, Modelador 3D. Contacto: i.gomezo.2019@gmail.com , IvanGomezOrtega *

* Rodrigo García Suárez: Programador. Contacto: r.garciasu.2019@alumnos.urjc.es , Rogarsu2014 *

* Mariano Jesús De Biase: Artista, Modelador 3D, Scrum Master. Contacto: mj.debiase.2019@gmail.com , marianoj27 *

* Isabel Escudero Orden: Programador, Encargada de redes. Contacto: i.escudero.2019@alumnos.urjc.es , Isaeo22 *

* Pablo Pomares Crespo: Game designer, Beta tester. Contacto: p.pomaresc.2018@alumnos.urjc.es , A6MFlygon *

## Índice

- [Concepto del Juego] (#concepto-del-juego)
- [Características Principales] 
  * [Objetivos sencillos]
  * [Escenificación innovadora]
  * [Partidas rápidas y rejugables]
- [Estilo del Juego] 
- [Mecánicas del Juego] 


*Los acentos no están disponibles en el índice por el sistema de Markdown*

## Concepto del Juego

En *Deathboat*, seremos el único superviviente del ataque del Diablo Negro, un monstruo marino, en alta mar. Nuestro objetivo será escapar del barco vivo, resolviendo los diversos puzzles que nos encontremos tras el desastre causado por el monstruo, mientras le vamos dando esquinazo.

## Características Principales

* ### Objetivos sencillos

El objetivo principal es lograr escapar del barco, como es típico en este género de juegos.

* ### Escenificación innovadora

Siendo muchos de los escenarios empleados en los Survival-Horror hospitales, colegios, mansiones abandonados, naves espaciales... Deathboat pretende mostrar otra posible escenificación para estos juegos, siendo esta un crucero recreativo.

* ### Partidas rápidas y rejugables

En Deathboat es todo o nada. Aunque sea el mismo barco, lo que el jugador tendrá que realizar para huir cada vez será distinto y en caso de fracasar, el jugador tendrá que comenzar desde el principio.

## Estilo del Juego

El estilo de Deathboat es 3D realista con una temática oscura. El objetivo es que el escenario sea realista y tenebroso, dando así la sensación de inseguridad y miedo del género de terror. Al encontrarnos en un crucero podremos ver objetos relacionados con barcos, también veremos elementos del cine de terror como arañazos en las paredes del barco, marcas de sangre, así como muebles y objetos descolocados o dañados. El barco donde se desarrolla el juego será moderno con muebles de género minimalista.

## Mecánicas del Juego

El objetivo del juego es avanzar por el mapa resolviendo puzles para intentar escapar de una criatura que vaga por el mapa acechando.

El mundo del juego se trata de un mapa 3D cerrado. El jugador tendrá una vista en primera persona durante la mayoría de la partida, exceptuando cuándo interactúe con los puzles, en los cuales la cámara cambiará a una vista cenital del puzle para mejorar la interacción con estos.

* ### Controles de Juego:

Respecto al movimiento, la cámara se mueve en el ordenador con el ratón y en móvil con un touchpad. De forma similar, el movimiento en móvil se realizará mediante otro touchpad en el lado contrario de la pantalla, mientras que en el ordenador se realiza con las teclas WASD. Para agacharse además se podrá usar un botón en el teclado del ordenador (Ctrl) y en el móvil será un botón de acción cercano al touchpad de movimiento.
 
En cuanto a las acciones, para interactuar con los elementos del escenario se hará uso del botón E; mientras que en el móvil, al igual que al agacharse, se usará un botón de acción cercano a este. De manera similar para saltar se usará la Barra Espaciadora y en el móvil otro botón.

* ### Puntuacion:

No está pensado incluirla en el juego, ya que no tiene mucho sentido andar puntuando al jugador en un juego de miedo, puesto que les saca de la inmersión. Como mucho sería planteado al final un timer de cuanto se ha tardado en completar el juego.

* ### Guardar/Cargar:

En cuanto al guardado, ya que el juego no sigue una narrativa lineal y se basa en partidas relativamente cortas, no se le dará al jugador la posibilidad de guardar la partida. Por este motivo, actualmente no se tiene contemplada esta opción.

## Diagrama de flujo de Pantallas

Un punto a destacar del flujo de pantallas es la aparición del menú de ajustes dos veces. Esto se debe a que queremos mostrar la diferencia de relaciones entre los accesos al mismo:

El menú de Ajustes al que se accede desde el menú principal es uno desde el cuál se puede volver al mismo sin ninguna repercusión.

Sin embargo una vez que se accede al menú de Ajustes desde el menú de Pausa y se va al Menú Principal ya no se puede volver a la partida, de ahí la razón para mostrarlo dos veces

## Interfaces

* ### Nombre de la pantalla: Menú principal
* ### Descripción de la Pantalla: Es la primera interfaz del juego y sirve para desplazarse entre los estados de juego iniciales.
* ### Estados de Juego: Llama a los creditos (en Credits), a los ajustes (en Options) y a comenzar una (en Play). Es llamada por los créditos, los ajustes y por el menú de pausa una vez dentro de la partida. 
* ### Otros: La opción de Quit cierra el juego.

* ### Nombre de la pantalla: Créditos
* ### Descripción de la Pantalla: Es la interfaz en la que se va a mostrar a los creadores del juego.
* ### Estados del Juego: Llama al menú principal (Back). Es llamada por el menú principal. 
* ### Otros: Abre una ventana emergente con nuestro twitter

* ### Nombre de la pantalla: Ajustes
* ### Descripción de la Pantalla: Es la interfaz que permite modificar varios parámetros del juego como el volumen del audio, la resolución de la pantalla, si está en pantalla completa y el volumen.
* ### Estados del Juego: Llama al menú principal y puede volver al menú de pausa. Es llamada por el menú principal y por el menú de pausa.

* ### Nombre de la pantalla: Menú de pausa
* ### Descripción de la Pantalla: Es la interfaz a la que se accede en medio de la partida para acceder a los ajustes o salir al menú principal.
* ### Estados del Juego: Llama al menú principal y a los créditos. Se accede a este menú volviendo desde los ajustes si previamente se ha accedido a ellos desde este menú y también se accede durante la partida.

* ### Nombre de la pantalla: HUD de la partida
* ### Descripción de la Pantalla: Es la interfaz que indica el estado del jugador y del mapa.
* ### Estados del Juego: Visible durante la partida.

* ### Nombre de la pantalla: Game Over / Victory Screen
* ### Descripción de la Pantalla: Es la interfaz que aparece una vez terminada la partida.
* ### Estados del Juego: Visible al final de la partida.

## Niveles

* ### Título del Nivel: Apóleia
* ### Encuentro: El jugador llega a este nivel al comenzar una partida. Es el primer y único nivel del juego.
* ### Objetivos del nivel: Resolver los 3 puzzles repartidos por el mapa y llegar a la salida sin ser pillado por el monstruo marino rondando el escenario.
* ### Enemigos: El Diablo Negro
* ### Personajes: El enemigo y el protagonista
* ### Música y Efectos de Sonido: Música solo ambiental, casi imperceptible, con efectos sonoros típicos  de un barco: agua goteando, la madera crujiendo, humedad. Pendiente de implementación.
* ### Referencias de BGM y SFX: Concretamente, se implementarán los sonidos que cree el monstruo (pisadas, gritos) y los que cree el jugador (pisadas, puertas, saltos). Pendiente de implementación.

### El Apóleia es el barco en donde ocurre la aventura de Deathboat. Como tal, es el único nivel del juego, pero cambiará en cada partida el layout de los puzzles en el mapa (para la versión Alpha esta parte no será así); resolver estos puzzles es el método que tiene el jugador para ganar el juego, tras resolver los puzzles el jugador deberá escapar del mapa por el sitio que le sea indicado, sin embargo, si es atrapado por el Diablo Negro varias veces antes de su huida, perderá.

### Para ayudar al jugador, por el mapa se repartirán varios objetos en los cuales el jugador podrá esconderse del monstruo. Los únicos personajes que se han planteado para el juego son el jugador y el Diablo Negro, de los cuales el jugador estará en primera persona y apenas podrá verse a sí mismo.

### Sobre la música, se plantea usar una música ambiental tétrica apenas perceptible, acompañada de efectos sonoros típicos de un barco parado (olas chocando contra el casco, el crujir de la madera, la  humedad,...). Por otro lado, para los SFX se emplearán los fondos que haga nuestro monstruo (pisadas, gritos, arañazos,...) y los que haga el jugador (pisadas, quejidos de dolor, apertura de muebles,...).

