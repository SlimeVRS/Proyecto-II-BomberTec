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
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
