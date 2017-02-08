using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CityStreetGridAdvantLvl1
{
    public struct currentLocation
    {
        // I am going to create a struct for EVERY block that I traverse through.
        // Example: Entry R4 would create 8 structs of (0,1),(0,2),(0,3),(0,4)
        // Before I create and add a new location, I need to check that I haven't been here before.
        // If I have been here, what is the distance?
         
        public int _x;
        public int _y;
        public int _distance;

        public currentLocation(int x, int y)
        {
            _x = x;
            _y = y;
            _distance = Math.Abs(x) + Math.Abs(y);            
        }
    }

    class Calculations
    {
        int _distanceFromHQ = 0;
        int _prevX = 0;
        int _prevY = 0;

        public int DistanceFromHQ { set{_distanceFromHQ = value;} }
        
        List<currentLocation> locations = new List<currentLocation>();

        public void AddLocationX(int x) // 1 -3 = -2 = (0, -1, -2)
        {
            bool result = false;
            currentLocation cl;

            int currentX = _prevX;
            int moveTO = currentX + x;

            for(int i = 0; i < Math.Abs(x); i++)
            {                
                if(x >= 0)
                    cl = new currentLocation( (currentX + x) - (x - (i+1)) , _prevY);
                else
                    cl = new currentLocation((currentX + x) + Math.Abs((x + (i+1))), _prevY);

                foreach(currentLocation loc in locations)
                {                                        
                    if(loc.Equals(cl))
                    {
                        result = true;
                        MessageBox.Show("Distance: " + cl._distance.ToString());                        
                    }
                }

                if (result == false)
                { 
                    _distanceFromHQ = cl._distance;
                    locations.Add(cl);
                }
            }

            _prevX += x;
        }

        public void AddLocationY(int y)
        {
            bool result = false;
            currentLocation cl;
            int currentY = _prevY;

            for (int i = 0; i < Math.Abs(y); i++)
            {
                if (y >= 0)
                    cl = new currentLocation( _prevX, (currentY + y) - (y - (i + 1)));
                else
                    cl = new currentLocation( _prevX, (currentY + y) + Math.Abs((y + (i+1))));

                foreach(currentLocation loc in locations)
                {                                        
                    if(loc.Equals(cl) == true)
                    {
                        result = true;
                        MessageBox.Show("Distance: " + cl._distance.ToString());                        
                    }
                }

                if (result == false)
                {                    
                    _distanceFromHQ = cl._distance;
                    locations.Add(cl);
                }
            }
            _prevY += y;
        }               

    }
}
