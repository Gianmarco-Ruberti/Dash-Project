# Dash-Project - Unity 2D
Petit projet de moteur de jeu rythmique développé sous Unity. L'objectif était de recréer les sensations physiques précises du jeu Geometri Dash, notamment la gestion de la gravité et les changements de modes de jeu.

## Fonctionnalités
Mouvement Automatique : Vitesse constante avec accélération basée sur des flèche.
<img width="506" height="106" alt="image" src="https://github.com/user-attachments/assets/431a48e8-244c-4f5c-b7e5-ffce6534773d" />


Système de Gravité Dynamique : La gravité s'adapte à la vitesse du joueur pour maintenir un saut consistant.
<img width="310" height="141" alt="image" src="https://github.com/user-attachments/assets/975f7796-d1da-4983-9613-6275adea2922" />
<img width="339" height="173" alt="image" src="https://github.com/user-attachments/assets/82bafa21-ea2b-4b52-9406-8befb1ff164a" />


Changement de Gravité : Support complet de la gravité inversée (plafond).

<img width="483" height="217" alt="image" src="https://github.com/user-attachments/assets/d47a00c9-86ab-4e2a-90a3-0381ab28e551" />


Mode Vertical : Passage du défilement horizontal au mode "Climb" sur les murs.

<img width="356" height="309" alt="image" src="https://github.com/user-attachments/assets/be097538-d664-43fc-a8ab-ac12838da3d4" />


## Technologies
Moteur : Unity 2D.

Physique : Rigidbody2D en mode cinématique/manuel.

Scripting : C# (Détection par OverlapBox pour une précision au pixel près).

## Le Défi Technique : La détection du sol
Le plus gros défi a été de créer un système de détection de collision (Grounded) qui fonctionne dans toutes les directions.

j'ai implémenté 4 points de détection (groundCheck) :

Bas (Sol normal)

Haut (Gravité inversée)

Gauche/Droite (Mode Vertical)

    //Logique de décision selon le mode
    if (!isVerticalMode)
    {
        // En mode normal, on ne peut sauter que si on touche le sol ou le plafond
        isGrounded = touchingDown || touchingUp;
    }
    else
    {
        // En mode vertical, on ne peut sauter que si on touche un mur
        isGrounded = touchingDownV || touchingUpV;
    }
<img width="414" height="174" alt="image" src="https://github.com/user-attachments/assets/4a3f3f66-af14-4b53-8977-0189854ca511" />
