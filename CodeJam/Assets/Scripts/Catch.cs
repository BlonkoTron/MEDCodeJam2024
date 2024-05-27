public class Catch 
{
    
    public FishType type;
    public int gold;
    public int possibleElements;
    public int catchInSeconds;

    public Catch(FishType catchType)
    {
        type = catchType;
        switch (catchType)
        {
            case FishType.normal: //1
                gold = 1;
                catchInSeconds = 1;
                possibleElements = 15;
                break;
            case FishType.cat: //2
                gold = 10;
                catchInSeconds = 2;
                possibleElements = 4;
                break;
            case FishType.blob: //3
                gold = 2;
                catchInSeconds = 3;
                possibleElements = 10;
                break;
            case FishType.rainbow: //4
                gold = 2;
                catchInSeconds = 3;
                possibleElements = 10;
                break;
            case FishType.flat: //5
                gold = 2;
                catchInSeconds = 3;
                possibleElements = 10;
                break;
            case FishType.duck: //6
                gold = 20;
                catchInSeconds = 1;
                possibleElements = 2;
                break;
            case FishType.chips: //7
                gold = 5;
                catchInSeconds = 2;
                possibleElements = 6;
                break;
            case FishType.sword: //8
                gold = 20;
                catchInSeconds = 1;
                possibleElements = 2;
                break;
            case FishType.clown: //9
                gold = 5;
                catchInSeconds = 2;
                possibleElements = 5;
                break;
            case FishType.puffer: //10
                gold = 10;
                catchInSeconds = 2;
                possibleElements = 4;
                break;
            case FishType.shrimp: //11
                gold = 100;
                catchInSeconds = 3;
                possibleElements = 1;
                break;
            case FishType.crab: //12
                gold = 5;
                catchInSeconds = 2;
                possibleElements = 7;
                break;
            case FishType.boot: //13
                gold = 0;
                catchInSeconds = 4;
                possibleElements = 8;
                break;
        }
    }


}


