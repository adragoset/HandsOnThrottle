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
                    States = new VirtualButton[] {
                        new VirtualButton {
                            Id = 1,
                            CurrentColor = Color.Yellow,
                            InitialColor = Color.Yellow,
                            StateList = new Color[] {Color.Yellow,Color.Blue,Color.Purple,Color.Green,Color.Red}
                        }
                    }
                },
                ButtonStates = new ButtonState[]{
                    new ButtonState { 
                        States = new VirtualButton[]{
                            new VirtualButton {
                                    Id=2,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.Yellow,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.Orange
                                    }
                                },
                                new VirtualButton {
                                    Id=9,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                 new VirtualButton {
                                    Id=16,
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.LightBlue
                                    }
                                },
                                new VirtualButton {
                                    Id=23,
                                  CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Magenta
                                    }
                                },
                                new VirtualButton {
                                    Id=30,
                                    CurrentColor = Color.Red,
                                    InitialColor = Color.Red,
                                    StateList = new Color[] {
                                        Color.Red,
                                        Color.White
                                    }
                                }
                        }
                    },
                    new ButtonState { 
                        States = new VirtualButton[]{
                            new VirtualButton {
                                    Id=3,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.Yellow,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.Orange
                                    }
                                },
                                new VirtualButton {
                                    Id=10,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                 new VirtualButton {
                                    Id=17,
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.LightBlue
                                    }
                                },
                                new VirtualButton {
                                    Id=24,
                                  CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Magenta
                                    }
                                },
                                new VirtualButton {
                                    Id=31,
                                    CurrentColor = Color.Red,
                                    InitialColor = Color.Red,
                                    StateList = new Color[] {
                                        Color.Red,
                                        Color.White
                                    }
                                }
                        }
                    },
                    new ButtonState { 
                        States = new VirtualButton[]{
                            new VirtualButton {
                                    Id=4,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.Yellow,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.Orange
                                    }
                                },
                                new VirtualButton {
                                    Id=11,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                 new VirtualButton {
                                    Id=18,
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.LightBlue
                                    }
                                },
                                new VirtualButton {
                                    Id=25,
                                  CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Magenta
                                    }
                                },
                                new VirtualButton {
                                    Id=32,
                                    CurrentColor = Color.Red,
                                    InitialColor = Color.Red,
                                    StateList = new Color[] {
                                        Color.Red,
                                        Color.White
                                    }
                                }
                        }
                    },
                    new ButtonState { 
                        States = new VirtualButton[]{
                            new VirtualButton {
                                    Id=5,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.Yellow,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.Orange
                                    }
                                },
                                new VirtualButton {
                                    Id=12,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                 new VirtualButton {
                                    Id=19,
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.LightBlue
                                    }
                                },
                                new VirtualButton {
                                    Id=26,
                                  CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Magenta
                                    }
                                },
                                new VirtualButton {
                                    Id=33,
                                    CurrentColor = Color.Red,
                                    InitialColor = Color.Red,
                                    StateList = new Color[] {
                                        Color.Red,
                                        Color.White
                                    }
                                }
                        }
                    },
                    new ButtonState { 
                        States = new VirtualButton[]{
                            new VirtualButton {
                                    Id=6,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.Yellow,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.Orange
                                    }
                                },
                                new VirtualButton {
                                    Id=13,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                 new VirtualButton {
                                    Id=20,
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.LightBlue
                                    }
                                },
                                new VirtualButton {
                                    Id=27,
                                  CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Magenta
                                    }
                                },
                                new VirtualButton {
                                    Id=34,
                                    CurrentColor = Color.Red,
                                    InitialColor = Color.Red,
                                    StateList = new Color[] {
                                        Color.Red,
                                        Color.White
                                    }
                                }
                        }
                    },
                    new ButtonState { 
                        States = new VirtualButton[]{
                            new VirtualButton {
                                    Id=7,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.Yellow,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.Orange
                                    }
                                },
                                new VirtualButton {
                                    Id=14,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                 new VirtualButton {
                                    Id=21,
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.LightBlue
                                    }
                                },
                                new VirtualButton {
                                    Id=28,
                                  CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Magenta
                                    }
                                },
                                new VirtualButton {
                                    Id=35,
                                    CurrentColor = Color.Red,
                                    InitialColor = Color.Red,
                                    StateList = new Color[] {
                                        Color.Red,
                                        Color.White
                                    }
                                }
                        }
                    },
                    new ButtonState { 
                        States = new VirtualButton[]{
                            new VirtualButton {
                                    Id=8,
                                    CurrentColor = Color.Yellow,
                                    InitialColor = Color.Yellow,
                                    StateList = new Color[] {
                                        Color.Yellow,
                                        Color.Orange
                                    }
                                },
                                new VirtualButton {
                                    Id=15,
                                    CurrentColor = Color.Blue,
                                    InitialColor = Color.Blue,
                                    StateList = new Color[] {
                                        Color.Blue,
                                        Color.Pink
                                    }
                                },
                                 new VirtualButton {
                                    Id=22,
                                    CurrentColor = Color.Purple,
                                    InitialColor = Color.Purple,
                                    StateList = new Color[] {
                                        Color.Purple,
                                        Color.LightBlue
                                    }
                                },
                                new VirtualButton {
                                    Id=29,
                                  CurrentColor = Color.Green,
                                    InitialColor = Color.Green,
                                    StateList = new Color[] {
                                        Color.Green,
                                        Color.Magenta
                                    }
                                },
                                new VirtualButton {
                                    Id=36,
                                    CurrentColor = Color.Red,
                                    InitialColor = Color.Red,
                                    StateList = new Color[] {
                                        Color.Red,
                                        Color.White
                                    }
                                }
                        }
                    }
                }
            };

        }
    }
}
