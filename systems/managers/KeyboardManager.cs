using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Abyss_Call
{
	public class KeyboardManager
	{
		public KeyboardState CurrentState { get; private set; }
		public KeyboardState PreviousState { get; private set; }

		public KeyboardManager()
		{
			CurrentState = Keyboard.GetState();
			PreviousState = CurrentState;
		}

		public bool IsKeyDown(Keys key)
		{
			return CurrentState.IsKeyDown(key);
		}

		public bool IsKeyUp(Keys key)
		{
			return CurrentState.IsKeyUp(key);
		}

		public bool IsKeyPressed(Keys key)
		{
			return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
		}

		public bool IsKeyReleased(Keys key)
		{
			return PreviousState.IsKeyDown(key) && CurrentState.IsKeyUp(key);
		}

		public void Flush(GameTime gameTime)
		{
			PreviousState = CurrentState;
			CurrentState = Keyboard.GetState();
		}
	}
}
