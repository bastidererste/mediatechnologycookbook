

Random rnd = new Random();


//NextDouble() generates a random double evenly distributed between 0.0 and 1.0 including 1.0.
//statisticaly in one out of 100 calls rnd.NextDouble() generates a double smaller than 0.01.
if (rnd.NextDouble() <  0.01)	
{
				Console.WriteLine("winner");
}else
{
				
				Console.WriteLine("loser");
}
			
//statisticaly in one out of 1000 calls rnd.NextDouble() generates a double smaller than 0.001.
if (rnd.NextDouble() <  0.001)	
{
				Console.WriteLine("winner");
}else
{
				
				Console.WriteLine("loser");
}
