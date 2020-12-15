using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Timers;
using UnityEditor.XR;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector2 = UnityEngine.Vector2;

/// <summary>
/// This class manages the genetic algorithm and main loop of the game.
/// </summary>
public class GeneticController : MonoBehaviour
{
    /// <summary>
    /// An Unity GameObject array that contains the instances of the AI bots that will be spawned in game.
    /// </summary>
    public GameObject[] _pool;
    /// <summary>
    /// A reference to the map in which the pathfinding will be made.
    /// </summary>
    public MapManager map;
    private PathFinding _pathFinding;
    private int _skillPoints = 10;
    private CharacterManager playerOne;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject bot;
    private float timer;
    private float geneticTime = 10f;



    // Start is called before the first frame update
    
    /// <summary>
    /// Instantiates every bot in the pool 
    /// </summary>
    void Awake()
    {
        _pool = new GameObject[7];
        for (int botCount = 0; botCount < _pool.Length; botCount++)
        {
            _pool[botCount] = Instantiate(bot);
        }
    }

    /// <summary>
    /// Start the game 
    /// </summary>
    void Start()
    {
        
        
        _SpawnPlayer();
        RandomizeStarters();
        RandomizeScores();
        PositionBots();
        _pathFinding = new PathFinding();
        _pathFinding.SetMap(map);
        
    }

    private void GeneticLoop()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Debug.Log("NUEVO CICLO DEL GENETICO");
            //EJECUTAR LO RELEVANTE A LAS PRUEBAS DEL GENETICO DENTRO DE ESTE IF
            timer = geneticTime;
        }
    }

    private void RandomizeScores()
    {
        var distanceLimit = 15;
        var hitLimit = 5;
        Random randRange = new Random();

        for (int bot = 0; bot < _pool.Length; bot++)
        {
            Enemy currentBot = _pool[bot].GetComponent<Enemy>();
            var prox = 0;
            var hit = 0;
            hit = randRange.Next(0, hitLimit);
            prox = randRange.Next(0,distanceLimit);
            currentBot.proximity = prox;
            currentBot.hitScore = hit;
        }
    }
    
    /// <summary>
    /// Set the player 
    /// </summary>
    private void _SpawnPlayer()
    {
        Vector2 playerSpawn = map.GetWorldPosition(0, 10)+new Vector2(10f,10f) * 0.5f;
        GameObject player = GameObject.Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        playerOne = player.GetComponent<CharacterManager>();
        playerOne.playerMatrixPos.x = 0;
        playerOne.playerMatrixPos.y = 10;

    }
    
    

    /// <summary>
    /// Sets the game map in which the bots will be positioned
    /// </summary>
    /// <param name="gameMap">MapManager</param>
    public void SetMap(MapManager gameMap)
    {
        this.map = gameMap;
    }

    /// <summary>
    /// Set the positions of each bot
    /// </summary>
    public void PositionBots()
    {
        Vector2 botPosition = map.GetWorldPosition(10, 0)+new Vector2(10f,10f) * 0.5f;
        _pool[0].GetComponent<Enemy>()._enemyBody.position = botPosition;
        _pool[0].GetComponent<Enemy>()._botMatrixPos.x = 10;
        _pool[0].GetComponent<Enemy>()._botMatrixPos.y = 0;
        
        botPosition = map.GetWorldPosition(0, 0)+new Vector2(10f,10f) * 0.5f;
        _pool[1].GetComponent<Enemy>()._enemyBody.position = botPosition;
        _pool[1].GetComponent<Enemy>()._botMatrixPos.x = 0;
        _pool[1].GetComponent<Enemy>()._botMatrixPos.y = 0;
        
        botPosition = map.GetWorldPosition(10, 10)+new Vector2(10f,10f) * 0.5f;
        _pool[2].GetComponent<Enemy>()._enemyBody.position = botPosition;
        _pool[2].GetComponent<Enemy>()._botMatrixPos.x = 10;
        _pool[2].GetComponent<Enemy>()._botMatrixPos.y = 10;
        
        botPosition = map.GetWorldPosition(8, 5)+new Vector2(10f,10f) * 0.5f;
        _pool[3].GetComponent<Enemy>()._enemyBody.position = botPosition;
        _pool[3].GetComponent<Enemy>()._botMatrixPos.x = 8;
        _pool[3].GetComponent<Enemy>()._botMatrixPos.y = 5;
        
        botPosition = map.GetWorldPosition(2, 5)+new Vector2(10f,10f) * 0.5f;
        _pool[4].GetComponent<Enemy>()._enemyBody.position = botPosition;
        _pool[4].GetComponent<Enemy>()._botMatrixPos.x = 2;
        _pool[4].GetComponent<Enemy>()._botMatrixPos.y = 5;
        
        botPosition = map.GetWorldPosition(5, 7)+new Vector2(10f,10f) * 0.5f;
        _pool[5].GetComponent<Enemy>()._enemyBody.position = botPosition;
        _pool[5].GetComponent<Enemy>()._botMatrixPos.x = 5;
        _pool[5].GetComponent<Enemy>()._botMatrixPos.y = 7;
        
        botPosition = map.GetWorldPosition(5, 3)+new Vector2(10f,10f) * 0.5f;
        _pool[6].GetComponent<Enemy>()._enemyBody.position = botPosition;
        _pool[6].GetComponent<Enemy>()._botMatrixPos.x = 5;
        _pool[6].GetComponent<Enemy>()._botMatrixPos.y = 3;
        

    }
    
    /// <summary>
    /// Set the stats of each bot 
    /// </summary>
    private void RandomizeStarters()
    {
        var upperLimit = 4;
        Random randRange = new Random();

        for (int botCount = 0; botCount < _pool.Length; botCount++)
        {
            Enemy currentBot = _pool[botCount].GetComponent<Enemy>();
            while (_skillPoints > 0)
            {
                var num = 0;
                num = randRange.Next(1, upperLimit);
                switch (num)
                {
                    case 1:
                        currentBot.health++;
                        _skillPoints--;
                        break;
                    case 2:
                        currentBot.speed++;
                        _skillPoints--;
                        break;
                    case 3:
                        currentBot.radius++;
                        _skillPoints--;
                        break;
                }
            }

            _skillPoints = 10;

            CheckEmptyValues(currentBot);
        }

        RandomProbabilities();
        
    }

    /// <summary>
    /// Generate a list of posibilities for each bot
    /// </summary>
    private void RandomProbabilities()
    {
        Random randRange = new Random();
        for (int botCount = 0; botCount < _pool.Length; botCount++)
        {
            for (int actions = 0; actions != 10; actions++)
            {
                int probability = randRange.Next(1, 5);
                _pool[botCount].GetComponent<Enemy>().actionProbability[actions] = probability;
            }
        }
    }

    /// <summary>
    /// Check if the bot has empty values ​​and in case of having empty values this ​​adjust them
    /// </summary>
    /// <param name="currentBot">Enemy object</param>
    private void CheckEmptyValues(Enemy currentBot)
    {
        if (currentBot.health == 0)
        {
            currentBot.health+=2;
            currentBot.radius--;
            currentBot.speed--;
        }else if (currentBot.speed == 0)
        {
            currentBot.health--;
            currentBot.radius--;
            currentBot.speed+=2;
                
        }else if (currentBot.radius == 0)
        {
            currentBot.health--;
            currentBot.radius+=2;
            currentBot.speed--;
        }
    }
    /// <summary>
    /// Recieves 3 Enemies and make a cross in their actions porbabilities to 
    /// change the actions porbabilities for each bot in the pool 
    /// </summary>
    /// <param name="parent1">Enemy object</param>
    /// <param name="parent2">Enemy object</param>
    /// <param name="parent3">Enemy object</param>
    private void cross(Enemy parent1,Enemy parent2,Enemy parent3)
    {
        Enemy[] tempBotPool=new Enemy[7];
        _pool[0].GetComponent<Enemy>().actionProbability=crossAux(parent1,parent2);
        _pool[1].GetComponent<Enemy>().actionProbability=crossAux(parent2,parent1);
        _pool[2].GetComponent<Enemy>().actionProbability=crossAux(parent1,parent3);
        _pool[3].GetComponent<Enemy>().actionProbability=crossAux(parent3,parent1);
        _pool[4].GetComponent<Enemy>().actionProbability=crossAux(parent2,parent3);
        _pool[5].GetComponent<Enemy>().actionProbability=crossAux(parent3,parent2);
        _pool[6].GetComponent<Enemy>().actionProbability=crossAux(parent1,parent2); //revisar el cruce     
        mutation();
    }
    /// <summary>
    /// Recieve 2 enemies and make a cross with their action probabilities 
    /// </summary>
    /// <param name="parent1">Enemy object</param>
    /// <param name="parent2">Enemy object</param>
    /// <returns></returns>
    private int[] crossAux(Enemy parent1, Enemy parent2)
    {
       int[] actions_parent1= parent1.actionProbability ;
       int[] actions_parent2= parent2.actionProbability ;
       actions_parent2[1]=actions_parent1[1];
       actions_parent2[3]=actions_parent1[3];
       actions_parent2[5]=actions_parent1[5];
       actions_parent2[7]=actions_parent1[7];
       actions_parent2[8]=actions_parent1[8];
       return actions_parent2;
    }

    /// <summary>
    /// scrolls through the list of bots and mutates them with a 33,33333333% probability
    /// </summary>
    private void mutation()
    {
        Random randRange = new Random();
        
        for (int i=0;i<_pool.Length;i++)
        {
            var num = 0; 
            num =randRange.Next(1,3);
            if (num==1){
                _pool[i].GetComponent<Enemy>().actionProbability[randRange.Next(0,11)]=randRange.Next(1, 5);
            }                
        }
    }


    /// <summary>
    /// Makes a bot place a bomb 
    /// </summary>
    /// <param name="botId">Integer</param>
    private void PlaceBomb(int botId)
    {
        Vector2Int playerPos = playerOne.playerMatrixPos;
        Vector2Int botPos = _pool[botId].GetComponent<Enemy>()._botMatrixPos;
        List<Node> path = _pathFinding.FindPath(botPos.x, botPos.y, playerPos.x, playerPos.y);
        //Tomamos bot y modificamos los atributos hitScore y proximity.
        int pathCount = path.Count;
        
        if (pathCount < _pool[botId].GetComponent<Enemy>().proximity)
        {
            _pool[botId].GetComponent<Enemy>().proximity = pathCount;
        }
        if (true){//cambiar true por funcion que detecte el hit
           _pool[botId].GetComponent<Enemy>().hitScore++; 
        }
        
    }

    /// <summary>
    /// Calculate the fitnes based in the proximty and hitscore stat
    /// </summary>
    /// <param name="bot">Enemy object</param>
    /// <returns>Integer</returns>
    private int fitnesAux(Enemy bot){
        int fit=bot.proximity-bot.hitScore;
        return fit;
    }
    
    /// <summary>
    /// Scroll the bots list and calculates the fitness of each to can choose the 3 bots with the higher fitness
    /// </summary>
    private void fitnes(){
        Enemy bot1=null;
        Enemy bot2=null;
        Enemy bot3=null;
        for (int botCount = 0; botCount < _pool.Length; botCount++){
            Enemy bot=_pool[botCount].GetComponent<Enemy>();
            int stat=fitnesAux(bot);
            if(bot1==null){
                bot1=bot;
                continue;
            }
            if(stat<fitnesAux(bot1)){
                if(bot2==null){
                    bot2=bot1;
                    bot1=bot;
                }
                if(bot2!=null){
                    bot3=bot2;
                    bot2=bot1;
                    bot1=bot;
                }
            }
            if(bot2==null){
                bot2=bot;
                continue;
            }
            if(stat<fitnesAux(bot2)){
               if(bot2==null){
                   bot2=bot;
                   continue;
               }            
               if(bot2!=null){
                   bot3=bot2;
                   bot2=bot;
                   continue;
               }
           }
            if (bot3=null){
                bot3=bot;
                continue;
            }
            if(stat<fitnesAux(bot3)){
                bot3=bot;
            }    
        }
        cross(bot1,bot2,bot3);
    }
    
    /// <summary>
    /// Makes a bot follow the player
    /// </summary>
    /// <param name="botId">Integer</param>
    private void ChasePlayer(int botId)
    {
        Vector2Int playerPos = playerOne.playerMatrixPos;
        Enemy bot = _pool[botId].GetComponent<Enemy>();
        Vector2Int botPos = bot._botMatrixPos;
        List<Node> path = _pathFinding.FindPath(botPos.x, botPos.y, playerPos.x, playerPos.y);//FUNCION DE PATHFINDING 
        bot.MoveBot(path);
    }

    /// <summary>
    /// Makes a bot choose an action
    /// </summary>
    private void ActionPerformer()
    {
        ChasePlayer(0);
    }

    // Update is called once per frame
    
    void Update()
    {
        
        ActionPerformer();
        GeneticLoop();
    }
}
