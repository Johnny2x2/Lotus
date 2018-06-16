using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{


    [SerializeField]
    float Multiplyer = 100f;

    public int DayInMonth = 0;
    public int MilHr = 0;
    public int WorldTimeMin =0;
    public int Month = 0;

    int WorldTimeHr = 0;
    bool Am = true;
    float WorldTimeSec = 0f;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTime();
    }

    //---------------Time stuff-----------------------
    void UpdateTime()
    {
        WorldTimeSec += Time.deltaTime * Multiplyer;
        if (WorldTimeSec >= 60f)
        {
            WorldTimeSec = 0f;
            WorldTimeMin++;
            if (WorldTimeMin >= 60)
            {
                WorldTimeMin = 0;
                WorldTimeHr++;
                MilHr++;
                if (WorldTimeHr >= 12)
                {
                    WorldTimeHr = 0;
                    Am = !Am;
                }
                if (MilHr == 24)
                {                   
                    MilHr = 0;
                    DayInMonth++;
                    if (DayInMonth == 30)
                    {
                        Month++;
                        DayInMonth = 0;
                    }
                }
            }
        }       
    }

    public string DisplayTime()
    {
        if (Am)
        {
            return "Month: " + Month.ToString() + " Day: " + DayInMonth.ToString() + " -- " + WorldTimeHr.ToString() + ":" + WorldTimeMin.ToString() + " AM";
        }
        else
        {
            return "Month: " + Month.ToString() + " Day: " + DayInMonth.ToString() + " -- " + WorldTimeHr.ToString() + ":" + WorldTimeMin.ToString() + " PM";
        }
    }



    public void OverrideTime(int milhr, int min, int day, int month)
    {
        MilHr = milhr;
        WorldTimeMin = min;
        WorldTimeSec = 0;
        DayInMonth = day;
        Month = month;

        if (MilHr < 12)
        {
            Am = true;
            
        }
        else
        {
            Am = false;
        }

        if (Am && MilHr <= 12)
        {
            WorldTimeHr = MilHr;
        }
        else
        {
            WorldTimeHr = MilHr - 12;
        }
    }

    public void NewDay()
    {

    }

    public void NewMonth()
    {

    }


}
