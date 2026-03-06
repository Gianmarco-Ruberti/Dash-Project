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

## Le Défi Technique : 
### La détection du sol
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

### le même saut peu importe la vitesse
Un saut constant quelle que soit la vitesse
Un autre défi majeur a été de garantir que le joueur parcoure toujours la même distance lors d'un saut, indépendamment de sa vitesse horizontale (notamment lors du passage dans des portails d'accélération).

Pour maintenir cette cohérence et éviter que le gameplay ne devienne imprévisible, j'ai implémenté un système d'ajustement dynamique : la force de saut et la gravité sont modifiées en temps réel selon un multiplicateur de vitesse.

        float adjustedJump = jumpForce * multiplier;
    
        if (isVerticalMode)
        {
            float jumpDirectionX = isGravityUp ? adjustedJump : -adjustedJump;
            rb.linearVelocity = new Vector2(jumpDirectionX, rb.linearVelocity.y);
        }
        else
        {
            float jumpDirectionY = isGravityUp ? -adjustedJump : adjustedJump;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpDirectionY);
        }
J'ai choisi de coder le saut via la physique (Rigidbody2D) plutôt que d'utiliser une simple animation pour plusieurs raisons de gameplay cruciales :

**Liberté d'action** : Utiliser une animation pour le saut "verrouille" souvent l'état du joueur. Par exemple, dans mes tests précédents, le joueur devenait invincible ou ignorait les collisions mortelles tant que l'animation n'était pas terminée.

**Gestion des collisions** : Avec un saut physique, les composants de détection de collision restent actifs en permanence. Le joueur peut donc mourir instantanément s'il percute un obstacle, même en plein milieu de sa trajectoire de saut.

## Conclusion
Pour conclure, ce projet dispose d'une base solide mais pourrait bénéficier de plusieurs améliorations, comme la création d'un menu principal et le développement de niveaux. Un point technique reste également à peaufiner : les particules de traînée (trail particles) qui suivent le joueur sont pour le moment fixées à un coin du cube, ce qui manque de naturel lors des rotations et des changement de mode.

Cependant, même si certains aspects peuvent être améliorés, la réalisation de ce jeu m'a énormément plu. Ce projet m'a permis de découvrir en profondeur le fonctionnement du Rigidbody2D, de la physique de saut, et plus globalement des fonctionnalités essentielles d'Unity.
