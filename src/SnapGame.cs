using System;
using SwinGameSDK;
using CardGames.GameLogic;

namespace CardGames
{
    public class SnapGame
    {
        public static void LoadResources()
        {
            Bitmap cards;
            cards = SwinGame.LoadBitmapNamed ("Cards", "Cards.png");
            SwinGame.BitmapSetCellDetails (cards, 167, 250, 13, 5, 53);      // set the cells in the bitmap to match the cards
        }



		/// <summary>
		/// Respond to the user input -- with requests affecting myGame
		/// </summary>
		/// <param name="myGame">The game object to update in response to events.</param>
		private static void HandleUserInput(Snap myGame)
		{
			//Fetch the next batch of UI interaction
			SwinGame.ProcessEvents();

			if (SwinGame.KeyTyped (KeyCode.vk_SPACE))
			{
				myGame.Start ();
			}
		}

		/// <summary>
		/// Draws the game to the Window.
		/// </summary>
		/// <param name="myGame">The details of the game -- mostly top card and scores.</param>
		private static void DrawGame(Snap myGame)
		{
			SwinGame.ClearScreen(Color.White);

			// Draw the top card
			Card top = myGame.TopCard;
			if (top != null)
			{
				SwinGame.DrawText ("Top Card is " + top.ToString (), Color.RoyalBlue, 0, 20);
				SwinGame.DrawText ("Player 1 score: " + myGame.Score(0), Color.RoyalBlue, 0, 30);
				SwinGame.DrawText ("Player 2 score: " + myGame.Score(1), Color.RoyalBlue, 0, 40);
				SwinGame.DrawCell (SwinGame.BitmapNamed ("Cards"), top.CardIndex, 521, 153);
			}
			else
			{
				SwinGame.DrawText ("No card played yet...", Color.RoyalBlue, 0, 20);
			}

			// Draw the back of the cards... to represent the deck
			SwinGame.DrawCell (SwinGame.BitmapNamed ("Cards"), 52, 153, 153);

			//Draw onto the screen
			SwinGame.RefreshScreen(60);
		}

		/// <summary>
		/// Updates the game -- it should flip the cards itself once started!
		/// </summary>
		/// <param name="myGame">The game to be updated...</param>
		private static void UpdateGame(Snap myGame)
		{
			myGame.Update(); // just ask the game to do this...
		}

		
        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("Snap!", 800, 600);

            LoadResources();
            
			Deck myDeck = new Deck ();
			Card testCard = Card.RandomCard();

            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                
				SwinGame.DrawBitmap("QuickSnap_Background.png", 0, 0);
                
                
				//generate a random card on spacebar press
				if (SwinGame.KeyTyped(KeyCode.vk_SPACE) && myDeck.CardsRemaining > 0)
                {
					testCard = myDeck.Draw ();
                }

                //turn over the card on F press
                if (SwinGame.KeyTyped(KeyCode.vk_f))
                {
                    testCard.TurnOver();
                }

                SwinGame.DrawText ("Card generated was: " + testCard.ToString (), Color.RoyalBlue, 0, 20);
                SwinGame.DrawCell (SwinGame.BitmapNamed ("Cards"), testCard.CardIndex, 160, 50);

                //Clear the screen and draw the framerate
                SwinGame.DrawFramerate(0,0);

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}