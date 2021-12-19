
using System.Collections;
public class Team{

    public static int currentTeamNumber = 1;
    public string teamName{get; private set;}

    public int teamNumber{get; private set;}

    public Team(string name){
        teamName = name;
        teamNumber = currentTeamNumber;
        currentTeamNumber ++ ;
    }




    public override bool Equals(object obj)
    {
        return this == (Team) obj;
    }

    public override string ToString()
    {
        return teamName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(Team x, Team y){
        return (x.teamName == y.teamName) && (x.teamNumber == y.teamNumber); 
    }

    public static bool operator !=(Team x, Team y){
        return !(x == y);
    }


    
}