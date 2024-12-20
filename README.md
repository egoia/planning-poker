# Planning Poker
Ce jeu est une simulation du célèbre Planning Poker, une méthode collaborative utilisée pour estimer la difficulté des tâches à réaliser dans le cadre d’un projet informatique, en impliquant l’ensemble des membres de l’équipe.


## Getting started
- Sur GitHub, aller dans la section "Release" et télécharger l'archive 1.1.zip de la release nommée 1.1 (qui marche)
- Extraire les fichiers de l'archive
- Pour lancer l'application, lancez l'exécutable nommé PlanningPoker

## Comment jouer
- Ouvrez l'application, un menu s'affiche.

- Séléctionnez un fichier JSON rédigé au préalable suivant cette structure :
![alt text](image.png)

- Ou bien, chargez un des fichiers déjà existants dans le dossier fichiersJSON (à la racine du projet).

- Choisissez un mode de jeu :
    1. Strict : les joueurs votent jusqu’à ce que l’unanimité soit acquise. A la fin de chaque tour, si l’unanimité n’est pas atteinte, une page de débat entre les deux extrémités s’affiche. Les joueurs ont 1 minute pour débattre et se mettre d’accord pour le prochain tour. Tant que l’unanimité n’est pas acquise, on recommence le processus autant de fois que nécessaire.
    Pour les prochains modes, le premier tour de chaque tâche se joue quand même sur l’unanimité.
    2. Moyenne : les joueurs votent et la carte gagnante est celle se rapprochant le plus de la moyenne des notes (Cf. fonction numberToCard() de la classe Card).
    3. Médiane : similaire au mode moyenne, mais calcule la médiane à la place.
    4. Majorité relative : les joueurs votent et la carte la plus utilisée est la gagnante. Si aucune carte n’est « plus » utilisée que les autres, la page de débat s’affiche puis le tour est rejoué.
    5. Majorité absolue : similaire un mode majorité relative, mais ici avec la majorité absolue (plus de la moitié des cartes sont similaires).

- Ajoutez des joueurs en mettant leurs noms (un nom n'est pas accepté s'il dépasse 20 caractères, s'il est vide ou si un autre joueur a déjà ce nom)

- Cliquer sur Start. La partie commence !

- Le nom du joueur courant est précisé en haut de l'écran; il doit choisir une carte de sa main pour estimer une tâche donc le titre et le descriptif sont également affichés à l'écran. Une fois sa carte choisie, il doit la déplacer vers le slot (le cadre) et cliquer sur OK.

- S'il y a débat, une page s'affiche avec un chronomètre d'1 minute (que vous pouvez stopper à tout moment) et c'est alors au tour suivant.

- Les cartes Joker et Café :
    -	Joker : le joueur ne parvient pas à évaluer la tâche. Cette carte n’est pas prise en compte dans le calcul de la moyenne ni et la médiane. Pour les modes majorité et strict, si le Joker obtient la majorité ou l’unanimité, le tour recommence. De plus, il empêche l’unanimité même si c’est la seule carte différente des autres.
    -	Café : le joueur veut une pause. Si cette carte obtient l’unanimité ou la majorité, elle provoque un affichage « pause-café » et on peut retourner au jeu en cliquant sur la barre d’espace du clavier. Le bouton « quitter » ici sert à quitter la partie. 

- Une fois la partie terminée, vous pouvez soit quitter le jeu ou revenir au menu pour en commencer une nouvelle.

- Votre fichier JSON est automatiquement sauvegardé à la fin de chaque vote validé. Vous pouvez quitter la partie à tout moment et revenir plus tard. 