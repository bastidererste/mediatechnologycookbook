# How to controll probability for a simple draw based game. 
A client asked me to program a simple draw game for an exhibition. Pushing a "red-button" would trigger a randomized draw and the system would tell you if you won or not. One major feature had to be that the probability of winning should be controllable to make sure certain prices where definitely won between 2pm and 3pm but still could be won outside this time frame; just with less probability.  

### Getting ready

Start your prefered C# editor and create a new .net console application. We will be using the Random() class to generate a draw method for numbers bwtween 0 and 1. A winning draw will be defined. We will then controll the probability by simple if statements. 

### How to do it
```C#
using System;

namespace probability
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Random rnd = new Random();
            double probability;
            
            do
            {

                Console.WriteLine("press any key to draw");
                Console.ReadKey();

                bool increaseProbability = TimeBetween(DateTime.Now, new TimeSpan(14, 0, 0), new TimeSpan(15, 0, 0));

                if (increaseProbability)
                {
                     probability = 0.1;
                }
                else
                {
                     probability = 0.0001;
                }
                
                double draw = rnd.NextDouble();

                Console.WriteLine(draw);

                if (draw < probability)
                {
                    Console.WriteLine("Winner");
                }
                else {

                    Console.WriteLine("Loser");
                }
          

            } while (true);


        bool TimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            TimeSpan now = datetime.TimeOfDay;
            if (start < end)
                return start <= now && now <= end;
            return !(end < now && now < start);
        }

       }
    }
}


```

### How it works
The NextDouble method generates evenly distributet, randomized numbers between 0.0 and 1.0. Probability defines how likely somthing is going to happen. With a probability of lets say 0.5 and 100.000 draws, the randomly generated numbers halfe the time lie above 0.5 and halfe the time below 0.5. With a probability of 0.1 and 100.000 draws the randomly generated numbers 90% of the time lie above 0.1 and 10% the time below 0.1. 

So, to define a winner we compare the draw to the probability we like to set. To define that statistically every 10th person wins a price you set the probability to 0.1 and check if the draw is smaller than 0.1. To define that statistically every 100th person wins a price you set the probability to 0.01 and check if the draw is smaller than 0.01. (With a given minimum of ~250 draws per hour.)  

To increase probability for a given timeframe, we simple compare if datetime.TimeOfDay for a draw falls in between the given timeframe and select a higher probability for this draw. 

### There is more


