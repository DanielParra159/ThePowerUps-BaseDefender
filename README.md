# ThePowerUps-BaseDefender

Este proyecto es un ejemplo de como se puede aplicar la [Clean Architecture](https://youtu.be/UYHTSwtWQIM) en Unity.
El proyecto lo estoy desarrollando en directo en mi canal de [Twitch](https://www.twitch.tv/thepowerupslearning).

Antes de ejecutar el proyecto tendrás que configurar tu servidor de PlayFab, primero conecta tu cuenta desde Window/PlayFab/Extensions. Una vez conectada, tienes que subir las configuraciones del juego. Para esto abre la ventana de extensiones de PlayFab, y en la pestaña de Data, crea la key: `InitialUnits` con los valores: `{"unitsId":["Unit001", "Unit002"]}`.
Ahora busca dentro de la carpeta Configurations/Units las Unidades 001 y 002 y pulsa sobre "Save On Server".

El repositorio utiliza Actions automáticas para generar una build cada vez que abrimos un Pull Request, estas actions las encontrarás en [https://game.ci/](https://game.ci/)

Puedes seguir la evolución del juego en el tablero de [Trello](https://trello.com/b/NukOzCil/juego-defender-la-base).

Si tienes cualquier pregunta, no dudes en pasarte por el server de [Discord](https://discord.gg/KWABp4BfN4) dónde somos más de 300 devs compartiendo nuestros conocimientos.
