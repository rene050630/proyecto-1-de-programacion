       			MAZE RUNNER 
El indescifrable laberinto te llevará hacia trampas y obstáculos. Mucho cuidado con ellos.
 CARACTERÍSTICAS DEL JUEGO :
Está diseñado para jugar dos personas. Pudiendo elegir entre 5 fichas la que más le guste. Cada ficha cuenta con un poder, tiempo de enfriamiento, nombre y velocidad.
TRAMPAS :
Cuenta con 4 distintas trampas. Al caer en la casilla con una trampa se muestra el mensaje de "Has caído en una trampa" y se realiza la ejecución de la misma. 
Tipos de Trampas:
1- Retrasa tu posición al inicio del laberinto.
2- Retrasa tu posición algunas casillas hacia atrás.
3- Aumenta tu tiempo de enfriamiento.
4- Obstáculos invisibles que al chocar con ellos pierdes tu turno.
TIEMPO DE ENFRIAMIENTO :
Cada ficha cuenta con un tiempo distinto que se va descontando a medida que se ejecuta cada movimiento.
PODERES :
Cada ficha cuenta con un poder. Puedes activarlo pulsando a letra P y cuando el tiempo de enfriamiento esté en 0 del teclado, antes de realizar el movimiento. La ejecución del poder dura un turno, y luego puedes ejecutar tu movimiento.
OBJETIVO Y CONDICIÓN DE VICTORIA :
Llegar al final del laberinto. El jugador que llegue primero gana el juego.
ESTRUCTURA DEL PROYECTO :
Dividido en 7 distintas clases:
class GameMazeRunner : clase principal del juego donde está la lógica de selección de las fichas, los turnos de los jugadores, los movimientos y la lógica general del juego.
Los movimientos se pueden ejecutar a través de las teclas Up para moverse hacia arriba, Down hacia abajo, Left para la izquierda, Rigth hacia la derecha. 
class Interface : Pequeño menú que se ejecuta al iniciar el juego
class Maze : Contiene el algoritmo de generación del laberinto :
Crea una matriz booleana y aleatoriamente coloca casillas en true o false. Luego recorre cada casilla true de la matriz y en cada una de estas, camina un paso para el lado y si la casilla está en true retorna y sigue buscando casillas true. Si está en false, convierte esa casilla a true y chequea la de abajo haciendo la misma operación. Si la posición es no válida, directamente chequea la casilla de abajo convirtiéndola en true, si está en false. Como resultado tenemos un laberinto con todas las casillas caminables, muy abierto y con infinidad de caminos.
class Trap : Contiene la lógica para ubicar trampas :
Como nuestro laberinto es muy abierto para hacerlo más indescifrable contiene infinidad de obstáculos invisibles que se ubican si alrededor de ese obstáculo todas las casillas son true. 
Las demás trampas se ubican usando un for que recorre el laberinto de atrás hacia delante para hacer más complicado el juego.
class Player : Contiene el campo ficha y nombre para guardar el nombre del jugador y la ficha que escoja.
class Token : Guarda entre sus campos, las características de las fichas y las posiciones.
class Program : Se instancia la clase principal.
REQUISITOS PARA JUGAR :
https://code.visualstudio.com/download visual studio code
https://dotnet.microsoft.com/en-us/download 6.0 o superior 
1- Clona el repositorio 
https://github.com/rene050630/proyecto-1-de-programacion
2- Abre el visual studio. 
3- Selecciona la carpeta donde están los archivos del juego.
4- Abre una nueva terminal y ejecuta el juego.
