using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Movement
{
    public class Location : MonoBehaviour
    {
        public string locationName;

        public Tilemap[] tilemaps;

        public static Tilemap GetGround(string other)
        {
            var locations = FindObjectsOfType<Location>();
            return (from location in locations where location.locationName == other select location.tilemaps[0]).FirstOrDefault();
        }

        public static Location GetLocationFromString(string name)
        {
            var locations = FindObjectsOfType<Location>();
            return locations.FirstOrDefault(location => location.locationName == name);
        }
    }
}
