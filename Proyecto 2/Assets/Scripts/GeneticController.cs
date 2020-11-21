using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GeneticController : MonoBehaviour
{
    private GameObject[] _pool;
    private int _skillPoints = 10;
    [SerializeField] private GameObject bot;
    
    // Start is called before the first frame update
    void Start()
    {

        _pool = new GameObject[8];
        float xPosition = 0;
        for (int botCount = 0; botCount < 8; botCount++)
        {
            _pool[botCount] = Instantiate(bot);
            _pool[botCount].GetComponent<Enemy>().Spawn(xPosition);
            xPosition += 1;
        }
        
        RandomizeStarters();
        

    }

    private void RandomizeStarters()
    {
        var upperLimit = 4;
        Random randRange = new Random();

        for (int botCount = 0; botCount < 8; botCount++)
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

    private void RandomProbabilities()
    {
        Random randRange = new Random();
        for (int botCount = 0; botCount < 8; botCount++)
        {
            for (int actions = 0; actions != 10; actions++)
            {
                int probability = randRange.Next(1, 5);
                _pool[botCount].GetComponent<Enemy>().actionProbability[actions] = probability;
            }
        }
    }

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
    private void cross(Enemy parent1,Enemy parent2,Enemy parent3)
    {
    Enemy[] tempBotPool=new Enemy[8];
    _pool[0].GetComponent<Enemy>().actionProbability=crossAux(parent1,parent2);
    _pool[1].GetComponent<Enemy>().actionProbability=crossAux(parent2,parent1);
    _pool[2].GetComponent<Enemy>().actionProbability=crossAux(parent1,parent3);
    _pool[3].GetComponent<Enemy>().actionProbability=crossAux(parent3,parent1);
    _pool[4].GetComponent<Enemy>().actionProbability=crossAux(parent2,parent3);
    _pool[5].GetComponent<Enemy>().actionProbability=crossAux(parent3,parent2);
    _pool[6].GetComponent<Enemy>().actionProbability=crossAux(parent1,parent2);
    //nos falta el ultimo(6)  que podriamos meterle una mutacion...xD     
    }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
