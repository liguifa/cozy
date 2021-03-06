﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled.Json;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTileJsonManager
    {
        private Dictionary<string, CozyTileJsonResult> TilesDictionary = new Dictionary<string, CozyTileJsonResult>();

        public CozyTiledJsonParser Parser { get; set; }

        public CozyTileJsonManager()
        {
            Parser = new CozyTiledJsonParser();
        }

        public CozyTileJsonResult ParseWithFile(string filename)
        {
            if(!TilesDictionary.ContainsKey(filename))
            {
                TilesDictionary[filename] = Parser.ParseWithFile(filename) as CozyTileJsonResult;
            }
            return TilesDictionary[filename];
        }

        public CozyTileJsonResult Parse(string json)
        {
            return Parser.Parse(json) as CozyTileJsonResult;
        }
    }
}
