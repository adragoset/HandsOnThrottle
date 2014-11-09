using System;
using Microsoft.SPOT;

namespace Core.KeyPadState
{
    public static class KeyPadConfigurationParser
    {
        public static KeyPadStateData GetConfig(string config)
        {

            //Stream stream = new System.IO.MemoryStream(ByteHelper.GetBytes(config));
            //var settings = new XmlReaderSettings();
            //settings.IgnoreComments = true;
            //settings.IgnoreWhitespace = true;
            //var reader = XmlReader.Create(stream, settings);

            //reader.ReadToFollowing("");

            return new KeyPadStateData
            {
                CommandButton = new ButtonState
                {
                    Id = 1,
                    CurrentColor = Color.Yellow,
                    InitialColor = Color.Yellow,
                    StateList = new Color[] { 
                            Color.Yellow,
                            Color.SeaFoamGreen,
                            Color.Pink,
                            Color.Purple,
                            Color.Blue
                        }
                },
                PageStates = new PageState[]{
                        new PageState{
                            PageNumber = 1,
                            ButtonStates = new ButtonState[]{
                                new ButtonState {
                                    Id=2,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.SeaFoam,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.SeaFoam
                                    }
                                },
                                new ButtonState {
                                    Id=3,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.SeaFoam,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.SeaFoam
                                    }
                                },
                                new ButtonState {
                                    Id=4,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.SeaFoam,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.SeaFoam
                                    }
                                },
                                new ButtonState {
                                    Id=5,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.SeaFoam,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.SeaFoam
                                    }
                                },
                                new ButtonState {
                                    Id=6,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.SeaFoam,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.SeaFoam
                                    }
                                },
                                new ButtonState {
                                    Id=7,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.SeaFoam,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.SeaFoam
                                    }
                                },
                                new ButtonState {
                                    Id=8,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.SeaFoam,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.SeaFoam
                                    }
                                }
                            }
                        }, 
                        new PageState{
                            PageNumber = 2,
                            ButtonStates = new ButtonState[]{ 
                                 new ButtonState {
                                    Id=9,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                new ButtonState {
                                    Id=10,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                new ButtonState {
                                    Id=11,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                new ButtonState {
                                    Id=12,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                new ButtonState {
                                    Id=13,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                new ButtonState {
                                    Id=14,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                new ButtonState {
                                    Id=15,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                }
                            }
                        }, 
                        new PageState{
                            PageNumber = 3,
                            ButtonStates = new ButtonState[]{ 
                                new ButtonState {
                                    Id=16,
                                    CurrentColor = Color.Magenta,
                                    InitialColor = Color.Magenta,
                                    StateList = new Color[] {
                                        Color.Magenta,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=17,
                                    CurrentColor = Color.Magenta,
                                    InitialColor = Color.Magenta,
                                    StateList = new Color[] {
                                        Color.Magenta,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=18,
                                    
                                    CurrentColor = Color.Magenta,
                                    InitialColor = Color.Magenta,
                                    StateList = new Color[] {
                                        Color.Magenta,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=19,
                                    
                                    CurrentColor = Color.Magenta,
                                    InitialColor = Color.Magenta,
                                    StateList = new Color[] {
                                        Color.Magenta,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=20,
                                    
                                    CurrentColor = Color.Magenta,
                                    InitialColor = Color.Magenta,
                                    StateList = new Color[] {
                                        Color.Magenta,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=21,
                                    
                                    CurrentColor = Color.Magenta,
                                    InitialColor = Color.Magenta,
                                    StateList = new Color[] {
                                        Color.Magenta,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=22,
                                    
                                    CurrentColor = Color.Magenta,
                                    InitialColor = Color.Magenta,
                                    StateList = new Color[] {
                                        Color.Magenta,
                                        Color.Orange
                                    }
                                }
                            }
                        }, 
                        new PageState{
                            PageNumber = 4,
                            ButtonStates = new ButtonState[]{ 
                                new ButtonState {
                                    Id=23,
                                    
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.YellowGreen
                                    }
                                },
                                new ButtonState {
                                    Id=24,
                                    
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.YellowGreen
                                    }
                                },
                                new ButtonState {
                                    Id=25,
                                    
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.YellowGreen
                                    }
                                },
                                new ButtonState {
                                    Id=26,
                                    
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.YellowGreen
                                    }
                                },
                                new ButtonState {
                                    Id=27,
                                    
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.YellowGreen
                                    }
                                },
                                new ButtonState {
                                    Id=28,
                                    
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.YellowGreen
                                    }
                                },
                                new ButtonState {
                                    Id=29,
                                    
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.YellowGreen
                                    }
                                }
                            }
                        }, 
                        new PageState{
                            PageNumber = 5,
                            ButtonStates = new ButtonState[]{ 
                                new ButtonState {
                                    Id=30,
                                    
                                    CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=31,
                                    
                                    CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=32,
                                    
                                    CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=33,
                                    
                                    CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=34,
                                    
                                    CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=35,
                                    
                                    CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Orange
                                    }
                                },
                                new ButtonState {
                                    Id=36,
                                    
                                    CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Orange
                                    }
                                }
                            }
                        }
                    },

            };
        }



    }
}
