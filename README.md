# Dash-Project - Unity 2D
Petit projet de moteur de jeu rythmique développé sous Unity. L'objectif était de recréer les sensations physiques précises du jeu Geometri Dash, notamment la gestion de la gravité et les changements de modes de jeu.

Fonctionnalités
Mouvement Automatique : Vitesse constante avec accélération basée sur des flèche.
<img width="506" height="106" alt="image" src="https://github.com/user-attachments/assets/431a48e8-244c-4f5c-b7e5-ffce6534773d" />


Système de Gravité Dynamique : La gravité s'adapte à la vitesse du joueur pour maintenir un saut consistant.
<img width="310" height="141" alt="image" src="https://github.com/user-attachments/assets/975f7796-d1da-4983-9613-6275adea2922" />
<img width="339" height="173" alt="image" src="https://github.com/user-attachments/assets/82bafa21-ea2b-4b52-9406-8befb1ff164a" />


Changement de Gravité : Support complet de la gravité inversée (plafond).

Mode Vertical : Passage du défilement horizontal au mode "vol" sur les murs.

Technologies
Moteur : Unity 2D.

Physique : Rigidbody2D en mode cinématique/manuel.

Scripting : C# (Détection par OverlapBox pour une précision au pixel près).
