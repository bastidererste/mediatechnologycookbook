

Random rnd = new Random();


//NextDouble() generates a random double evenly distributed between 0.0 and 1.0 including 1.0.
//statisticaly rnd.NextDouble() generates a double smaller than 0.01 in one out of 100 calls 
if (rnd.NextDouble() <  0.01)	
{
				Console.WriteLine("winner");
}else
{
				
				Console.WriteLine("loser");
}
			
//statisticaly rnd.NextDouble() generates a double smaller than 0.001 in one out of 1000 calls. Its much less probable now to win.
if (rnd.NextDouble() <  0.001)	
{
				Console.WriteLine("winner");
}else
{
				
				Console.WriteLine("loser");
}

//If you build a live game for an event, and you need to "make sure" the price is given out 
//between 14:00 and 15:00, you can simply increse winning probability for a draw at this time.
bool increaseProbability = TimeBetween(DateTime.Now, new TimeSpan(14, 0, 0), new TimeSpan(15, 0, 0););

if(increaseProbability)
{ 
	probability=0.1;
}
else
{ 
	probability=0.001;
}

bool TimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
{
    TimeSpan now = datetime.TimeOfDay;
    if (start < end)
        return start <= now && now <= end;
    return !(end < now && now < start);
}
